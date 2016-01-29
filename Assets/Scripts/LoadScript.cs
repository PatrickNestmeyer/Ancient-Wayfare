using UnityEngine;
using System.Collections;

public class LoadScript : MonoBehaviour
{   
    public GameObject gameManager;
    public GameObject army;
    public CoordinateSystem CS;
    public Userinteraface UI;
    public GlobalSettings GS;
    public Highscore HS;
    
	void Start ()
    {
        GS = GlobalSettings.Instance;
        CS = CoordinateSystem.Instance;
        UI = Userinteraface.Instance;
        if(Highscore.instance == null)
            Instantiate(HS);
        if(GameManager.instance == null)
            Instantiate(gameManager);
        if(Army.instance == null)
            Instantiate(army);
        
	}
}