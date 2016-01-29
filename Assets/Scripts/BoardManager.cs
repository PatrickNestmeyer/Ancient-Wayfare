using UnityEngine;
using System;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour 
{    
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
	private CoordinateSystem cs;
    private GlobalSettings GS;
    private Level level;
    
	private void BoardSetup()
	{	
		int columns;
		float X_Position;
		float Y_Position;
		bool isShortRow = true;
		
		for(int i = 0; i < cs.Positions.Length; i++)
		{ cs.Positions[i].Clear(); }
		
		boardHolder = new GameObject ("Board").transform;
		
		for(int x = GS.loopOffset; x < (GS.rows + GS.loopOffset); x++)
		{
			columns = (isShortRow) ? GS.shortColumn : GS.longColumn;
			for(int y = GS.loopOffset; y < (columns + GS.loopOffset); y++)
			{
				GameObject toInstantiate = null;
				
				X_Position = Convert.ToSingle(x) * GS.positionFactorX;
				Y_Position = Convert.ToSingle(y) * GS.positionFactorY;
				if(isShortRow)
					Y_Position += GS.shortPositionOffset;
				
				cs.Positions[x - GS.loopOffset].Add(new Vector3(X_Position, Y_Position, 0f));
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

	public void SetupScene(int currentLevel)
	{
        level = Level.Instance;
        cs = CoordinateSystem.Instance;
        GS = GlobalSettings.Instance;
		BoardSetup();
		PlacesSetup();
	}
    
    public void killScene()
    {
        /*
        var children = new List<GameObject>();
        foreach (Transform child in boardHolder) children.Add(child.gameObject);
        children.ForEach( Destroy(child);
        */
        /*
        int childs = boardHolder.childCount;
        for (int i = childs - 1; i > 0; i--)
        {
            GameObject.Destroy(boardHolder.GetChild(i).gameObject);
        }*/
        
        GameObject.Destroy(boardHolder.gameObject);
    }
}