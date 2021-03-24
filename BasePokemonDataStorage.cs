using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class BasePokemonDataStorage
{
    public string name;
    public Stat maxHealth;
    public Stat currentHealth;
    public Stat attack;
    public Stat defense;
    public Stat spAttack;
    public Stat spDefense;
    public Stat speed;
    public string type;
    public string type2;
    public int baseExperienceYield;

    public bool fainted;

    public bool[] conditions;

    public int level;

    public int currentExp;
    public int experienceToNextLevel;
    public int evolveLevel;
}
