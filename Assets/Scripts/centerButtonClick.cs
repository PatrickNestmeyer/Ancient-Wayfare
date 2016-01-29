using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class centerButtonClick : MonoBehaviour 
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
        buttonText = UI.centerButton.GetComponentInChildren<Text>().text;
        
        if(buttonText == "Buy")
        {
            if(resources.Gold >= level.EquipmentUnlockCosts && resources.Equipment != level.UnlockableEquipment)
            {
                resources.Gold -= level.EquipmentUnlockCosts;
                resources.setEquipment(level.UnlockableEquipment);
                UI.goldText.text = "Gold: " + resources.Gold;
                UI.equipmentText.text = "Food: " + resources.Equipment;
            }
        }
        if(buttonText == "Finish")
        {
            Application.Quit();
        }
    }
}