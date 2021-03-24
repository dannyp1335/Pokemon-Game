using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;

public class MoveManager : MonoBehaviour
{
    [SerializeField]
    public Move[] moves;

    private int power;
    private int accuracy;

    //why is this even here
    private BattleManager battleManager;

    public string returnMoveName(int move)
    {
        return moves[move].returnName();
    }

    public int returnMovePower(int move)
    {
        return moves[move].returnPower();
    }

    public int returnMoveAccuracy(int move)
    {
        return moves[move].returnAccuracy();
    }

    public string returnMoveType(int move)
    {
        return moves[move].returnType();
    }

    public int returnEffectChance(int move)
    {
        return moves[move].returnEffectChance();
    }

    public bool isStatChanger(int move)
    {
        return moves[move].isStatChanger();
    }
    public bool isPhysical(int move)
    {
        return moves[move].isPhysicalMove();
    }
    public bool isSpecial(int move)
    {
        return moves[move].isSpecialMove();
    }
    public bool isStatusEffect(int move)
    {
        return moves[move].returnIsStatusEffect();
    }
    public bool isAffectsSelf(int move)
    {
        return moves[move].isAffectsSelf();
    }
    public int returnStageChanger(int move)
    {
        return moves[move].returnStageChanger();
    }
    public int returnStatValue(int move)
    {
        return moves[move].returnStatValue();
    }
    public int returnStatusValue(int move)
    {
        return moves[move].returnStatusValue();
    }

}