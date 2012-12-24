using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Spell
/// </summary>
public class Spell
{
    public string strSpellName;
    public int intCaster;
    public int intTarget;
    public double intChance;
    public string strDescription;
    public int intTargetPredetermined;

	public Spell()
	{
		//
		// TODO: Add constructor logic here
		//
        this.strSpellName = "";
        this.intCaster = 0;
        this.intTarget = 0;
        this.intTarget = 0;
        this.strDescription = "";
        this.intTargetPredetermined = 1;
	}

    public static List<Spell> getSpells()
    {
        List<Spell> splLstAll = mysql.getSpells();
        return splLstAll;
    }

    public static List<string> getSpellNames()
    {
        List<string> strLstAllSpells = new List<string>();
        List<Spell> splLstAll = mysql.getSpells();
        for (int i = 0; i < splLstAll.Count; i++)
        {
            strLstAllSpells.Add(splLstAll[i].strSpellName);
        }
        return strLstAllSpells;
    }
    /// <summary>
    /// 返回是否是特定目标
    /// </summary>
    /// <param name="strSpellName"></param>
    /// <returns></returns>
    public static int isSpellTargetFixed(string strSpellName, List<Spell> splLstAll)
    {
        for (int i = 0; i < splLstAll.Count; i++)
        {
            if (strSpellName == splLstAll[i].strSpellName)
            {
                return splLstAll[i].intTargetPredetermined;
            }
        }
        return 0;
    }

    public static double getChance(string strSpellName, List<Spell> splLstAll)
    {
        for (int i = 0; i < splLstAll.Count; i++)
        {
            if (strSpellName == splLstAll[i].strSpellName)
            {
                return splLstAll[i].intChance;
            }
        }
        return 0;
    }

    public static bool canCast(int intSourceIndex, int intTargetIndex, int intSpellIndex, Status statCurrent)
    {
        string strSource = Convert.ToString(statCurrent.chaLstCharacter[intSourceIndex].intRole, 2).PadLeft(9, '0');
        string strSourceAllowed = Convert.ToString(statCurrent.splLstSpell[intSpellIndex].intCaster, 2).PadLeft(9, '0');
        string strTarget = "";
        if (intTargetIndex != -1)
        {
            strTarget = Convert.ToString(statCurrent.chaLstCharacter[intTargetIndex].intRole, 2).PadLeft(9, '0');
        }
        string strTargetAllowed = Convert.ToString(statCurrent.splLstSpell[intSpellIndex].intTarget, 2).PadLeft(9, '0');
        if (intSourceIndex == intTargetIndex && intTargetIndex != -1)
        {
            if (strTargetAllowed[1] == '0')
            {
                return false;
            }
        }
        if (intSourceIndex != intTargetIndex && intTargetIndex != -1)
        {
            if (strTargetAllowed[2] == '0')
            {
                return false;
            }
        }
        int intIndex = strSource.IndexOf('1');
        if (intIndex >= 0)
        {
            if (strSourceAllowed[intIndex] == '0')
            {
                return false;
            }
        }
        if (intTargetIndex != -1)
        {
            intIndex = strTarget.IndexOf('1');
            if (intIndex >= 0)
            {
                if (strTargetAllowed[intIndex] == '0')
                {
                    return false;
                }
            }
        }
        return true;
    }
}