using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class rightButtonClick : MonoBehaviour {

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
        buttonText = UI.rightButton.GetComponentInChildren<Text>().text;
        
        if(buttonText == "Buy")
        {
            if(resources.Gold >= level.FighterCosts)
            {
                resources.Gold -= level.FighterCosts;
                resources.Fighters++;
            }
        }
        if(buttonText == "Withdraw")
        {
            if(resources.Fighters > 1)
                resources.Fighters /= 2;
            UI.DisableButtons();
            UI.RemoveText();
            UI.backGroundImage.SetActive(false);
            
            //Reposition army
        }
    }
}