using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
    public static GameManager instance;
	public BoardManager boardScript;
    public Userinteraface UI;
    public Resources resources;
    
    private Highscore HS;
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
        UI.FindUiElements();
        resources = Resources.Instance;
        HS = Highscore.instance;
        
        DontDestroyOnLoad(gameObject);
		boardScript = GetComponent<BoardManager>();
		InitGame();
	}
	
    private void OnLevelWasLoaded(int index)
    {
        currentLevel++;
        UI.FindUiElements();
        InitGame();
        Army.instance.asylumVisited = false;
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
        Army.instance.isInLocation = false;
        Army.instance.RepositionArmy(Army.instance.position.Row, Army.instance.position.Column);
        resources.Fighters = GS.startingFighters;
        resources.Food = GS.startingFood;
        UI.bottomButton.SetActive(true);
        enabled = false;
    }
    
    public void GameWon()
    {
        UI.RemoveText();
        UI.DisableButtons();
        
        UI.headText.text = "Game Finihed";
        UI.centerText.text = "Hihgscore: " + HS.getHighscore();
        UI.centerButton.GetComponentInChildren<Text>().text = "Finish";
        UI.centerButton.SetActive(true);
    }
    
    public void FightVictory()
    {
        UI.RemoveText();
        UI.DisableButtons();
        
        //UI.headText.text = HS.Time.ToString();
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
        if(currentLevel < 4)
        {
            Destroy(UI.uiCanvas);
            Application.LoadLevel(Application.loadedLevel);
        }
        else
        {
            //congrats
        }
    }
	
	void Update ()
    {
        //if(doingSetup)
	}
}