using UnityEngine;
using System.Collections;

public class Army : MovingObject 
{   
    public CoordinateSystem cs;
    public static Army instance = null;
    private int gold;
    private int fighters;
    private int food;
    private string equipment;
    private bool key;
    private int starvingFighters;
    
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
	
    protected override void AttemptMove (int xDir, int yDir)
    {
       if(food > 0)
       {
           food -= fighters;
       }else{
           food = 0;
           fighters -= starvingFighters;
       }
        
       base.AttemptMove(xDir, yDir);
        
       RaycastHit2D hit;
        
       CheckIfGameOver();
    }
    
    protected override void OnCantMove<T>(T component)
    {
        //Do not move
        //animator.setTrigger("armyStart");
        //animator.setTrigger("armyStop");
    }
    
    private void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
    
	void Update () 
    {
	    int horizontal = 0;
        int vertical = 0;
       
        horizontal = (int) Input.GetAxisRaw("Horizontal");
        vertical = (int) Input.GetAxisRaw("Vertical");
       
        if(vertical == 0 || (vertical != 0 && horizontal != 0))
        {
            AttemptMove(horizontal, vertical);
        }
        else
        {
            return;
        }
	}
}