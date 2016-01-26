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
        UI = Userinteraface.Instance;
        resources = Resources.Instance;
        
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
        
        UI.RemoveText();
        UI.DisableButtons();
        
        level.createLevel(currentLevel);
        
        InitGuiElements();
        
        //Invoke("StartLevel", GS.levelStartDelay);
        
		boardScript.SetupScene(currentLevel);
	}
    
    private void StartLevel()
    {
        UI.DisableButtons();
        UI.RemoveText();
    }
    
    private void InitGuiElements()
    {
        UI.backGroundImage.SetActive(true);
        UI.bottomButton.SetActive(true);
        UI.centerText.text = level.LevelAnnouncement;
        UI.headText.text = "Level " + currentLevel.ToString();
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