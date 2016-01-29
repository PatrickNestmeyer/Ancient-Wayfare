using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Army : MovingObject 
{   
    public CoordinateSystem cs;
    public static Army instance = null;
    
    public bool isInLocation = false;
    public int enemies;
    private Level level;    
    private GlobalSettings GS;
    private Userinteraface UI;
    private Resources resources;
    public Position position;
    private Position previousPosition;
    private bool leftMovement = false;
    private Animator animator;
    private string lastPlace;

	protected override void Start ()
    {
        if(instance == null)
            instance = this;
        else if(instance != this)
            Destroy(gameObject);
        
        level = Level.Instance;
        resources = GameManager.instance.resources;
        GS = GlobalSettings.Instance;
        UI = Userinteraface.Instance;
        cs = CoordinateSystem.Instance;
        animator = GetComponent<Animator>();
        
        UI.fightersText.text = "Fighters: " + resources.Fighters;
        UI.foodText.text = "Food: " + resources.Food;
        UI.goldText.text = "Gold: " + resources.Gold;
        UI.equipmentText.text = "Equipment: " + resources.Equipment;
        
        //army spawns in the middle of left row at x=0, y=1
        position = new Position(GS.ArmySpawnPositionX, GS.ArmySpawnPositionY);
        previousPosition = new Position(GS.ArmySpawnPositionX, GS.ArmySpawnPositionY);
        transform.position = new Vector3(cs.Positions[position.Row][position.Column].x, cs.Positions[position.Row][position.Column].y, 0f);
        
        base.Start();
	}
    
    public void RepositionArmy( int positionArmyX, int positionArmyY)
    {
        float X = cs.Positions[positionArmyX][positionArmyY].x;
        float Y = cs.Positions[positionArmyX][positionArmyY].y;
        transform.position = new Vector3(X,Y,0f);
    }
   
   public void Withdraw()
   {
       RepositionArmy(position.Row, position.Column);
   }
    
    private void CheckIfGameOver()
    {
        if(resources.Fighters <= 0)
        {
            GameManager.instance.GameOver();
        }
    }
    
    private void CheckIfLevelComplete()
    {
        if(level.Key == true)
        {
            enabled = false;
            GameManager.instance.LevelComplete();
        }else{
            position.Column = previousPosition.Column;
            position.Row = previousPosition.Row;
        }
    }
    
    private void OnTriggerEnter2D (Collider2D other)
    {
        
        if(isInLocation == false)
        {
            isInLocation = true;
            if(other.tag == "Asylum" && level.AsylumVisited == false)
            {
                level.AsylumVisited = true;
                UI.backGroundImage.SetActive(true);
                UI.headText.text = "Asylum";
                UI.centerText.text = level.AsylumAnnouncement;
                UI.bottomButton.SetActive(true);
                resources.Food += level.AsylumDonation;
                UI.foodText.text = "Food: " + resources.Food;
            }
            if(other.tag == "City")
            {
                UI.backGroundImage.SetActive(true);
                UI.headText.text = level.CityAnnouncement;
                UI.yourFightersText.text = "Food: " + level.FoodCosts;
                UI.centerText.text = "Equipment: " + level.EquipmentUnlockCosts;
                UI.enemyFightersText.text = "Fighter: " + level.FighterCosts;
                UI.leftButton.GetComponentInChildren<Text>().text = "Buy";
                UI.centerButton.GetComponentInChildren<Text>().text = "Buy";
                UI.rightButton.GetComponentInChildren<Text>().text = "Buy";
                UI.bottomButton.SetActive(true);
                UI.leftButton.SetActive(true);
                UI.centerButton.SetActive(true);
                UI.rightButton.SetActive(true);
            }
            if((other.tag == "Cave" || other.tag == "Forest" || other.tag == "Swamp") && resources.Fighters > 0)
            {
                UI.backGroundImage.SetActive(true);
                UI.headText.text = "Fight in Hideout";
                UI.yourFightersText.text = "Fighters: " + resources.Fighters;
                enemies = Random.Range(level.MinEnemies, level.MaxEnemies);
                UI.enemyFightersText.text = "Enemies: " + enemies;
                UI.leftButton.GetComponentInChildren<Text>().text = "Attack";
                UI.rightButton.GetComponentInChildren<Text>().text = "Withdraw";
                UI.leftButton.SetActive(true);
                UI.rightButton.SetActive(true);
            }
            if(other.tag == "Mountain" || other.tag == "Sea" || other.tag == "Desert")
            {
                
            }
        }else{
            isInLocation = false;
        }
    }
	
    protected override void AttemptMove (float xDir, float yDir)
    {
       if(resources.Food > 0)
       {
           resources.Food -= resources.Fighters;
       }else{
           resources.Fighters -= GS.starvingFighters;
       }
       
       if(resources.Food < 0)
        resources.Food = 0;
       if(resources.Fighters < 0)
        resources.Fighters = 0;
       UI.foodText.text = "Food: " + resources.Food;
       UI.fightersText.text = "Fighters: " + resources.Fighters;
       
       base.AttemptMove(xDir, yDir);
    }
    
	void Update () 
    {
        if(!movementInProgress)
        {
            
            float horizontal;
            float vertical;
            
            //Up-Left Movement
            if(Input.GetKeyUp("q") == true || Input.GetKeyUp("w") == true)
            {
                //Not possible if in first(left) column or in last(upper) row
                if(position.Column == 3 || position.Row == 0)
                    return;
                if(position.Row % 2 == 0)
                {
                    previousPosition.Column = position.Column;
                    position.Column++;
                }
                previousPosition.Row = position.Row;
                position.Row--;
                horizontal = GS.horizontalMovementShort * -1;
                vertical = GS.verticalMovement;
                leftMovement = true;
            }
            //Up-Right Movement
            else if(Input.GetKeyUp("e") == true || Input.GetKeyUp("r") == true)
            {
                //Not possible if in last(right) column or in last(upper) row
                if(position.Column == 3 || position.Row == 12)
                    return;
                if(position.Row % 2 == 0)
                {
                    previousPosition.Column = position.Column;
                    position.Column++;
                }
                previousPosition.Row = position.Row;
                position.Row++;
                horizontal = GS.horizontalMovementShort;
                vertical = GS.verticalMovement;
            }
            //Left Movement
            else if(Input.GetKeyUp("a") == true || Input.GetKeyUp("s") == true)
            {
                //Not possible if in first or second (left) column
                if(position.Row == 0 || position.Row == 1)
                    return;
                previousPosition.Row = position.Row;
                position.Row-=2;
                vertical = 0;
                horizontal = GS.horizontalMovementLong * -1;
                leftMovement = true;
            }
            //Right Movement
            else if(Input.GetKeyUp("d") == true || Input.GetKeyUp("f") == true)
            {
                //Not possible if in last or forelast (right) column
                if(position.Row == 11 || position.Row == 12)
                    return;
                previousPosition.Row = position.Row;
                position.Row += 2;
                vertical = 0;
                horizontal = GS.horizontalMovementLong;
            }
            //Down-Left Movement
            else if(Input.GetKeyUp("y") == true || Input.GetKeyUp("x") == true)
            {
                //Not possible if in first(left) column or last(down) row
                if(position.Row == 0 || (position.Column == 0 && position.Row % 2 == 1))
                    return;
                if(position.Row % 2 == 1)
                {
                    previousPosition.Column = position.Column;
                    position.Column--;
                }
                previousPosition.Row = position.Row;
                position.Row--;
                horizontal = GS.horizontalMovementShort * -1;
                vertical = GS.verticalMovement * -1;
                leftMovement = true;
            }
            //Down-Right Movement
            else if(Input.GetKeyUp("c") == true || Input.GetKeyUp("v") == true)
            {
                //Not possible if in last(right) column or last(down) row
                if(position.Row == 12 || (position.Column == 0 && position.Row % 2 == 1))
                    return;
                if(position.Row % 2 == 1)
                {
                    previousPosition.Column = position.Column;
                    position.Column--;
                }
                previousPosition.Row = position.Row;
                position.Row++;
                horizontal = GS.horizontalMovementShort;
                vertical = GS.verticalMovement * -1;
            }
            else
            {
                return;
            }
            if(leftMovement)
                transform.Rotate(0,180,0);
            animator.SetTrigger("army_Start");
            AttemptMove(horizontal, vertical);
        //If already a movement is in progress the moving keys are will be blocked
        }else{
            return;
        }
	}
    
    protected override void OnMovementFinished()
    {
        base.OnMovementFinished();
        animator.SetTrigger("armyStop");
        
        if(position.Row >= (GS.rows-2))
            CheckIfLevelComplete();
        
        if(leftMovement)
        {
            transform.Rotate(0,180,0);
            leftMovement = false;
        }
        transform.position = new Vector3(cs.Positions[position.Row][position.Column].x, cs.Positions[position.Row][position.Column].y, 0f);
        //reposition Army to avoid position deviation after several turns
        CheckIfGameOver();
        RepositionArmy(position.Row, position.Column);
    }
}