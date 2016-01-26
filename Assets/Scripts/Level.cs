using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;
public class Level
{
    private static Level instance;
    public static Level Instance
    {
        get
        {
            if(instance == null)
                instance = new Level();
            return instance;
        }
    }
    
    private Level()
    {    
        GS = GlobalSettings.Instance;
        fields = new string[GS.boardLength];
    }
	
    public string UnlockableEquipment;
    public int EquipmentUnlockCosts;
    public string LevelAnnouncement;
    public string CityAnnouncement;
    public string AsylumAnnouncement;
    public int AsylumDonation;
    public int MaxEnemies;
    public int MinEnemies;
    public string KeyFreeText;
    public int DefeatEnemyReward;
    public int FoodCosts;
    public int FighterCosts;
	public string[] fields;
	public string hideoutType;
	public Position[] hideoutPositions;
	public Position cityPosition;
	public Position asylumPosition;
	
	private int rnd;
	private int index = 0;
    private GlobalSettings GS;
    
    //Indexer for fields
	public string this[int number]
	{
		get
		{
			if(number >= 0 && number < fields.Length)
			{ return fields[number]; }
			return "Error";
		}
		set
		{
		    if (number >= 0 && number < fields.Length)
		    {fields[number] = value;}
		}
	}
	
	//Getter and setter for index
	public int Index
	{
		get{ return index; }
		set{ index = value; }		
	}
	
	public void createLevel(int level)
	{
        FoodCosts = 1;
        FighterCosts = 2;
		switch (level)
		{
			case 1: 
				LevelOneFieldSetup();
				break;
			case 2: 
				LevelTwoFieldSetup();
				break;
			case 3: 
				LevelThreeFieldSetup();
				break;
			case 4: 
				LevelFourFieldSetup();
				break;
		}
		keyFieldSetup(level);
	}
	
	private void LevelOneFieldSetup()
	{
        UnlockableEquipment = "medium";
        EquipmentUnlockCosts = 25;
        LevelAnnouncement = "Greece - fertile valley near Athens";
        CityAnnouncement = "A nearby fortified city allows you to refresh. You can buy javelins for your Army.";
        AsylumDonation = 20;
        AsylumAnnouncement = "Near where you started an asylum supplies you with " + AsylumDonation + " Food";
        MaxEnemies = 7;
        MinEnemies = 3;
        KeyFreeText = "Chained to a tree your fighters find a  boatbuilder. Maybe he can help us to cross the Mediterranean?";
        DefeatEnemyReward = 15;
		hideoutType = "frs";
		hideoutPositions = new Position[] { (new Position(GS.hideout1XPos, GS.hideout1YPos)), (new Position(GS.hideout2XPos, GS.hideout2YPos)), (new Position(GS.hideout3XPos, GS.hideout3YPos)) };
        cityPosition = new Position(GS.cityPositionXPos, GS.cityPositionYPos);
		asylumPosition = new Position(GS.asylumPositionXPos, GS.asylumPositionYPos);
		
		//fill ground
		for(int i = 0; i < GS.groundLength; i++)
		{
			//level 1 - greece - consists of grassland, agriculture and woodland ( 1 - 1 - 1)
			rnd = Random.Range(0,3);
			if(rnd == 0)
				fields[i] = "gl";
			if(rnd == 1)
				fields[i] = "ac";
			if(rnd == 2)
				fields[i] = "wl";
		}
	}
	
	private void LevelTwoFieldSetup()
	{	
        UnlockableEquipment = "heavy";
        EquipmentUnlockCosts = 25;
        LevelAnnouncement = "Egypt - Nile River Delta";
        CityAnnouncement = "A nearby fortified city allows you to refresh. You can buy wooden shields for your Army.";
        AsylumDonation = 25;
        AsylumAnnouncement = "Near where you arrived an asylum supplies you with " + AsylumDonation + " Food";
        MaxEnemies = 10;
        MinEnemies = 6;
        KeyFreeText = "After you slaughtered the hideout near country folks wants to join your journey. They know the way through the mountains.";
        DefeatEnemyReward = 20;
		hideoutType = "swp";
		hideoutPositions = new Position[] { (new Position(GS.hideout1XPos, GS.hideout1YPos)), (new Position(GS.hideout2XPos, GS.hideout2YPos)), (new Position(GS.hideout3XPos, GS.hideout3YPos)) };
		cityPosition = new Position(GS.cityPositionXPos, GS.cityPositionYPos);
		asylumPosition = new Position(GS.asylumPositionXPos, GS.asylumPositionYPos);
		
		//fill ground till transientFields
		for(int i = 0; i < (GS.groundLength - GS.transientFieldsLength); i++)
		{
			//level 2 - egypt - consists of grassland, agriculture and woodland till transientFields ( 3 - 2 - 1)
			rnd = Random.Range(0,6);
			if(rnd >= 0 || rnd <= 3)
				fields[i] = "gl";
			if(rnd  == 4)
				fields[i] = "ac";
			if(rnd == 5)
				fields[i] = "wl";
		}
		//fill ground from transientFields till keyGround with steppe
		for(int i = (GS.groundLength - GS.transientFieldsLength); i < GS.groundLength; i++)
		{
			fields[i] = "sp";
		}
	}
	
	private void LevelThreeFieldSetup()
	{
        UnlockableEquipment = "superior";
        EquipmentUnlockCosts = 25;
        LevelAnnouncement = "Phoenicia - dry and rolling landscape";
        CityAnnouncement = "A nearby fortified city allows you to refresh. You can buy horses for your Army.";
        AsylumDonation = 25;
        AsylumAnnouncement = "Near where you arrived an asylum supplies you with " + AsylumDonation + " Food";
        MaxEnemies = 15;
        MinEnemies = 10;
        KeyFreeText = "Close to where you defeated your enemy some camels stand around. We can use them to cross the desert.";
        DefeatEnemyReward = 25;
		hideoutType = "cve";
		hideoutPositions = new Position[] { (new Position(GS.hideout1XPos, GS.hideout1YPos)), (new Position(GS.hideout2XPos, GS.hideout2YPos)), (new Position(GS.hideout3XPos, GS.hideout3YPos)) };
		cityPosition = new Position(GS.cityPositionXPos, GS.cityPositionYPos);
		asylumPosition = new Position(GS.asylumPositionXPos, GS.asylumPositionYPos);
		
		//fill ground
		for(int i = 0; i < GS.groundLength; i++)
		{
			//level 3 - phenicia - consists of steppe only
			fields[i] = "sp";
		}
	}
	
	private void LevelFourFieldSetup()
	{
        UnlockableEquipment = "No Equipment available";
        EquipmentUnlockCosts = 25;
        LevelAnnouncement = "Mesopotamia - Euphrates";
        CityAnnouncement = "A nearby fortified city allows you to refresh. The locals warn you not to get close to the mountains. There will be a behemoth";
        AsylumDonation = 30;
        AsylumAnnouncement = "Near where you arrived an asylum supplies you with " + AsylumDonation + " Food";
        MaxEnemies = 20;
        MinEnemies = 15;
        KeyFreeText = "";
        DefeatEnemyReward = 30;
		hideoutType = "frs";
		hideoutPositions = new Position[] { (new Position(GS.hideout1XPos, GS.hideout1YPos)), (new Position(GS.hideout2XPos, GS.hideout2YPos)), (new Position(GS.hideout3XPos, GS.hideout3YPos)) };
		cityPosition = new Position(GS.cityPositionXPos, GS.cityPositionYPos);
		asylumPosition = new Position(GS.asylumPositionXPos, GS.asylumPositionYPos);
		
		for(int i = 0; i < GS.transientFieldsLength; i++)
		{
			fields[i] = "sp";
		}
		for(int i = GS.transientFieldsLength; i < GS.groundLength; i++)
		{
			//level 4 - mesopotamia - consists of grassland, agriculture and woodland till transientFields ( 3 - 2 - 1)
			rnd = Random.Range(0,6);
			if(rnd >= 0 || rnd <= 3)
				fields[i] = "gl";
			if(rnd  == 4)
				fields[i] = "ac";
			if(rnd == 5)
				fields[i] = "wl";
		}
	}
	
	private void keyFieldSetup(int level)
	{
		for(int i = GS.groundLength; i < GS.boardLength; i++)
		{
			switch(level)
			{
				case 1: 
					fields[i] = "sea";
					break;
				case 3: 
					fields[i] = "dsr";
					break;
				default:
					fields[i] = "mtn";
					break;
			}
		}
	}
}