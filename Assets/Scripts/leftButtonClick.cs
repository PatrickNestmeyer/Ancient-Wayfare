using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class leftButtonClick : MonoBehaviour {

	private Level level;
    private Resources resources;
    private Userinteraface UI;
    private string buttonText;
    
    void Start()
    {
        level = Level.Instance;
        UI = Userinteraface.Instance;
        resources = Resources.Instance;
    }
    
	public void OnClick()
    {
        buttonText = UI.leftButton.GetComponentInChildren<Text>().text;
        
        if(buttonText == "Buy")
        {
            if(resources.Gold >= level.FoodCosts)
            {
                resources.Gold -= level.FoodCosts;
                resources.Food ++;
            }
        }
        
        if(buttonText == "Attack")
        {
            //Attack
        }
    }
}