using UnityEngine;
using System.Collections;

public class LoadScript : MonoBehaviour {
    
    public GameObject gameManager;
    
	void Start () 
    {
        if(GameManager.instance == null)
            Object.Instantiate(gameManager);
       
	}
}
