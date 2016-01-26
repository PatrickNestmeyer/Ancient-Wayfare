using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Userinteraface {

    private static Userinteraface instance;
    
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
    
    private Userinteraface()
    {
        backGroundImage = GameObject.Find("BackgroundImage");
        centerButton = GameObject.Find("centerButton");
        leftButton = GameObject.Find("leftButton");
        rightButton = GameObject.Find("rightButton");
        centerText = GameObject.Find("centerText").GetComponent<Text>();
        headText = GameObject.Find("headText").GetComponent<Text>();
        equipmentText = GameObject.Find("equipmentText").GetComponent<Text>();
        goldText = GameObject.Find("goldText").GetComponent<Text>();
        fightersText = GameObject.Find("fightersText").GetComponent<Text>();
        foodText = GameObject.Find("foodText").GetComponent<Text>();
        yourFightersText = GameObject.Find("yourFightersText").GetComponent<Text>();
        enemyFightersText = GameObject.Find("enemyFightersText").GetComponent<Text>();    
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
}