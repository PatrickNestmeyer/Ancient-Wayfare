using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Army : MovingObject 
{   
    public float horizontalMovementLong = 1.6f;
    public float horizontalMovementShort = 0.8f;
    public float verticalMovement = 1.3f;
    public CoordinateSystem cs;
    public static Army instance = null;
    public int gold;
    public int fighters;
    public int food;
    public string equipment = "light";
    public Text levelTextGold;
    public Text levelTextFighters;
    public Text levelTextFood;
    public Text levelTextEquipment;
    
    private bool key;
    private int starvingFighters = 5;
    public int xPos;
    public int yPos;
    private bool leftMovement = false;
    private float restartLevelDelay = 1f;
    private Animator animator;

	protected override void Start ()
    {
        if(instance == null)
            instance = this;
        else if(instance != this)
            Destroy(gameObject);
        
        cs = CoordinateSystem.Instance;
        animator = GetComponent<Animator>();
        
        this.food = GameManager.instance.food;
        this.gold = GameManager.instance.gold;
        this.fighters = GameManager.instance.fighters;
        this.equipment = GameManager.instance.equipment;
        
        levelTextFighters = GameObject.Find("Text_Fighters").GetComponent<Text>();
        levelTextFood = GameObject.Find("Text_Food").GetComponent<Text>();
        levelTextGold = GameObject.Find("Text_Gold").GetComponent<Text>();
        levelTextEquipment = GameObject.Find("Text_Equipment").GetComponent<Text>();
        
        levelTextFighters.text = "Fighters: " + this.fighters;
        levelTextFood.text = "Food: " + this.food;
        levelTextGold.text = "Gold: " + this.gold;
        levelTextEquipment.text = "Equipment: " + this.equipment;
        
        //army spawns in the middle of left row at x=0, y=1
        xPos = 0;
        yPos = 1;
        transform.position = new Vector3(cs.Positions[xPos][yPos].x, cs.Positions[xPos][yPos].y, 0f);
        
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
       if(fighters <= 0)
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
                Invoke("Restart", restartLevelDelay);
                enabled = false;
            }
        }
    }
	
    protected override void AttemptMove (float xDir, float yDir)
    {
       if(food > 0)
       {
           food -= fighters;
       }else{
           food = 0;
           fighters -= starvingFighters;
       }
       
       levelTextFood.text = "Food: " + food;
       levelTextFighters.text = "Fighters: " + fighters;
       
       base.AttemptMove(xDir, yDir);
       
       //RaycastHit2D hit;
       
       CheckIfGameOver();
    }
    
    private void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
    
	void Update () 
    {
        //TODO: invert army direction if necessary
        if(!movementInProgress)
        {
            float horizontal;
            float vertical;
            
            //Up-Left Movement
            if(Input.GetKeyUp("q") == true || Input.GetKeyUp("w") == true)
            {
                //Not possible if in first(left) column or in last(upper) row
                if(yPos == 3 || xPos == 0)
                    return;
                if(xPos % 2 == 0)
                    yPos++;
                xPos--;
                horizontal = horizontalMovementShort * -1;
                vertical = verticalMovement;
                leftMovement = true;
            }
            //Up-Right Movement
            else if(Input.GetKeyUp("e") == true || Input.GetKeyUp("r") == true)
            {
                //Not possible if in last(right) column or in last(upper) row
                if(yPos == 3 || xPos == 12)
                    return;
                if(xPos % 2 == 0)
                    yPos++;
                xPos++;
                horizontal = horizontalMovementShort;
                vertical = verticalMovement;
            }
            //Left Movement
            else if(Input.GetKeyUp("a") == true || Input.GetKeyUp("s") == true)
            {
                //Not possible if in first or second (left) column
                if(xPos == 0 || xPos == 1)
                    return;
                xPos-=2;
                vertical = 0;
                horizontal = horizontalMovementLong * -1;
                leftMovement = true;
            }
            //Right Movement
            else if(Input.GetKeyUp("d") == true || Input.GetKeyUp("f") == true)
            {
                //Not possible if in last or forelast (right) column
                if(xPos == 11 || xPos == 12)
                    return;
                xPos += 2;
                vertical = 0;
                horizontal = horizontalMovementLong;
            }
            //Down-Left Movement
            else if(Input.GetKeyUp("y") == true || Input.GetKeyUp("x") == true)
            {
                //Not possible if in first(left) column or last(down) row
                if(xPos == 0 || (yPos == 0 && xPos % 2 == 1))
                    return;
                if(xPos % 2 == 1)
                    yPos--;
                xPos--;
                horizontal = horizontalMovementShort * -1;
                vertical = verticalMovement * -1;
                leftMovement = true;
            }
            //Down-Right Movement
            else if(Input.GetKeyUp("c") == true || Input.GetKeyUp("v") == true)
            {
                //Not possible if in last(right) column or last(down) row
                if(xPos == 12 || (yPos == 0 && xPos % 2 == 1))
                    return;
                if(xPos % 2 == 1)
                    yPos--;
                xPos++;
                horizontal = horizontalMovementShort;
                vertical = verticalMovement * -1;
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
        transform.position = new Vector3(cs.Positions[xPos][yPos].x, cs.Positions[xPos][yPos].y, 0f);
        //reposition Army to avoid position deviation after several turns
        repositionArmy(xPos, yPos);
    }
}