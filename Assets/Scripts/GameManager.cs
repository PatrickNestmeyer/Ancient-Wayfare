using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
    
    public static GameManager instance;
	public BoardManager boardScript;
    public Userinteraface UI;
    public Resources resources;
    
    private Level level;
	private int currentLevel;
    private bool doingSetup;
    private GlobalSettings GS;
    
	void Start ()
    {
        if(instance == null)
            instance = this;
        else if(instance != this)
            Destroy(gameObject);
            
        level = Level.Instance;
        GS = GlobalSettings.Instance;
        currentLevel = GS.startingLevel;
        
        resources = new Resources(GS.startingEquipment, GS.startingGold, GS.startingFighters, GS.startingFood);
        
        UI = Userinteraface.Instance;
        
        DontDestroyOnLoad(gameObject);
		boardScript = GetComponent<BoardManager>();
		InitGame();
	}
    
    private void OnLevelWasLoaded(int index)
    {
        currentLevel++;
        InitGame();
    }
	
	void InitGame()
	{
        doingSetup = true;
        
        level.createLevel(currentLevel);
        
        InitGuiElements();
        
        Invoke("StartLevel", GS.levelStartDelay);
        
		boardScript.SetupScene(currentLevel);
	}
    
    private void StartLevel()
    {
        
        UI.backGroundImage.SetActive(false);
        UI.centerText.text = "";
        UI.headText.text = "";
        doingSetup = false;
    }
    
    private void InitGuiElements()
    {
        //should take place in Level.cs
        string setting = "";
        switch(currentLevel)
        {
            case 1:
                setting = "Greece - fertile valley near Athens";
                break;
            case 2:
                setting = "Egypt - Nile River Delta";
                break;
            case 3:
                setting = "Phoenicia - dry and rolling landscape";
                break;
            case 4: 
                setting = "Mesopotamia - Euphrates";
                break;
        }
        UI.backGroundImage.SetActive(true);
        UI.centerButton.SetActive(false);
        UI.leftButton.SetActive(false);
        UI.rightButton.SetActive(false);
        UI.centerText.text = setting;
        UI.headText.text = "Level " + currentLevel.ToString();
        UI.equipmentText.text = "";
        UI.goldText.text = "";
        UI.fightersText.text = "";
        UI.foodText.text = "";
        UI.yourFightersText.text = "";
        UI.enemyFightersText.text = "";
    }
    
    public void GameOver()
    {
        UI.centerText.text = "You were not strong enough!";
        UI.backGroundImage.SetActive(true);
        enabled = false;
    }
	
	void Update ()
    {
        //if(doingSetup)
	}
}