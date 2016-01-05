using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour {

	public int rows = 12;
	public int shortColumn = 2;
	public int longColumn = 3;
	
	public GameObject Army;
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
	
	void BoardSetup()
	{
		Positions.Clear();
		
		for(int x = 0; x < rows; x++)
		{
			int columns = (x % 2 == 0) ? shortColumn : longColumn;
			for(int y = 1; y < columns; y++)
			{
				Positions.Add(new Vector3(x,y,0f));
				GameObject toInstantiate = 
			}
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}