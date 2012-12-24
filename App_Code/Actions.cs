using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for Actions
/// </summary>
public class Actions
{
    public string strCharacterName;
    public string strSpellName;
    public int intCharacter;                //动作者(actor)在list<character>的编号
    public int intDestination;              //目标(target)在list<character>的编号
    public int intTargetAvailable;          //后来有可能成为target的list<character>的编号，-1表示任何人（用于守护等）
    public int intResult;                   //-1表失败 0表未使用 其他表成功

	public Actions()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static List<Actions> initialization(List<string> strLstActions, Status thisStat)
    {
        List<Character> chaLstCharacters = thisStat.chaLstCharacter;
        List<Spell> splLstAll = thisStat.splLstSpell;
        List<Actions> actLstResult = new List<Actions>();
        Regex rgSource = new Regex("{.*}");
        Regex rgSpell = new Regex("\\[\\[.*\\]\\]");
        Regex rgDestination = new Regex("->.*<-");
        string strTmp = "";
        for (int i = 0; i < strLstActions.Count; i++)
        {
            Actions actionItem = new Actions();
            strTmp = rgSource.Match(strLstActions[i]).ToString();
            strTmp = strTmp.Replace("{", "");
            actionItem.strCharacterName = strTmp.Replace("}", "");
            strTmp = rgSpell.Match(strLstActions[i]).ToString();
            strTmp = strTmp.Replace("[[", "");
            actionItem.strSpellName = strTmp.Replace("]]", "");
            strTmp = rgDestination.Match(strLstActions[i]).ToString();
            strTmp = strTmp.Replace("->", "");
            actionItem.intCharacter = Character.findIndexForCharacter(actionItem.strCharacterName, chaLstCharacters);
            strTmp = strTmp.Replace("<-", "");
            actionItem.intDestination = Character.findIndexForCharacter(strTmp, chaLstCharacters);
            actionItem.intResult = 0;
            actionItem.intTargetAvailable = Spell.isSpellTargetFixed(actionItem.strSpellName, splLstAll);
            actLstResult.Add(actionItem);
        }
        return actLstResult;
    }
}