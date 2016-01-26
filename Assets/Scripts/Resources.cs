using UnityEngine;
using System.Collections;

public class Resources 
{
    private string equipment;
    private int gold;
    private int fighters;
    private int food;
    private float attackFactor;
    private float defenseFactor;
    private GlobalSettings GS;
    
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
    
    public Resources(string equipment, int gold, int fighters, int food)
    {
        GS = GlobalSettings.Instance;
        setEquipment(equipment);
        this.gold = gold;
        this.fighters = fighters;
        this.food = food;
    }
}