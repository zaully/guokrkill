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
    /// -1 死亡       debuff1
    /// 0 健康        debuff1
    /// 1 感染        debuff1
    /// 2 传承        debuff2
    /// 3 饥饿        debuff2
    /// 4 茫然        debuff2
    /// 5 神佑        debuff3
    /// </summary>
    public List<Character> chaLstCharacter;
    public List<Spell> splLstSpell;

    static string strVisit = "拜访";
    static string strPurge = "净化";
    static string strPrayer = "祈祷";
    static string strBlessing = "神佑";
    static string strConsecration = "奉献";
    static string strDevourment = "吞噬";
    static string strInfestation = "感染";
    static string strSilverBullet = "银弹";
    static string strCompass = "罗盘";
    static string strHolyWater = "圣水";
    static string strInheritance = "传承";
    static string strGuard = "守护";
    static string strFire = "火刑";
    static string strSpy = "窥探";

    static int Werewolf = 8;
    static int Priest = 4;
    static int Hunter = 2;
    static int Villager = 1;
    static int Judge = 16;
    static int Monster = 32;

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

    public void Inherit(int InheritTo, int intInheritFrom)
    {
        this.chaLstCharacter[InheritTo] = Character.setHunter(this.chaLstCharacter[InheritTo]);
        this.chaLstCharacter[intInheritFrom].intDebuff2 = -1;
        this.chaLstCharacter[InheritTo].intHolyWaterLimit = this.chaLstCharacter[intInheritFrom].intHolyWaterLimit;
    }

    public string updateFromStatus()
    {
        string strStatus4All = "编号\t角色名\t\t\t身份\t状态" + Environment.NewLine;
        for (int i = 0; i < chaLstCharacter.Count; i++)
        {
            strStatus4All += (i + 1).ToString() + "\t";
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
                    if (chaLstCharacter[i].intDebuff2 == 0)
                    {
                        strStatus4All += "原生猎\t";
                    }
                    else if (chaLstCharacter[i].intDebuff2 == 2)
                    {
                        strStatus4All += "传承猎\t";
                    }
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
                        strStatus4All += "茫然";
                        break;
                    default:
                        break;
                }
                switch (chaLstCharacter[i].intDebuff2)
                {
                    case 4:
                        strStatus4All += "茫然";
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
                        strStatus4All += "茫然";
                        break;
                    case 5:
                        strStatus4All += "神佑";
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
        string strStatus4All = "编号\t角色名\t\t\t身份\t状态" + Environment.NewLine;
        this.chaLstCharacter = mysql.getBatchPlayers(intGameNo, intDayNo, "All");
        this.splLstSpell = mysql.getSpells();
        
        for (int i = 0; i < chaLstCharacter.Count; i++)
        {
            strStatus4All += (i + 1).ToString() + "\t";
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
                    if (chaLstCharacter[i].intDebuff2 == 0)
                    {
                        strStatus4All += "原生猎\t";
                    }
                    else if (chaLstCharacter[i].intDebuff2 == 2)
                    {
                        strStatus4All += "传承猎\t";
                    }
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
                        strStatus4All += "茫然";
                        break;
                    default:
                        break;
                }
                switch (chaLstCharacter[i].intDebuff2)
                {
                    case 4:
                        strStatus4All += "茫然";
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
                        strStatus4All += "茫然";
                        break;
                    case 5:
                        strStatus4All += "神佑";
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
        string strSQL = "insert into guokrhunt.Character(GameNo,DayNo,PlayerID,CharacterName,InitialRole,CurrentRole, Status1, Status1Counter, Status2, Status2Counter, Status3, Status3Counter, HolyWaterCount, VisitCount, DeathCause) values ";
        string strTemp = "";
        for (int i = 0; i < chaLstAll.Count; i++)
        {
            if (i > 0)
            {
                strSQL += ", ";
            }
            strTemp = "(" + intGameNo + "," + intDayNo + ",-1,'";
            strTemp += chaLstAll[i].strCharacterName + "'," + chaLstAll[i].intInitialRole + "," + chaLstAll[i].intRole + "," + chaLstAll[i].intDebuff1 + "," + chaLstAll[i].intDebuff1Counter + "," + chaLstAll[i].intDebuff2 + "," + chaLstAll[i].intDebuff2Counter + "," + chaLstAll[i].intDebuff3 + "," + chaLstAll[i].intDebuff3Counter + "," + chaLstAll[i].intHolyWaterLimit + "," + chaLstAll[i].intVisitLimit + "," + chaLstAll[i].intDeathCause + ")";
            strSQL += strTemp;
        }
        return(mysql.updateBatchPlayers(strSQL));
    }

    public static int updateLogs(int intGameNo, int intDayNo, string strLogs)
    {
        string strSQL = "insert into guokrhunt.GameLog(GameNo,DayNo,LogToday) values (" + intGameNo + "," + intDayNo + ",'" + strLogs + "')";
        return mysql.runSql(strSQL);
    }

    public void RenewBuff()
    {
        for (int i = 0; i < this.chaLstCharacter.Count; i++)
        {
            if (chaLstCharacter[i].intDebuff3 == 5 && chaLstCharacter[i].intRole != Werewolf)
            {
                chaLstCharacter[i].dbDevourResist = 0.5;
                chaLstCharacter[i].dbInfestResist = 1.0;
            }
        }
    }

    public string CalculateResult(List<Actions> actLstActions)
    {
        string strResult = "";
        Random rand = new Random(Utilities.GetRandomSeed());
        this.RenewBuff();

        //第一轮，处理牧师特有技能
        for (int i = 0; i < actLstActions.Count; i++)
        {
            if (actLstActions[i].intResult == 0)    //若技能未执行
            {
                if (Character.findRoleForCharacter(actLstActions[i].strCharacterName, this.chaLstCharacter) == Priest)      //若是牧师
                {
                    //净化
                    if (actLstActions[i].strSpellName == strPurge)
                    {
                        if (rand.NextDouble() <= Spell.getChance(strPurge, this.splLstSpell) && this.chaLstCharacter[actLstActions[i].intDestination].intDebuff1 == 1)
                        {
                            this.chaLstCharacter[actLstActions[i].intDestination] = Character.setCured(this.chaLstCharacter[actLstActions[i].intDestination]);
                            strResult += "【" + actLstActions[i].strCharacterName + "】的净化成功，【" + this.chaLstCharacter[actLstActions[i].intDestination].strCharacterName + "】的【感染】痊愈了，且【" + this.chaLstCharacter[actLstActions[i].intDestination].strCharacterName + "】得知了牧师的身份" + Environment.NewLine;
                            actLstActions[i].intResult = 1;
                        }
                        else if (this.chaLstCharacter[actLstActions[i].intDestination].intDebuff1 != -1)
                        {
                            strResult += "【" + actLstActions[i].strCharacterName + "】施放了净化，但【" + this.chaLstCharacter[actLstActions[i].intDestination].strCharacterName + "】本来就很健康。且【" + this.chaLstCharacter[actLstActions[i].intDestination].strCharacterName + "】得知了牧师的身份" + Environment.NewLine;
                            actLstActions[i].intResult = -1;
                        }
                        else
                        {
                            strResult += "【" + actLstActions[i].strCharacterName + "】的净化失败" + Environment.NewLine;
                            actLstActions[i].intResult = -1;
                        }
                    }
                    //祈祷
                    else if (actLstActions[i].strSpellName == strPrayer)
                    {
                        if (rand.NextDouble() <= Spell.getChance(strPrayer, this.splLstSpell))
                        {
                            this.chaLstCharacter[actLstActions[i].intDestination].dbDevourResist = 1;
                            this.chaLstCharacter[actLstActions[i].intDestination].dbInfestResist = 1;
                            strResult += "【" + actLstActions[i].strCharacterName + "】正在祈祷，获得了神的庇护" + Environment.NewLine;
                            actLstActions[i].intResult = 1;
                        }
                        else
                        {
                            strResult += "【" + actLstActions[i].strCharacterName + "】正在祈祷，但神没有回应" + Environment.NewLine;
                            actLstActions[i].intResult = -1;
                        }
                    }
                    //奉献
                    else if (actLstActions[i].strSpellName == strConsecration)
                    {
                        if (rand.NextDouble() <= Spell.getChance(strConsecration, this.splLstSpell))
                        {
                            strResult += "【" + actLstActions[i].strCharacterName + "】奉献啦！！！" + Environment.NewLine;
                            if (this.chaLstCharacter[actLstActions[i].intCharacter].intDebuff1 != 1)
                            {
                                strResult += "所有人的感染被治愈！" + Environment.NewLine;
                                for (int j = 0; j < this.chaLstCharacter.Count; j++)
                                {
                                    this.chaLstCharacter[j] = Character.setCured(this.chaLstCharacter[j]);
                                }
                            }
                            else
                            {
                                for (int j = 0; j < this.chaLstCharacter.Count; j++)
                                {
                                    this.chaLstCharacter[j] = Character.setConfused(this.chaLstCharacter[j]);
                                }
                                strResult += "狼人因【" + this.chaLstCharacter[actLstActions[i].intCharacter] + "】的奉献陷入茫然状态一天。" + Environment.NewLine;
                            }
                            this.chaLstCharacter[actLstActions[i].intCharacter] = Character.setDead(this.chaLstCharacter[actLstActions[i].intCharacter], 4);
                            actLstActions[i].intResult = 1;
                        }
                        else
                        {
                            strResult += "【" + actLstActions[i].strCharacterName + "】奉献失败，连神也不再眷顾他了吗？" + Environment.NewLine;
                            actLstActions[i].intResult = -1;
                        }
                    }
                    //神佑
                    else if (actLstActions[i].strSpellName == strBlessing)
                    {
                        if (rand.NextDouble() <= Spell.getChance(strBlessing, this.splLstSpell))
                        {
                            this.chaLstCharacter[actLstActions[i].intDestination].dbDevourResist = 0.5;
                            this.chaLstCharacter[actLstActions[i].intDestination].dbInfestResist = 1;
                            this.chaLstCharacter[actLstActions[i].intDestination] = Character.setBlessed(this.chaLstCharacter[actLstActions[i].intDestination]);
                            strResult += "【" + actLstActions[i].strCharacterName + "】施放了神佑，狼人对【" + this.chaLstCharacter[actLstActions[i].intDestination].strCharacterName + "】的攻击被削弱了。" + Environment.NewLine;
                            actLstActions[i].intResult = 1;
                        }
                        else
                        {
                            strResult += "【" + actLstActions[i].strCharacterName + "】的神佑木有效果。" + Environment.NewLine;
                            actLstActions[i].intResult = -1;
                        }
                    }
                }
            }
        }
        //第二轮，处理猎人守护的buff部分
        for (int i = 0; i < actLstActions.Count; i++)
        {
            if (actLstActions[i].intResult == 0)
            {
                if (actLstActions[i].strSpellName == strGuard)
                {
                    if (rand.NextDouble() <= Spell.getChance(strGuard, this.splLstSpell))
                    {
                        strResult += "【" + actLstActions[i].strCharacterName + "】施放了守护，【" + this.chaLstCharacter[actLstActions[i].intDestination].strCharacterName + "】被守护了。" + Environment.NewLine;
                        if (this.chaLstCharacter[actLstActions[i].intDestination].dbInfestResist != 1.0)
                        {
                            this.chaLstCharacter[actLstActions[i].intDestination].dbInfestResist = 0.3;
                            this.chaLstCharacter[actLstActions[i].intDestination].dbDevourResist = 0.3;
                        }
                        actLstActions[i].intResult = 1;
                    }
                    else
                    {
                        strResult += "【" + actLstActions[i].strCharacterName + "】施放了守护，但没能找到【" + this.chaLstCharacter[actLstActions[i].intDestination].strCharacterName + "】的家。" + Environment.NewLine;
                        actLstActions[i].intResult = -1;
                    }
                }
            }
        }
        //第三轮，处理妖怪技能

        //第四轮，处理狼人技能
        for (int i = 0; i < actLstActions.Count; i++)
        {
            if (actLstActions[i].intResult == 0)
            {
                if (Character.findRoleForCharacter(actLstActions[i].strCharacterName, this.chaLstCharacter) == Werewolf)
                {
                    //处理挡牙
                    int intIsTransferred = 0;
                    for (int j = 0; j < actLstActions.Count; j++)
                    {
                        if (actLstActions[j].strSpellName == strVisit && actLstActions[j].intDestination == actLstActions[i].intDestination)
                        {
                            if (rand.NextDouble() <= 0.5)
                            {
                                actLstActions[i].intDestination = actLstActions[j].intCharacter;
                                intIsTransferred = 1;
                                actLstActions[i].strSpellName = strDevourment;
                            }
                        }
                    }
                    //吞噬
                    if (actLstActions[i].strSpellName == strDevourment)
                    {
                        if ((rand.NextDouble() <= (Spell.getChance(strDevourment, this.splLstSpell) - this.chaLstCharacter[actLstActions[i].intDestination].dbDevourResist + this.chaLstCharacter[actLstActions[i].intCharacter].dbDevourBuff)) && this.chaLstCharacter[actLstActions[i].intDestination].intDebuff1 != -1)
                        {
                            this.chaLstCharacter[actLstActions[i].intDestination] = Character.setDead(this.chaLstCharacter[actLstActions[i].intDestination], 1);
                            strResult += "【" + actLstActions[i].strCharacterName + "】施放了吞噬，【" + this.chaLstCharacter[actLstActions[i].intDestination].strCharacterName + "】死亡。" + Environment.NewLine;
                            actLstActions[i].intResult = 1;
                        }
                        else if (this.chaLstCharacter[actLstActions[i].intDestination].intDebuff1 == -1)
                        {
                            strResult += "【" + actLstActions[i].strCharacterName + "】施放了吞噬，但【" + this.chaLstCharacter[actLstActions[i].intDestination].strCharacterName + "】早已死亡。" + Environment.NewLine;
                            actLstActions[i].intResult = -1;
                        }
                        else if (this.chaLstCharacter[actLstActions[i].intDestination].intDebuff3 == 5 || this.chaLstCharacter[actLstActions[i].intDestination].dbDevourResist == 1.0)
                        {
                            strResult += "【" + actLstActions[i].strCharacterName + "】施放了吞噬，不过【" + this.chaLstCharacter[actLstActions[i].intDestination].strCharacterName + "】因为有牧师的帮助，幸运地逃过了一劫。" + Environment.NewLine;
                            actLstActions[i].intResult = -1;
                        }
                        else
                        {
                            strResult += "【" + actLstActions[i].strCharacterName + "】施放了吞噬，不过【" + this.chaLstCharacter[actLstActions[i].intDestination].strCharacterName + "】幸运地逃过了一劫。" + Environment.NewLine;
                            actLstActions[i].intResult = -1;
                        }
                    }
                    //感染
                    if (actLstActions[i].strSpellName == strInfestation)
                    {
                        if (rand.NextDouble() <= Spell.getChance(strInfestation, this.splLstSpell) + this.chaLstCharacter[actLstActions[i].intCharacter].dbInfestBuff - this.chaLstCharacter[actLstActions[i].intDestination].dbInfestResist && this.chaLstCharacter[actLstActions[i].intDestination].intDebuff1 != -1 && this.chaLstCharacter[actLstActions[i].intDestination].intDebuff1 != 1)
                        {
                            this.chaLstCharacter[actLstActions[i].intDestination] = Character.setInfested(this.chaLstCharacter[actLstActions[i].intDestination]);
                            strResult += "【" + actLstActions[i].strCharacterName + "】施放了感染，【" + this.chaLstCharacter[actLstActions[i].intDestination].strCharacterName + "】现在的状态是【感染】。" + Environment.NewLine;
                            actLstActions[i].intResult = 1;
                        }
                        else if (this.chaLstCharacter[actLstActions[i].intDestination].intDebuff1 == -1)
                        {
                            strResult += "【" + actLstActions[i].strCharacterName + "】施放了感染，但【" + this.chaLstCharacter[actLstActions[i].intDestination].strCharacterName + "】早已死亡。" + Environment.NewLine;
                            actLstActions[i].intResult = -1;
                        }
                        else if (this.chaLstCharacter[actLstActions[i].intDestination].intDebuff1 == 1)
                        {
                            strResult += "【" + actLstActions[i].strCharacterName + "】施放了感染，但【" + this.chaLstCharacter[actLstActions[i].intDestination].strCharacterName + "】早已被感染了。" + Environment.NewLine;
                            actLstActions[i].intResult = -1;
                        }
                        else if (this.chaLstCharacter[actLstActions[i].intDestination].dbInfestResist == 1.0)
                        {
                            strResult += "【" + actLstActions[i].strCharacterName + "】施放了感染，但【" + this.chaLstCharacter[actLstActions[i].intDestination].strCharacterName + "】有牧师的帮助，幸运地逃过一劫。" + Environment.NewLine;
                            actLstActions[i].intResult = -1;
                        }
                        else
                        {
                            strResult += "【" + actLstActions[i].strCharacterName + "】施放了感染，不过【" + this.chaLstCharacter[actLstActions[i].intDestination].strCharacterName + "】幸运地逃过了一劫。" + Environment.NewLine;
                            actLstActions[i].intResult = -1;
                        }
                    }
                    //处理狼人触发守护
                    if (intIsTransferred == 0)
                    {
                        for (int j = 0; j < actLstActions.Count; j++)
                        {
                            if (actLstActions[j].intDestination == actLstActions[i].intDestination && actLstActions[j].strSpellName == strGuard && actLstActions[j].intResult == 1)
                            {
                                double dbGuardResult = rand.NextDouble();
                                if (dbGuardResult <= 0.3)
                                {
                                    strResult += "【" + actLstActions[i].strCharacterName + "】的身份被【" + actLstActions[j].strCharacterName + "】识破了！" + Environment.NewLine;
                                }
                                else if (dbGuardResult > 0.3 && dbGuardResult <= 0.7)
                                {
                                    strResult += "【" + actLstActions[i].strCharacterName + "】踩中了【" + actLstActions[j].strCharacterName + "】的陷阱！陷入【茫然】状态。" + Environment.NewLine;
                                    this.chaLstCharacter[actLstActions[i].intCharacter] = Character.setConfused(this.chaLstCharacter[actLstActions[i].intCharacter]);
                                }
                                else if (dbGuardResult > 0.7 && dbGuardResult <= 0.8)
                                {
                                    strResult += "【" + actLstActions[i].strCharacterName + "】被【" + actLstActions[j].strCharacterName + "】的【冷枪】击中！" + Environment.NewLine;
                                    this.chaLstCharacter[actLstActions[i].intCharacter] = Character.setDead(this.chaLstCharacter[actLstActions[i].intCharacter], 2);
                                }
                                actLstActions[j].intResult = 2;
                                this.chaLstCharacter[actLstActions[j].intDestination] = Character.setDisguard(this.chaLstCharacter[actLstActions[j].intDestination]);
                                break;
                            }
                        }
                    }
                }
            }
        }

        //第五轮处理猎人其他技能
        for (int i = 0; i < actLstActions.Count; i++)
        {
            if (actLstActions[i].intResult == 0)
            {
                //处理猎人特有技能
                if (Character.findRoleForCharacter(actLstActions[i].strCharacterName, this.chaLstCharacter) == Hunter)
                {
                    //银弹
                    if (actLstActions[i].strSpellName == strSilverBullet)
                    {
                        //处理挡枪
                        for (int j = 0; j < actLstActions.Count; j++)
                        {
                            if (actLstActions[j].strSpellName == strVisit && actLstActions[j].intDestination == actLstActions[i].intDestination)
                            {
                                if (rand.NextDouble() <= 0.5)
                                {
                                    actLstActions[i].intDestination = actLstActions[j].intCharacter;
                                }
                            }
                        }
                        if (rand.NextDouble() <= Spell.getChance(strSilverBullet, this.splLstSpell) && this.chaLstCharacter[actLstActions[i].intDestination].intDebuff1 != -1)
                        {
                            this.chaLstCharacter[actLstActions[i].intDestination] = Character.setDead(this.chaLstCharacter[actLstActions[i].intDestination], 2);
                            strResult += "【" + actLstActions[i].strCharacterName + "】施放了银弹，【" + this.chaLstCharacter[actLstActions[i].intDestination].strCharacterName + "】死亡。" + Environment.NewLine;
                            actLstActions[i].intResult = 1;
                        }
                        else if (this.chaLstCharacter[actLstActions[i].intDestination].intDebuff1 == -1)
                        {
                            strResult += "【" + actLstActions[i].strCharacterName + "】施放了银弹，但【" + this.chaLstCharacter[actLstActions[i].intDestination].strCharacterName + "】早已死亡。" + Environment.NewLine;
                            actLstActions[i].intResult = -1;
                        }
                        else
                        {
                            strResult += "【" + actLstActions[i].strCharacterName + "】施放了银弹，不过【" + this.chaLstCharacter[actLstActions[i].intDestination].strCharacterName + "】幸运地逃过了一劫。" + Environment.NewLine;
                            actLstActions[i].intResult = -1;
                        }
                    }
                    //圣水
                    else if (actLstActions[i].strSpellName == strHolyWater)
                    {
                        if (rand.NextDouble() <= Spell.getChance(strHolyWater, this.splLstSpell) && this.chaLstCharacter[actLstActions[i].intDestination].intDebuff1 == 1)
                        {
                            this.chaLstCharacter[actLstActions[i].intDestination] = Character.setCured(this.chaLstCharacter[actLstActions[i].intDestination]);
                            strResult += "【" + actLstActions[i].strCharacterName + "】喝下了圣水，【感染】的症状消失了。" + Environment.NewLine;
                            actLstActions[i].intResult = 1;
                            this.chaLstCharacter[actLstActions[i].intDestination].intHolyWaterLimit--;
                        }
                        else
                        {
                            strResult += "【" + actLstActions[i].strCharacterName + "】喝下了圣水，咦？没什么效果啊。到底是圣水过期了还是他本来就没病？" + Environment.NewLine;
                            actLstActions[i].intResult = -1;
                            this.chaLstCharacter[actLstActions[i].intDestination].intHolyWaterLimit--;
                        }
                    }
                    //罗盘
                    else if (actLstActions[i].strSpellName == strCompass)
                    {
                        if (rand.NextDouble() <= Spell.getChance(strCompass, this.splLstSpell))
                        {
                            int intClueIndex = -1;
                            while (intClueIndex == -1)
                            {
                                intClueIndex = Character.clueAbout(this.chaLstCharacter, rand);
                            }
                            strResult += "【" + actLstActions[i].strCharacterName + "】使用了罗盘，获得了关于【" + this.chaLstCharacter[intClueIndex].strCharacterName + "】的真实线索！" + Environment.NewLine;
                            actLstActions[i].intResult = 1;
                        }
                        else
                        {
                            strResult += "【" + actLstActions[i].strCharacterName + "】使用了罗盘，咦？没有反应？" + Environment.NewLine;
                            actLstActions[i].intResult = -1;
                        }
                    }
                }
            }
        }

        for (int i = 0; i < actLstActions.Count; i++)
        {
            if (actLstActions[i].intResult == 0 && this.chaLstCharacter[actLstActions[i].intCharacter].intDebuff1 != -1)
            {
                if (actLstActions[i].strSpellName == strVisit)
                {
                    //处理拜访
                    for (int j = 0; j < actLstActions.Count; j++)
                    {
                        if (actLstActions[j].strCharacterName == this.chaLstCharacter[actLstActions[i].intDestination].strCharacterName && actLstActions[j].strSpellName == strPrayer)
                        {
                            actLstActions[i].intResult = 1;
                            strResult += "【" + actLstActions[i].strCharacterName + "】拜访了【" + this.chaLstCharacter[actLstActions[i].intDestination].strCharacterName + "】，结果对方不在家！" + Environment.NewLine;
                            break;
                        }
                    }
                    if (actLstActions[i].intResult == 0)
                    {
                        strResult += "【" + actLstActions[i].strCharacterName + "】拜访了【" + this.chaLstCharacter[actLstActions[i].intDestination].strCharacterName + "】，双方进行了愉快的会谈！" + Environment.NewLine;
                        actLstActions[i].intResult = 1;
                    }
                    this.chaLstCharacter[actLstActions[i].intDestination].intVisitLimit--;
                }
            }
        }
        return strResult;
    }
}