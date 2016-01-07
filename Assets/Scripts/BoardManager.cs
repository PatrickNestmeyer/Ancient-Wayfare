using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour {

	public int rows = 13;
	public int shortColumn = 3;
	public int longColumn = 4;
	public float positionFactorX = 0.8f;
	public float positionFactorY = 2.6f;
	public float shortPositionOffset = 1.2f;
	public int loopOffset = -1;
	public Level level = new Level();
	
	/*
	 *Array of all rows as Vector3, via this array acess to a vector which index is known is possible.
	 *Further via the vector the exact position can be asked.
	 */
	public List<Vector3>[] Positions = new List<Vector3>[13];
	
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
	
	private void BoardSetup()
	{	
		int columns;
		float X_Position;
		float Y_Position;
		bool isShortRow = true;
		
		for(int i = 0; i < Positions.Length; i++)
		{ Positions[i].Clear(); }
		
		boardHolder = new GameObject ("Board").transform;
		
		for(int x = loopOffset; x < (rows + loopOffset); x++)
		{
			columns = (isShortRow) ? shortColumn : longColumn;
			for(int y = loopOffset; y < (columns + loopOffset); y++)
			{
				GameObject toInstantiate = null;
				
				X_Position = Convert.ToSingle(x) * positionFactorX;
				Y_Position = Convert.ToSingle(y) * positionFactorY;
				if(isShortRow)
					Y_Position += shortPositionOffset;
				
				Positions[x - loopOffset].Add(new Vector3(X_Position, Y_Position, 0f));
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
				GameObject instance = Instantiate(toInstantiate, new Vector3(X_Position, Y_Position, 0f), Quaternion.identity) as GameObject;
				instance.transform.SetParent(boardHolder);
				level.Index++;
			}
			isShortRow = !isShortRow;
		}
	}
	
	private void PlacesSetup()
	{
        inserObjectIntoBoard(level.asylumPosition.Row, level.asylumPosition.Column, Asylum);
        inserObjectIntoBoard(level.cityPosition.Row, level.cityPosition.Column, City);
		for(int i = 0; i < level.hideoutPositions.Length; i++)
		{ 
            GameObject toInstantiate = forestHideout;
            if(level.hideoutType == "swp")
                toInstantiate = swampHideout;
            if(level.hideoutType == "cve")
                toInstantiate = caveHideout;
            inserObjectIntoBoard(level.hideoutPositions[i].Row, level.hideoutPositions[i].Column, toInstantiate);
        }
	}
    
    private void inserObjectIntoBoard(int rowIndex, int columnIndex, GameObject toInsert)
    {
       //The position of the drawn field as float
	   float X_Position = Positions[rowIndex][columnIndex].x;
	   float Y_Position = Positions[rowIndex][columnIndex].y;
       
       GameObject instance = Instantiate(toInsert, new Vector3(X_Position, Y_Position, 0f), Quaternion.identity) as GameObject;
	   instance.transform.SetParent(boardHolder);
    }

	public void SetupScene(int level)
	{
		this.level.createLevel(level);
		for(int i = 0; i < Positions.Length; i++)
		{ Positions[i] = new List<Vector3>(); }
		BoardSetup();
		PlacesSetup();
	}
}

public class Position
{
	private int row;
	private int column;
	
	public int Row
	{
		get{ return row; }
		set{ row = value; }	
	}
	public int Column
	{
		get{ return column; }
		set{ column = value; }
	}
	
	public Position(int row, int column)
	{
		this.row = row;
		this.column = column;
	}
}

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