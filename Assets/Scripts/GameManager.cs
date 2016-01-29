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
        level.createLevel(currentLevel);
        
        UI.RemoveText();
        UI.DisableButtons();
        InitGuiElements();
        
		boardScript.SetupScene(currentLevel);
	}
    
    private void StartLevel()
    {
        level.AsylumVisited = false;
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
        Army.instance.position.Row = level.asylumPosition.Row;
        Army.instance.position.Column = level.asylumPosition.Column;
        Army.instance.RepositionArmy(Army.instance.position.Row, Army.instance.position.Column);
        resources.Fighters = GS.startingFighters;
        resources.Food = GS.startingFood;
        UI.bottomButton.SetActive(true);
        enabled = false;
    }
    
    public void FightVictory()
    {
        UI.RemoveText();
        UI.DisableButtons();
        
        UI.headText.text = "Victory - You found " + level.DefeatEnemyReward + "Gold";
        if(level.Key == false)
        {
            UI.centerText.text = level.KeyFreeText;
            level.Key = true;
        }
        resources.Gold += level.DefeatEnemyReward;
        UI.goldText.text = "Gold: " + resources.Gold;
        UI.bottomButton.SetActive(true);
    }
    
    public void LevelComplete()
    {
        Application.LoadLevel(Application.loadedLevel);
        
    }
	
	void Update ()
    {
        //if(doingSetup)
	}
}