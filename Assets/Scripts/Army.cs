using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Army : MovingObject 
{   
    public CoordinateSystem cs;
    public static Army instance = null;
    public Text levelTextGold;
    public Text levelTextFighters;
    public Text levelTextFood;
    public Text levelTextEquipment;
    
    private GlobalSettings GS;
    private Userinteraface UI;
    private Resources resources;
    private bool key;
    public Position position;
    private bool leftMovement = false;
    private Animator animator;
    private string lastPlace;

	protected override void Start ()
    {
        if(instance == null)
            instance = this;
        else if(instance != this)
            Destroy(gameObject);
        
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
        position = new Position(0, 1);
        transform.position = new Vector3(cs.Positions[position.Row][position.Column].x, cs.Positions[position.Row][position.Column].y, 0f);
        
        base.Start();
	}
    
    private void repositionArmy( int positionArmyX, int positionArmyY)
    {
        float X = cs.Positions[positionArmyX][positionArmyY].x;
        float Y = cs.Positions[positionArmyX][positionArmyY].y;
        transform.position = new Vector3(X,Y,0f);
    }
   
   /* 
    private void OnDisable()
    {
        GameManager.instance.food = this.food;
        GameManager.instance.gold = this.gold;
        GameManager.instance.fighters = this.fighters;
        GameManager.instance.equipment = this.equipment;
    }
    */
    
    private void CheckIfGameOver()
    {
       if(resources.Fighters <= 0)
        GameManager.instance.GameOver();
    }
    
    private void OnTriggerEnter2D (Collider2D other)
    {
        if(other.tag == "Asylum")
        {
            Debug.Log("Asylum");
        }
        if(other.tag == "City")
        {
            Debug.Log("City");
        }
        if(other.tag == "Cave" || other.tag == "Forest" || other.tag == "Swamp")
        {
            Debug.Log("Hideout");
        }
        if(other.tag == "Mountain" || other.tag == "Sea" || other.tag == "Desert")
        {
            Debug.Log("Key");
            if(key == true)
            {
                Invoke("Restart", GS.restartLevelDelay);
                enabled = false;
            }
        }
    }
	
    protected override void AttemptMove (float xDir, float yDir)
    {
       if(resources.Food > 0)
       {
           resources.Food -= resources.Fighters;
       }else{
           resources.Food = 0;
           resources.Fighters -= GS.starvingFighters;
       }
       
       UI.foodText.text = "Food: " + resources.Food;
       UI.fightersText.text = "Fighters: " + resources.Fighters;
       
       base.AttemptMove(xDir, yDir);
       
       CheckIfGameOver();
    }
    
    private void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
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
                    position.Column++;
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
                    position.Column++;
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
                    position.Column--;
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
                    position.Column--;
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
        if(leftMovement)
        {
            transform.Rotate(0,180,0);
            leftMovement = false;
        }
        transform.position = new Vector3(cs.Positions[position.Row][position.Column].x, cs.Positions[position.Row][position.Column].y, 0f);
        //reposition Army to avoid position deviation after several turns
        repositionArmy(position.Row, position.Column);
    }
}