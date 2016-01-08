using UnityEngine;
using System.Collections;

public class Position {

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
