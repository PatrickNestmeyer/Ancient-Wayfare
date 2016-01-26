using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class bottomButtonClick : MonoBehaviour
{
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
        buttonText = UI.bottomButton.GetComponentInChildren<Text>().text;
        
        if(buttonText == "Continue")
        {
            UI.backGroundImage.SetActive(false);
            UI.RemoveText();
            UI.DisableButtons();
        }
    }
}
