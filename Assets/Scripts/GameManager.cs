using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
    public float levelStartDelay = 2f;
    public static GameManager instance = null;
	public BoardManager boardScript;
    public int gold;
    public int fighters;
    public int food;
    public string equipment = "light";
    
    private Text levelText;
    private GameObject levelImage;
	private int level = 1;
    private bool doingSetup;
    
	void Start ()
    {
        if(instance == null)
            instance = this;
        else if(instance != this)
            Destroy(gameObject);
        
        fighters = 10;
        gold = 10;
        food = 50;
        equipment = "light";
        
        DontDestroyOnLoad(gameObject);
		boardScript = GetComponent<BoardManager>();
		InitGame();
	}
    
    private void OnLevelWasLoaded(int index)
    {
        level++;
        InitGame();
    }
	
	void InitGame()
	{
        doingSetup = true;
        
        //should take place in Level.cs
        string setting = "";
        switch(level)
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
        
        levelImage = GameObject.Find("LevelImage");
        levelText = GameObject.Find("LevelText").GetComponent<Text>();
        levelText.text = setting;
        levelImage.SetActive(true);
        Invoke("HideLevelImage", levelStartDelay);
        
		boardScript.SetupScene(level);
	}
    
    private void HideLevelImage()
    {
        levelImage.SetActive(false);
        doingSetup = false;
    }
    
    public void GameOver()
    {
        levelText.text = "You were not strong enough!";
        levelImage.SetActive(true);
        enabled = false;
    }
	
	void Update () 
    {
        //if(doingSetup)
	}
}