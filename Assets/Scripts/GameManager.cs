using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{    
    public float turnDelay = .5f;
    public static GameManager instance = null;
	public BoardManager boardScript;
    public int armyGold, armyFighters, armyFood;
    public string armyEquipment;
    
	public int level = 1;
    
	void Start ()
    {
        if(instance == null)
            instance = this;
        else if(instance != this)
            Destroy(gameObject);
        
        if(level == 1 || level == 2 || level == 3 || level == 4)
        {
            armyGold = 10;
            armyFighters = 10;
            armyFood = 10;
            armyEquipment = "light";
        }
        
        DontDestroyOnLoad(gameObject);
		boardScript = GetComponent<BoardManager>();
		InitGame();
	}
	
	void InitGame()
	{
		boardScript.SetupScene(level);
	}
    
    public void GameOver()
    {
        enabled = false;
    }
	
	void Update () 
    {
        
	}
}