﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Characters
/// </summary>
public class Character
{
    static int Werewolf = 8;
    static int Priest = 4;
    static int Hunter = 2;
    static int Villager = 1;
    static int Judge = 16;
    static int Monster = 32;

    public string strCharacterName;
    public int intInitialRole;
    public int intRole;
    public int intDebuffCount;
    public int intDebuff1;
    public int intDebuff2;
    public int intDebuff3;
    public int intDebuff1Counter;
    public int intDebuff2Counter;
    public int intDebuff3Counter;
    public double dbDevourResist;
    public double dbInfestResist;
    public double dbDevourBuff;
    public double dbInfestBuff;
    public double dbSilverBulletResist;
    public int intVisitLimit;
    public int intHolyWaterLimit;
    public int intDeathCause;

	public Character()
	{
		//
		// TODO: Add constructor logic here
		//
        this.strCharacterName = "";
        this.intInitialRole = 0;
        this.intRole = 0;
        this.intDebuffCount = 0;
        this.intDebuff1 = 0;
        this.intDebuff1Counter = 0;
        this.intDebuff2 = 0;
        this.intDebuff2Counter = 0;
        this.intDebuff3 = 0;
        this.intDebuff3Counter = 0;
        this.dbDevourResist = 0.0;
        this.dbInfestResist = 0.0;
        this.dbDevourBuff = 0.0;
        this.dbInfestBuff = 0.0;
        this.dbSilverBulletResist = 0.0;
        this.intHolyWaterLimit = 0;
        this.intVisitLimit = 0;
        this.intDeathCause = 0;
	}

    public static int findIndexForCharacter(string strCharacterName, List<Character> chaLstCharaters)
    {
        for (int i = 0; i < chaLstCharaters.Count; i++)
        {
            if (strCharacterName == chaLstCharaters[i].strCharacterName)
            {
                return i;
            }
        }
        return -1;
    }

    public static Character setCured(Character cha)
    {
        if (cha.intDebuff1 == 1)
        {
            cha.intDebuffCount--;
            cha.intDebuff1 = 0;
            cha.intDebuff1Counter = 0;
        }
        return cha;
    }

    public static Character setConfused(Character cha)
    {
        if (cha.intDebuff2 == 0 && cha.intRole == Werewolf)
        {
            cha.intDebuffCount++;
            cha.intDebuff2 = 4;
            cha.intDebuff2Counter++;
        }
        return cha;
    }

    public static Character setBlessed(Character cha)
    {
        if (cha.intRole != Werewolf && cha.intDebuff1 != 1)
        {
            cha.intDebuff3 = 5;
            cha.intDebuff3Counter = 2;
        }
        return cha;
    }
    
    public static Character setDead(Character cha, int intDeathCau)
    {
        cha.intDebuff1 = -1;
        if (cha.intDebuff2 != 2)
        {
            cha.intDebuff2 = 0;
        }
        cha.intDebuff3 = 0;
        cha.intDebuff1Counter = 0;
        cha.intDebuff2Counter = 0;
        cha.intDebuff3Counter = 0;
        cha.intDebuffCount = 1;
        cha.intDeathCause = intDeathCau;
        return cha;
    }

    public static Character setDisguard(Character cha)
    {
        if (cha.dbDevourResist == 0.3)
        {
            cha.dbDevourResist = 0;
            cha.dbInfestResist = 0;
        }
        return cha;
    }

    public static Character setHunter(Character cha)
    {
        if (cha.intRole == 1 && cha.intDebuff1 == 0)
        {
            cha.intRole = Hunter;
            cha.intDebuff2 = 2;
        }
        return cha;
    }

    public static Character setInfested(Character cha)
    {
        if (cha.intDebuff1 == 0)
        {
            cha.intDebuffCount++;
        }
        cha.intDebuff1 = 1;
        Random rand = new Random(Utilities.GetRandomSeed());
        cha.intDebuff1Counter = rand.Next(2) + 2;
        return cha;
    }

    public static int findRoleForCharacter(string strCharacterName, List<Character> chaLstCharaters)
    {
        for (int i = 0; i < chaLstCharaters.Count; i++)
        {
            if (strCharacterName == chaLstCharaters[i].strCharacterName)
            {
                return chaLstCharaters[i].intRole;
            }
        }
        return -1;
    }

    public static int clueAbout(List<Character> chaLstCharaters, Random rand)
    {
        int j = 0;
        for (int i = 0; i < chaLstCharaters.Count; i++)
        {
            if (chaLstCharaters[i].intDebuff1 != -1)
            {
                j++;
            }
        }
        j = rand.Next(j);
        int k = 0;
        for (int i = 0; i < chaLstCharaters.Count; i++)
        {
            if (chaLstCharaters[i].intDebuff1 != -1)
            {
                if (k == j)
                {
                    return i;
                }
                else
                {
                    k++;
                }
            }
        }
        return -1;
    }
}