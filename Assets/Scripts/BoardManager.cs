using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour {

	public int rows = 12;
	public int shortColumn = 2;
	public int longColumn = 3;
	public float positionFactorX = 1.62f;
	public float positionFactorY = 3.0f;
	
	public GameObject Asylum;
	public GameObject City;
	public GameObject caveHideout;
	public GameObject forestHideout;
	public GameObject swampHideout;
	public GameObject Agriculture;
	public GameObject Woodland;
	public GameObject Grassland;
	public GameObject Steppe;
	public GameObject Sea;
	public GameObject Mountain;
	public GameObject Desert;
	
	private Transform boardHolder;
	private List <Vector3> Positions = new List<Vector3>();
	private Level level = new Level();
	
	private void BoardSetup()
	{
		Positions.Clear();
		boardHolder = new GameObject ("Board").transform;
		
		for(int x = -2; x <= rows -2; x++)
		{
			int columns = (x % 2 == 0) ? shortColumn : longColumn;	
			for(int y = -2; y <= columns -2; y++)
			{
				GameObject toInstantiate = null;
				Positions.Add(new Vector3((Convert.ToSingle(x) * positionFactorX), (Convert.ToSingle(y) * positionFactorY), 0f));
				switch (level[level.Index])
				{
					case "gl":
						toInstantiate = Grassland;
						break;
					case "ac":
						toInstantiate = Agriculture;
						break;
					case "wl":
						toInstantiate = Woodland;
						break;
					case "sp":
						toInstantiate = Steppe;
						break;
					case "sea":
						toInstantiate = Sea;
						break;
					case "mtn":
						toInstantiate = Mountain;
						break;
					case "dsr":
						toInstantiate = Desert;
						break;
					default:
						break;
				}
				GameObject instance = Instantiate(toInstantiate, new Vector3((Convert.ToSingle(x) * positionFactorX), (Convert.ToSingle(y) * positionFactorY),0f), Quaternion.identity) as GameObject;
				instance.transform.SetParent(boardHolder);
				level.Index++;
			}
		}
	}

	public void SetupScene(int level)
	{
		this.level.createLevel(level);
		BoardSetup();
	}	
}

public class Level
{
	public string[] fields = new string[45];
	//Sum of all fields
	public int boardLength = 45;
	//Walkable Fields
	public int groundLength = 38;
	//Fields only walkable with key
	public int keyGroundLength = 7;
	//transient fields before or after a keyground
	public int transientFieldsLength = 7;
	
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
		//fill ground till transientFields
		for(int i = 0; i < (groundLength - transientFieldsLength); i++)
		{
			//level 2 - egypt - consists of grassland, agriculture and woodland till transientFields ( 3 - 2 - 1)
			rnd = Random.Range(0,6);
			if(rnd < 3)
				fields[i] = "gl";
			if(rnd < 5)
				fields[i] = "ac";
			if(rnd == 5)
				fields[i] = "wl";
		}
		//fill ground from transientFields till keyGround with steppe
		for(int i = (groundLength - transientFieldsLength); i < keyGroundLength; i++)
		{
			fields[i] = "sp";
		}
	}
	
	private void LevelThreeFieldSetup()
	{
		//fill ground
		for(int i = 0; i < groundLength; i++)
		{
			//level 3 - phenicia - consists of steppe only
			fields[i] = "sp";
		}
	}
	
	private void LevelFourFieldSetup()
	{
		for(int i = 0; i < transientFieldsLength; i++)
		{
			fields[i] = "sp";
		}
		for(int i = transientFieldsLength; i < groundLength; i++)
		{
			//level 4 - mesopotamia - consists of grassland, agriculture and woodland till transientFields ( 3 - 2 - 1)
			rnd = Random.Range(0,6);
			if(rnd < 3)
				fields[i] = "gl";
			if(rnd < 5)
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