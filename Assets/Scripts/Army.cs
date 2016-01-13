using UnityEngine;
using System.Collections;

public class Army : MovingObject 
{   
    public float horizontalMovement = 1.0f;
    public float verticalMovement = 1.0f;
    public CoordinateSystem cs;
    public static Army instance = null;
    private int gold;
    private int fighters;
    private int food;
    private string equipment;
    private bool key;
    private int starvingFighters;
    public int xPos;
    public int yPos;
    
    private float restartLevelDelay = 1f;
    private Animator animator;
    
    public int Gold
    {
        get{ return gold;}
        set{ gold = value;}
    }
    public int Fighters
    {
        get{ return fighters;}
        set{ fighters = value;}
    }
    public int Food
    {
        get{ return food;}
        set{ food = value;}
    }
    public string Equipment
    {
        get{ return equipment;}
        set{ equipment = value;}
    }

	protected override void Start ()
    {
        if(instance == null)
            instance = this;
        else if(instance != this)
            Destroy(gameObject);
        
        cs = CoordinateSystem.Instance;
        animator = GetComponent<Animator>();
        food = GameManager.instance.armyFood;
        gold = GameManager.instance.armyGold;
        fighters = GameManager.instance.armyFighters;
        equipment = GameManager.instance.armyEquipment;
        
        float spawnArmyX, spawnArmyY;
        spawnArmyX = cs.Positions[0][1].x;
        spawnArmyY = cs.Positions[0][1].y;
        xPos = 0;
        yPos = 1;
        this.transform.position += new Vector3(spawnArmyX, spawnArmyY, 0f);
        
        base.Start();
	}
    
    private void OnDisable()
    {
        GameManager.instance.armyFood = food;
        GameManager.instance.armyGold = gold;
        GameManager.instance.armyFighters = fighters;
        GameManager.instance.armyEquipment = equipment;
    }
    
    private void CheckIfGameOver()
    {
       if(fighters <= 0)
        GameManager.instance.GameOver();
    }
    
    private void OnTriggerEnter2D (Collider2D other)
    {
        if(other.tag == "Asylum")
        {
            
        }
        if(other.tag == "City")
        {
            
        }
        if(other.tag == "Cave" || other.tag == "Forest" || other.tag == "Swamp")
        {
            
        }
        if(other.tag == "Mountain" || other.tag == "Sea" || other.tag == "Desert")
        {
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
            float horizontal = horizontalMovement;
            float vertical = verticalMovement;
            
            //Up-Left Movement
            if(Input.GetKeyUp("q") == true || Input.GetKeyUp("w") == true)
            {
                if(yPos == 0 || xPos == 0)
                    return;
                horizontal *= -1;
            }
            //Up-Right Movement
            else if(Input.GetKeyUp("e") == true || Input.GetKeyUp("r") == true)
            {
                if(yPos == 0 || xPos == 12)
                    return;
            }
            //Left Movement
            else if(Input.GetKeyUp("a") == true || Input.GetKeyUp("s") == true)
            {
                if(xPos == 0)
                    return;
                vertical = 0;
                horizontal *= -1;
            }
            //Right Movement
            else if(Input.GetKeyUp("d") == true || Input.GetKeyUp("f") == true)
            {
                if(xPos == 12)
                    return;
                vertical = 0;
            }
            //Down-Left Movement
            else if(Input.GetKeyUp("y") == true || Input.GetKeyUp("x") == true)
            {
                if((yPos % 2 == 0 && yPos == 2) || (yPos % 2 == 1 && yPos == 3) || xPos == 0)
                    return;
                vertical *= -1;
                horizontal *= -1;
            }
            //Down-Right Movement
            else if(Input.GetKeyUp("c") == true || Input.GetKeyUp("v") == true)
            {
                if((yPos % 2 == 0 && yPos == 2) || (yPos % 2 == 1 && yPos == 3) || xPos == 12)
                    return;
                vertical *= -1;
            }
            else
            {
                return;
            }
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
        
        /* TODO:
         * refresh xPos, yPos
         * maybe set army direction forward and only invert if left movement
         *
         */
    }
}