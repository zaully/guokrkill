using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

/// <summary>
/// Summary description for Status
/// </summary>
public class Status
{
    /// <summary>
    /// -1 死亡
    /// 0 健康
    /// 1 感染        debuff1
    /// 2 悲悯        debuff2
    /// 3 饥饿        debuff2
    /// 4 迷茫        debuff2
    /// </summary>
    public List<Character> chaLstCharacter;
    public List<Spell> splLstSpell;

	public Status()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static int getStringLength(string str)
    {
        if (str.Length == 0)
            return 0;
        ASCIIEncoding ascii = new ASCIIEncoding();
        int tempLen = 0;
        byte[] s = ascii.GetBytes(str);
        for (int i = 0; i < s.Length; i++)
        {
            if ((int)s[i] == 63)
            {
                tempLen += 2;
            }
            else
            {
                tempLen += 1;
            }
        }
        return tempLen;
    }

    public string updateFromStatus()
    {
        string strStatus4All = "角色名\t\t\t身份\t状态" + Environment.NewLine;
        for (int i = 0; i < chaLstCharacter.Count; i++)
        {
            if (getStringLength(chaLstCharacter[i].strCharacterName) > 23)
            {
                strStatus4All += chaLstCharacter[i].strCharacterName;
            }
            else if (getStringLength(chaLstCharacter[i].strCharacterName) > 15)
            {
                strStatus4All += chaLstCharacter[i].strCharacterName + "\t";
            }
            else if (getStringLength(chaLstCharacter[i].strCharacterName) > 7)
            {
                strStatus4All += chaLstCharacter[i].strCharacterName + "\t\t";
            }
            else
            {
                strStatus4All += chaLstCharacter[i].strCharacterName + "\t\t\t";
            }
            switch (chaLstCharacter[i].intRole)
            {
                case 0:
                    break;
                case 1:
                    strStatus4All += "村民\t";
                    break;
                case 2:
                    strStatus4All += "猎人\t";
                    break;
                case 4:
                    strStatus4All += "牧师\t";
                    break;
                case 8:
                    strStatus4All += "狼人\t";
                    break;
                default:
                    break;
            }
            if (chaLstCharacter[i].intDebuffCount != 0)
            {
                switch (chaLstCharacter[i].intDebuff1)
                {
                    case -1:
                        strStatus4All += "死亡";
                        break;
                    case 1:
                        strStatus4All += "感染";
                        break;
                    case 2:
                        strStatus4All += "悲悯";
                        break;
                    case 3:
                        strStatus4All += "饥饿";
                        break;
                    case 4:
                        strStatus4All += "迷茫";
                        break;
                    default:
                        break;
                }
                switch (chaLstCharacter[i].intDebuff2)
                {
                    case 1:
                        strStatus4All += "感染";
                        break;
                    case 2:
                        strStatus4All += "悲悯";
                        break;
                    case 3:
                        strStatus4All += "饥饿";
                        break;
                    case 4:
                        strStatus4All += "迷茫";
                        break;
                    default:
                        break;
                }
                switch (chaLstCharacter[i].intDebuff3)
                {
                    case 1:
                        strStatus4All += "感染";
                        break;
                    case 2:
                        strStatus4All += "悲悯";
                        break;
                    case 3:
                        strStatus4All += "饥饿";
                        break;
                    case 4:
                        strStatus4All += "迷茫";
                        break;
                    default:
                        break;
                }
            }
            else
            {
                strStatus4All += "正常";
            }
            strStatus4All += Environment.NewLine;
        }
        return strStatus4All;
    }
    
    public string updateAll(int intGameNo, int intDayNo)
    {
        string strStatus4All = "角色名\t\t\t身份\t状态" + Environment.NewLine;
        this.chaLstCharacter = mysql.getBatchPlayers(intGameNo, intDayNo, "All");
        this.splLstSpell = mysql.getSpells();
        
        for (int i = 0; i < chaLstCharacter.Count; i++)
        {
            if (getStringLength(chaLstCharacter[i].strCharacterName) > 23)
            {
                strStatus4All += chaLstCharacter[i].strCharacterName;
            }
            else if (getStringLength(chaLstCharacter[i].strCharacterName) > 15)
            {
                strStatus4All += chaLstCharacter[i].strCharacterName + "\t";
            }
            else if (getStringLength(chaLstCharacter[i].strCharacterName) > 7)
            {
                strStatus4All += chaLstCharacter[i].strCharacterName + "\t\t";
            }
            else
            {
                strStatus4All += chaLstCharacter[i].strCharacterName + "\t\t\t";
            }
            switch (chaLstCharacter[i].intRole)
            {
                case 0:
                    break;
                case 1:
                    strStatus4All += "村民\t";
                    break;
                case 2:
                    strStatus4All += "猎人\t";
                    break;
                case 4:
                    strStatus4All += "牧师\t";
                    break;
                case 8:
                    strStatus4All += "狼人\t";
                    break;
                default:
                    break;
            }
            if (chaLstCharacter[i].intDebuffCount != 0)
            {
                switch (chaLstCharacter[i].intDebuff1)
                {
                    case -1:
                        strStatus4All += "死亡";
                        break;
                    case 1:
                        strStatus4All += "感染";
                        break;
                    case 2:
                        strStatus4All += "悲悯";
                        break;
                    case 3:
                        strStatus4All += "饥饿";
                        break;
                    case 4:
                        strStatus4All += "迷茫";
                        break;
                    default:
                        break;
                }
                switch (chaLstCharacter[i].intDebuff2)
                {
                    case 1:
                        strStatus4All += "感染";
                        break;
                    case 2:
                        strStatus4All += "悲悯";
                        break;
                    case 3:
                        strStatus4All += "饥饿";
                        break;
                    case 4:
                        strStatus4All += "迷茫";
                        break;
                    default:
                        break;
                }
                switch (chaLstCharacter[i].intDebuff3)
                {
                    case 1:
                        strStatus4All += "感染";
                        break;
                    case 2:
                        strStatus4All += "悲悯";
                        break;
                    case 3:
                        strStatus4All += "饥饿";
                        break;
                    case 4:
                        strStatus4All += "迷茫";
                        break;
                    default:
                        break;
                }
            }
            else
            {
                strStatus4All += "正常";
            }
            strStatus4All += Environment.NewLine;
        }
        return strStatus4All;
    }

    public static int updateIntoDatabase(int intGameNo, int intDayNo, List<Character> chaLstAll)
    {
        string strSQL = "insert into guokrhunt.Character(GameNo,DayNo,PlayerID,CharacterName,InitialRole,CurrentRole, Status1, Status1Counter, Status2, Status2Counter, Status3, Status3Counter) values ";
        string strTemp = "";
        for (int i = 0; i < chaLstAll.Count; i++)
        {
            if (i > 0)
            {
                strSQL += ", ";
            }
            strTemp = "(" + intGameNo + "," + intDayNo + ",-1,'";
            strTemp += chaLstAll[i].strCharacterName + "'," + chaLstAll[i].intInitialRole + "," + chaLstAll[i].intRole + "," + chaLstAll[i].intDebuff1 + "," + chaLstAll[i].intDebuff1Counter + "," + chaLstAll[i].intDebuff2 + "," + chaLstAll[i].intDebuff2Counter + "," + chaLstAll[i].intDebuff3 + "," + chaLstAll[i].intDebuff3Counter + ")";
            strSQL += strTemp;
        }
        return(mysql.updateBatchPlayers(strSQL));
    }

    public static int updateLogs(int intGameNo, int intDayNo, string strLogs)
    {
        string strSQL = "insert into guokrhunt.GameLog(GameNo,DayNo,LogToday) values (" + intGameNo + "," + intDayNo + ",'" + strLogs + "')";
        return mysql.runSql(strSQL);
    }
}