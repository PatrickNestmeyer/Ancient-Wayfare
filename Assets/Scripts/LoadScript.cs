using UnityEngine;
using System.Collections;

public class LoadScript : MonoBehaviour
{   
    public GameObject gameManager;
    public GameObject army;
    public CoordinateSystem cs;
    
	void Start ()
    {
        cs = CoordinateSystem.Instance;
        if(GameManager.instance == null)
            Instantiate(gameManager);
        if(Army.instance == null)
            Instantiate(army);
	}
}