using UnityEngine;
using System.Collections;

public class Resources 
{
    private static Resources instance;
    private string equipment;
    private int gold;
    private int fighters;
    private int food;
    private float attackFactor;
    private float defenseFactor;
    private GlobalSettings GS;
    
    public static Resources Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new Resources();
            }
            return instance;
        }
    }
    
    public string Equipment
    {
        get{return equipment;}
    }
    public int Gold
    {
        get{return gold;}
        set{gold = value;}
    }
    public int Fighters
    {
        get{return fighters;}
        set{fighters = value;}
    }
    public int Food
    {
        get{return food;}
        set{food = value;}
    }
    public float AttackFactor
    {
        get{return attackFactor;}
    }
    public float DefenseFactor
    {
        get{return defenseFactor;}
    }
    
    public void setEquipment(string equipment)
    {
        switch(equipment)
        {
            case "light":
                this.equipment = "light";
                attackFactor = GS.attackFactorLight;
                defenseFactor = GS.defenseFactorLight;
                break;
            case "medium":
                this.equipment = "medium";
                attackFactor = GS.attackFactorMedium;
                defenseFactor = GS.defenseFactorMedium;
                break;
           case "heavy":
                this.equipment = "medium";
                attackFactor = GS.attackFactorHeavy;
                defenseFactor = GS.defenseFactorHeavy;
                break;
           case "superior":
                this.equipment = "superior";
                attackFactor = GS.attackFactorSuperior;
                defenseFactor = GS.defenseFactorSuperior;
                break;
        }
    }
    
    public Resources()
    {
        GS = GlobalSettings.Instance;
        setEquipment(GS.startingEquipment);
        gold = GS.startingGold;
        fighters = GS.startingFighters;
        food = GS.startingFood;
    }
}