using UnityEngine;
using System;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour 
{    
	public int rows = 13;
	public int shortColumn = 3;
	public int longColumn = 4;
	public float positionFactorX = 0.8f;
	public float positionFactorY = 2.6f;
	public float shortPositionOffset = 1.2f;
	public int loopOffset = -1;
	public Level level = new Level();
    
    public CoordinateSystem cs;
    
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
		
		for(int i = 0; i < cs.Positions.Length; i++)
		{ cs.Positions[i].Clear(); }
		
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
				
				cs.Positions[x - loopOffset].Add(new Vector3(X_Position, Y_Position, 0f));
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
	   float X_Position = cs.Positions[rowIndex][columnIndex].x;
	   float Y_Position = cs.Positions[rowIndex][columnIndex].y;
       
       GameObject instance = Instantiate(toInsert, new Vector3(X_Position, Y_Position, 0f), Quaternion.identity) as GameObject;
	   instance.transform.SetParent(boardHolder);
    }

	public void SetupScene(int level)
	{
        cs = CoordinateSystem.Instance;
		this.level.createLevel(level);
		BoardSetup();
		PlacesSetup();
	}
}