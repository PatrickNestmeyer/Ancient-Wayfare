using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    
    public static GameManager instance = null;
	public BoardManager boardScript;
	
	public int level = 2;

	// Use this for initialization
	void Start () 
    {
        if(instance == null)
            instance = this;
        else if(instance != this)
            Object.Destroy(gameObject);
            
        Object.DontDestroyOnLoad(gameObject);
		boardScript = GetComponent<BoardManager>();
		InitGame();
	}
	
	void InitGame()
	{
		boardScript.SetupScene(level);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}