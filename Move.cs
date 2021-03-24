using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
//storage class for Move array
public class Move
{
    [SerializeField]
    private string name;
   // [SerializeField]
   // private string description;
    [SerializeField]
    private string type;
    [SerializeField]
    private int power;
    [SerializeField]
    private int accuracy;
    //private int numOfMoves;
    //[SerializeField]
    //private int proficiencyCost;
    [SerializeField]
    private bool isStatusEffect;
    [SerializeField]
    private int statusEffectValue;
    [SerializeField]
    private int effectChance;
    [SerializeField]
    private bool priorityMove;
    [SerializeField]
    private bool specialMove;
    [SerializeField]
    private bool physicalMove;
    [SerializeField]
    private bool statChanger;
    [SerializeField]
    private int statValue;
    [SerializeField]
    private int statChance;
    [SerializeField]
    private int stageChanger;
    [SerializeField]
    private bool selfInflict;
    
    //Accessor Methods

    public bool returnIsStatusEffect()
    {
        return isStatusEffect;
    }

    public int returnPower(){
    return power;
    }

    public int returnAccuracy(){
        return accuracy;
    }

    public string returnName()
    {
        return name;
    }

    public string returnType()
    {
        return type;
    }

    public bool isPriority()
    {
        return priorityMove;
    }

    public bool isSpecialMove()
    {
        return specialMove;
    }

    public int returnStatusValue()
    {
        return statusEffectValue;
    }

    public int returnEffectChance()
    {
        return effectChance;
    }

    public bool isPhysicalMove()
    {
        return physicalMove;
    }

    public bool isStatChanger()
    {
        return statChanger;
    }

    public int returnStatValue()
    {
        return statValue;
    }

    public int returnStatChance()
    {
        return statChance;
    }

    public int returnStageChanger()
    {
        return stageChanger;
    }

    public bool isAffectsSelf()
    {
        return selfInflict;
    }

}


