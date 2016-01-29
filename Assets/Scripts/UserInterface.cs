using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Userinteraface {

    private static Userinteraface instance;
    
    public GameObject uiCanvas;
    public GameObject backGroundImage;
    public Text headText;
    public Text centerText;
    public Text equipmentText;
    public Text goldText;
    public Text fightersText;
    public Text foodText;
    public Text yourFightersText;
    public Text enemyFightersText;
    public GameObject centerButton;
    public GameObject leftButton;
    public GameObject rightButton;
    public GameObject bottomButton;
    
    public Userinteraface()
    {
        
       backGroundImage = GameObject.Find("BackgroundImage");
       centerButton = GameObject.Find("centerButton");
       leftButton = GameObject.Find("leftButton");
       rightButton = GameObject.Find("rightButton");
       bottomButton = GameObject.Find("bottomButton");
       centerText = GameObject.Find("centerText").GetComponent<Text>();
       headText = GameObject.Find("headText").GetComponent<Text>();
       equipmentText = GameObject.Find("equipmentText").GetComponent<Text>();
       goldText = GameObject.Find("goldText").GetComponent<Text>();
       fightersText = GameObject.Find("fightersText").GetComponent<Text>();
       foodText = GameObject.Find("foodText").GetComponent<Text>();
       yourFightersText = GameObject.Find("yourFightersText").GetComponent<Text>();
       enemyFightersText = GameObject.Find("enemyFightersText").GetComponent<Text>();
       
       uiCanvas = GameObject.Find("uiCanvas");
       UnityEngine.MonoBehaviour.DontDestroyOnLoad(uiCanvas);
    }
    
    public static Userinteraface Instance
    {
        get
        {
            if(instance == null)
                instance = new Userinteraface();
            return instance;
        }
    }
    
    public void RemoveText()
    {
        headText.text = "";
        centerText.text = "";
        yourFightersText.text = "";
        enemyFightersText.text = "";
    }
    
    public void DisableButtons()
    {
        centerButton.GetComponentInChildren<Text>().text = "";
        leftButton.GetComponentInChildren<Text>().text = "";
        rightButton.GetComponentInChildren<Text>().text = "";
        centerButton.SetActive(false);
        leftButton.SetActive(false);
        rightButton.SetActive(false);
        bottomButton.SetActive(false);
    }
}