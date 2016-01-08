using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;
public class Level
{
	//Sum of all fields
	public int boardLength = 45;
	//Walkable Fields
	public int groundLength = 38;
	//Fields only walkable with key
	public int keyGroundLength = 7;
	//transient fields before or after a keyground
	public int transientFieldsLength = 7;
	
	public string[] fields = new string[45];
	
	public string hideoutType;
	public Position[] hideoutPositions;
	public Position cityPosition;
	public Position asylumPosition;
	
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
	
	private int rnd;
	private int index = 0;
	
	public void createLevel(int level)
	{
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
		hideoutType = "frs";
		hideoutPositions = new Position[] { (new Position(6,0)), (new Position(4,2)), (new Position(10,2)) };
		cityPosition = new Position(6, 1);
		asylumPosition = new Position(3, 1);
		
		//fill ground
		for(int i = 0; i < groundLength; i++)
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
		hideoutType = "swp";
		hideoutPositions = new Position[] { (new Position(6,0)), (new Position(4,2)), (new Position(10,2)) };
		cityPosition = new Position(6, 1);
		asylumPosition = new Position(3, 1);
		
		//fill ground till transientFields
		for(int i = 0; i < (groundLength - transientFieldsLength); i++)
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
		for(int i = (groundLength - transientFieldsLength); i < groundLength; i++)
		{
			fields[i] = "sp";
		}
	}
	
	private void LevelThreeFieldSetup()
	{
		hideoutType = "cve";
		hideoutPositions = new Position[] { (new Position(6,0)), (new Position(4,2)), (new Position(10,2)) };
		cityPosition = new Position(6, 1);
		asylumPosition = new Position(3, 1);
		
		//fill ground
		for(int i = 0; i < groundLength; i++)
		{
			//level 3 - phenicia - consists of steppe only
			fields[i] = "sp";
		}
	}
	
	private void LevelFourFieldSetup()
	{
		hideoutType = "frs";
		hideoutPositions = new Position[] { (new Position(6,0)), (new Position(4,2)), (new Position(10,2)) };
		cityPosition = new Position(6, 1);
		asylumPosition = new Position(3, 1);
		
		for(int i = 0; i < transientFieldsLength; i++)
		{
			fields[i] = "sp";
		}
		for(int i = transientFieldsLength; i < groundLength; i++)
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
		for(int i = groundLength; i < boardLength; i++)
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