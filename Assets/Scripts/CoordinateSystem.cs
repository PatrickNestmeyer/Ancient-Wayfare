using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CoordinateSystem
{    
    private static CoordinateSystem instance;
    
    private CoordinateSystem() 
    {
        for(int i = 0; i < Positions.Length; i++)
        {
            Positions[i] = new List<Vector3>();
        }
    }
    
    public List<Vector3>[] Positions = new List<Vector3>[13];
    
	public static CoordinateSystem Instance
    {
        get
        {
            if(instance == null)
                instance = new CoordinateSystem();
            return instance;
        }
    }
}