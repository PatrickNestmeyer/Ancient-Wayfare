using UnityEngine;
using System.Collections;

public class LoadScript : MonoBehaviour
{   
    public GameObject gameManager;
    public GameObject army;
    public CoordinateSystem CS;
    public Userinteraface UI;
    public GlobalSettings GS;
    
	void Start ()
    {
        GS = GlobalSettings.Instance;
        CS = CoordinateSystem.Instance;
        UI = Userinteraface.Instance;
        if(GameManager.instance == null)
            Instantiate(gameManager);
        if(Army.instance == null)
            Instantiate(army);
	}
}