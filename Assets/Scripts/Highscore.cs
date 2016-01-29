using UnityEngine;
using System.Collections;

public class Highscore : MonoBehaviour {
    
    public static Highscore instance;
    public float Time = 0.0f;
    public int KilledEnemies = 0;
    
	void Start () 
    {
	   if(instance == null)
            instance = this;
        else if(instance != this)
            Destroy(gameObject);
            
       DontDestroyOnLoad(gameObject);
	}
	
	void Update () 
    {
	   Time += UnityEngine.Time.deltaTime;
	}
    
    public int getHighscore()
    {
        int TimeScore = 6000 / (int) Time;
        return (TimeScore + KilledEnemies);
    }
}
