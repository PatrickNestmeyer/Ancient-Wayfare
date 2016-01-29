using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class leftButtonClick : MonoBehaviour {

	private Level level;
    private Resources resources;
    private Userinteraface UI;
    private string buttonText;
    private int rnd;
    private int enemies;
    
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
                UI.goldText.text = "Gold: " + resources.Gold;
                UI.foodText.text = "Food: " + resources.Food;
            }
        }
        
        if(buttonText == "Attack")
        {
            StartCoroutine(Attack());
        }
    }
    
    IEnumerator Attack()
    {
        UI.leftButton.SetActive(false);
        UI.rightButton.SetActive(false);
        int attackFighters = (int) (resources.Fighters * resources.AttackFactor);
        int attackEnemy;
        
        enemies = Army.instance.enemies;
        enemies -= attackFighters;
        if(enemies < 0)
            enemies = 0;
        
        Army.instance.enemies = enemies;
        
        UI.enemyFightersText.text = "Enemies: " + enemies;
        UI.centerText.text = "Fighters killed " + attackFighters + " Enemies";
        yield return new WaitForSeconds(2);
        
        if(enemies > 0)
        {
            //Counterattack
            UI.centerText.text = "Enemies starting Counterattack ";
            yield return new WaitForSeconds(2);
            attackEnemy = (int) (enemies * resources.DefenseFactor);
            resources.Fighters -= attackEnemy;
            Highscore.instance.KilledEnemies += attackEnemy;
            if(resources.Fighters < 0)
                resources.Fighters = 0;
            UI.fightersText.text = "Fighters: " + resources.Fighters;
            UI.centerText.text = "Enemies killed " + attackEnemy + " Fighters";
            yield return new WaitForSeconds(2);
            if(resources.Fighters == 0)
            {
                if(level.CurrentLevel == 4)
                    Army.instance.bossfight = false;
                //Defeat
                UI.RemoveText();
                UI.DisableButtons();
                GameManager.instance.GameOver();
            }else{
                UI.rightButton.SetActive(true);
                UI.leftButton.SetActive(true);
            }
        }else{
            //Victory
            if(level.CurrentLevel == 4 && Army.instance.bossfight == true)
            {
                Debug.Log("Won");
                GameManager.instance.GameWon();
            }
            else
            {
                GameManager.instance.FightVictory();
            }
            
        }
    }
}