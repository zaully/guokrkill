using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

public partial class Judge_Brain : System.Web.UI.Page
{
    static int Werewolf = 8;
    static int Priest = 4;
    static int Hunter = 2;
    static int Villager = 1;
    static int Judge = 16;
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
    static string strFire = "火刑";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.Session["Status"] = new Status();
            ClearResultAndPlayers();
        }
        if (false)
        {
            if (Session["encryptedname"] != null && Session["password"] != null)
            {
                if (Accounts.checkAccount(Session["encryptedname"].ToString(), Session["password"].ToString(), 1) != 0)
                {
                    Response.Redirect("~/JudgeLogin.aspx");
                }
            }
            else
            {
                Response.Redirect("~/JudgeLogin.aspx");
            }
        }
    }

    protected void ClearResultAndPlayers()
    {
        tbResult.Text = "";
        tbPlayerList.Text = "为测试;方便;先写几个;名字在;这里;测试前;请把开头的;空格;先去掉;Linux上的;DotNET不稳定;请多指教";
    }

    protected void btnStart_Click(object sender, EventArgs e)
    {
        btnStart.Enabled = false;
        int intHunterNum = 0;
        int intWerewolfNum = 0;
        try
        {
            intHunterNum = Convert.ToInt16(tbHunterNum.Text);
            intWerewolfNum = Convert.ToInt16(tbWerewolfNum.Text);
        }
        catch
        {
            return;
        }
        string strArrayTemp = tbPlayerList.Text;
        strArrayTemp.Replace(Environment.NewLine, "");
        string[] strArrayPlayers = strArrayTemp.Split(';');
        List<int> intListRole = new List<int>();
        tbPlayerList.Text = "";
        for (int i = 0; i < strArrayPlayers.Count(); i++)
        {
            if (strArrayPlayers[i].Length == 0)
            {
                if (i < strArrayPlayers.Count() - 1)
                {
                    return;
                }
                else
                {
                    break;
                }
            }
            for (int j = i + 1; j < strArrayPlayers.Count(); j++)
            {
                if (strArrayPlayers[i] == strArrayPlayers[j])
                {
                    return;
                }
            }
            intListRole.Add(1);
        }
        Random rand = new Random(Utilities.GetRandomSeed());
        int intTemp = -1;
        for (int i = 0; i < intWerewolfNum; i++)
        {
            intTemp = rand.Next(intListRole.Count() - 1);
            if (intListRole[intTemp] != 1)
            {
                i--;
            }
            else
            {
                intListRole[intTemp] = Werewolf;
            }
        }
        for (int i = 0; i < intHunterNum; i++)
        {
            intTemp = rand.Next(intListRole.Count() - 1);
            if (intListRole[intTemp] != 1)
            {
                i--;
            }
            else
            {
                intListRole[intTemp] = Hunter;
            }
        }
        for (int i = 0; i < 1; i++)
        {
            intTemp = rand.Next(intListRole.Count() - 1);
            if (intListRole[intTemp] != 1)
            {
                i--;
            }
            else
            {
                intListRole[intTemp] = Priest;
            }
        }
        int intGameNo = GameStatus.newGame(strArrayPlayers,intListRole);
        tbHunterNum.ReadOnly = true;
        tbWerewolfNum.ReadOnly = true;
        if (intGameNo >= 0)
        {
            tbPlayerList.Text = ((Status)this.Session["Status"]).updateAll(intGameNo, 0);
            lDayNo.Text = Convert.ToString(0);
        }
        tbPlayerList.ReadOnly = true;
        lGameNo.Text = intGameNo.ToString();
        newDay(0, (Status)this.Session["Status"]);
        lHintPlayerList.Text = "";
    }

    protected void newDay(int intDayNo, Status thisStat)
    {
        ddlSource.Enabled = true;
        ddlSource.Items.Clear();
        ddlSource.Items.Add("");
        ddlTarget.Enabled = true;
        ddlTarget.Items.Clear();
        ddlTarget.Items.Add("");
        ddlJudgeTarget.Enabled = true;
        ddlJudgeTarget.Items.Clear();
        ddlJudgeTarget.Items.Add("");
        for (int i = 0; i < thisStat.chaLstCharacter.Count; i++)
        {
            if (thisStat.chaLstCharacter[i].intDebuff1 != -1)
            {
                ddlSource.Items.Add(thisStat.chaLstCharacter[i].strCharacterName);
                ddlTarget.Items.Add(thisStat.chaLstCharacter[i].strCharacterName);
                ddlJudgeTarget.Items.Add(thisStat.chaLstCharacter[i].strCharacterName);
            }
        }
        ddlAction.Enabled = true;
        ddlAction.Items.Clear();
        ddlAction.Items.Add("");
        for (int i = 0; i < thisStat.splLstSpell.Count; i++)
        {
            ddlAction.Items.Add(thisStat.splLstSpell[i].strSpellName);
        }
        btnAddAction.Enabled = true;
        lbActions.Items.Clear();
        btnJudgeAction.Enabled = true;
        ddlJudgeAction.Items.Clear();
        List<Spell> splLstJudge = new List<Spell>();
        splLstJudge = mysql.getJudgeSpells();
        ddlJudgeAction.Items.Add("");
        for (int i = 0; i < splLstJudge.Count; i++)
        {
            ddlJudgeAction.Items.Add(splLstJudge[i].strSpellName);
        }
    }

    protected void btnAddAction_Click(object sender, EventArgs e)
    {
        if (ddlAction.SelectedValue == "" || ddlSource.SelectedValue == "" || ddlTarget.SelectedValue == "")
        {
            return;
        }
        Status statCurrent = (Status)this.Session["Status"];
        for (int i = 0; i < statCurrent.chaLstCharacter.Count; i++)
        {
            if (ddlSource.SelectedValue == statCurrent.chaLstCharacter[i].strCharacterName)
            {
                Regex reg = new Regex("^{" + statCurrent.chaLstCharacter[i].strCharacterName + "}");
                for (int m = 0; m < lbActions.Items.Count; m++)
                {
                    if (reg.IsMatch(lbActions.Items[m].ToString()))
                    {
                        return;
                    }
                }
                for (int j = 0; j < statCurrent.chaLstCharacter.Count; j++)
                {
                    if (ddlTarget.SelectedValue == statCurrent.chaLstCharacter[j].strCharacterName)
                    {
                        for (int k = 0; k < statCurrent.splLstSpell.Count; k++)
                        {
                            if (ddlAction.SelectedValue == statCurrent.splLstSpell[k].strSpellName)
                            {
                                if (!Spell.canCast(i,j,k,statCurrent))
                                {
                                    ddlAction.SelectedValue = "";
                                    ddlSource.SelectedValue = "";
                                    ddlTarget.SelectedValue = "";
                                    return;
                                }
                                string strActionAdded = "{" + statCurrent.chaLstCharacter[i].strCharacterName + "} 准备对->" + statCurrent.chaLstCharacter[j].strCharacterName + "<-使用 [[" + statCurrent.splLstSpell[k].strSpellName + "]]";
                                lbActions.Items.Add(strActionAdded);
                            }
                        }
                    }
                }
            }
        }
        ddlAction.SelectedValue = "";
        ddlSource.SelectedValue = "";
        ddlTarget.SelectedValue = "";
        btnCalculate.Enabled = true;
    }
    /*
    protected void ddlSource_SelectedIndexChanged(object sender, EventArgs e)
    {
        tbResult.Text = "!!!!!!";
        if (ddlSource.SelectedValue == "")
        {
            return;
        }
        ddlAction.Enabled = true;
        List<string> strLstSpells= Character.getSpellList(ddlSource.SelectedValue, (Status)this.Session["Status"]);
        ddlAction.Items.Add("");
        for (int i = 0; i < strLstSpells.Count; i++)
        {
            ddlAction.Items.Add(strLstSpells[i]);
        }
    }*/
    protected void btnDeleteAction_Click(object sender, EventArgs e)
    {
        if (lbActions.SelectedValue == null)
        {
            return;
        }
        else
        {
            lbActions.Items.Remove(lbActions.SelectedValue);
        }
    }
    protected void btnCalculate_Click(object sender, EventArgs e)
    {
        tbResult.Text = "";
        Random rand = new Random(Utilities.GetRandomSeed());
        List<string> strLstActions = new List<string>();
        for (int i = 0; i < lbActions.Items.Count; i++)
        {
            strLstActions.Add(lbActions.Items[i].ToString());
        }
        List<Actions> actLstActions = Actions.initialization(strLstActions, ((Status)this.Session["Status"]));
        List<Character> chaLstAllCharacters = ((Status)this.Session["Status"]).chaLstCharacter;
        List<Spell> splLstAllSpells = ((Status)this.Session["Status"]).splLstSpell;
        for (int i = 0; i < actLstActions.Count; i++)
        {
            if (actLstActions[i].intResult == 0)
            {
                if (Character.findRoleForCharacter(actLstActions[i].strCharacterName, chaLstAllCharacters) == Priest)
                {
                    //处理牧师特有技能
                    //净化
                    if (actLstActions[i].strSpellName == strPurge)
                    {
                        if (rand.NextDouble() <= Spell.getChance(strPurge, splLstAllSpells) && chaLstAllCharacters[actLstActions[i].intDestination].intDebuff1 == 1)
                        {
                            chaLstAllCharacters[actLstActions[i].intDestination] = Character.setCured(chaLstAllCharacters[actLstActions[i].intDestination]);
                            tbResult.Text += "【" + actLstActions[i].strCharacterName + "】的净化成功，【" + chaLstAllCharacters[actLstActions[i].intDestination].strCharacterName + "】的【感染】痊愈了" + Environment.NewLine;
                            actLstActions[i].intResult = 1;
                        }
                        else if (chaLstAllCharacters[actLstActions[i].intDestination].intDebuff1 != -1)
                        {
                            tbResult.Text += "【" + actLstActions[i].strCharacterName + "】施放了净化，但【" + chaLstAllCharacters[actLstActions[i].intDestination].strCharacterName + "】好像本来就很健康" + Environment.NewLine;
                            actLstActions[i].intResult = -1;
                        }
                        else
                        {
                            tbResult.Text += "【" + actLstActions[i].strCharacterName + "】的净化失败" + Environment.NewLine;
                            actLstActions[i].intResult = -1;
                        }
                    }
                    //祈祷
                    else if (actLstActions[i].strSpellName == strPrayer)
                    {
                        if (rand.NextDouble() <= Spell.getChance(strPurge, splLstAllSpells))
                        {
                            chaLstAllCharacters[actLstActions[i].intDestination].dbDevourResist = 0.3;
                            chaLstAllCharacters[actLstActions[i].intDestination].dbInfestResist = 0.3;
                            tbResult.Text += "【" + actLstActions[i].strCharacterName + "】正在祈祷，获得了神的庇护" + Environment.NewLine;
                            actLstActions[i].intResult = 1;
                        }
                        else
                        {
                            tbResult.Text += "【" + actLstActions[i].strCharacterName + "】正在祈祷，但神没有回应" + Environment.NewLine;
                            actLstActions[i].intResult = -1;
                        }
                    }
                    //奉献
                    else if (actLstActions[i].strSpellName == strConsecration)
                    {
                        if (rand.NextDouble() <= Spell.getChance(strConsecration, splLstAllSpells))
                        {
                            chaLstAllCharacters[actLstActions[i].intCharacter] = Character.setDead(chaLstAllCharacters[actLstActions[i].intCharacter]);
                            tbResult.Text += "【" + actLstActions[i].strCharacterName + "】奉献啦！！！" + Environment.NewLine;
                            actLstActions[i].intResult = 1;
                        }
                        else
                        {
                            tbResult.Text += "【" + actLstActions[i].strCharacterName + "】奉献失败，倒霉，想死都不行" + Environment.NewLine;
                            actLstActions[i].intResult = -1;
                        }
                    }
                    //神佑
                    else if (actLstActions[i].strSpellName == strBlessing)
                    {
                        if (rand.NextDouble() <= Spell.getChance(strBlessing, splLstAllSpells))
                        {
                            chaLstAllCharacters[actLstActions[i].intDestination].dbDevourResist = 0.3;
                            chaLstAllCharacters[actLstActions[i].intDestination].dbInfestResist = 0.3;
                            tbResult.Text += "【" + actLstActions[i].strCharacterName + "】施放了神佑，狼人对【" + chaLstAllCharacters[actLstActions[i].intDestination].strCharacterName + "】的攻击被削弱了。" + Environment.NewLine;
                            actLstActions[i].intResult = 1;
                        }
                        else
                        {
                            tbResult.Text += "【" + actLstActions[i].strCharacterName + "】的神佑没能得到神的回应。" + Environment.NewLine;
                            actLstActions[i].intResult = -1;
                        }
                    }
                }
            }
        }
        for (int i = 0; i < actLstActions.Count; i++)
        {
            if (actLstActions[i].intResult == 0)
            {
                if (Character.findRoleForCharacter(actLstActions[i].strCharacterName, chaLstAllCharacters) == Werewolf)
                {
                    //处理狼人特有技能
                    //吞噬
                    if (actLstActions[i].strSpellName == strDevourment)
                    {
                        if (rand.NextDouble() <= Spell.getChance(strDevourment, splLstAllSpells) - chaLstAllCharacters[actLstActions[i].intDestination].dbDevourResist && chaLstAllCharacters[actLstActions[i].intDestination].intDebuff1!=-1)
                        {
                            chaLstAllCharacters[actLstActions[i].intDestination] = Character.setDead(chaLstAllCharacters[actLstActions[i].intDestination]);
                            tbResult.Text += "【" + actLstActions[i].strCharacterName + "】施放了吞噬，【" + chaLstAllCharacters[actLstActions[i].intDestination].strCharacterName + "】死亡。" + Environment.NewLine;
                            actLstActions[i].intResult = 1;
                        }
                        else if (chaLstAllCharacters[actLstActions[i].intDestination].intDebuff1 == -1)
                        {
                            tbResult.Text += "【" + actLstActions[i].strCharacterName + "】施放了吞噬，但【" + chaLstAllCharacters[actLstActions[i].intDestination].strCharacterName + "】早已死亡。" + Environment.NewLine;
                            actLstActions[i].intResult = -1;
                        }
                        else
                        {
                            tbResult.Text += "【" + actLstActions[i].strCharacterName + "】施放了吞噬，不过【" + chaLstAllCharacters[actLstActions[i].intDestination].strCharacterName + "】幸运地逃过了一劫。" + Environment.NewLine;
                            actLstActions[i].intResult = -1;
                        }
                    }
                    //感染
                    if (actLstActions[i].strSpellName == strInfestation)
                    {
                        if (rand.NextDouble() <= Spell.getChance(strInfestation, splLstAllSpells) - chaLstAllCharacters[actLstActions[i].intDestination].dbInfestResist && chaLstAllCharacters[actLstActions[i].intDestination].intDebuff1 != -1 && chaLstAllCharacters[actLstActions[i].intDestination].intDebuff1 != 1)
                        {
                            chaLstAllCharacters[actLstActions[i].intDestination] = Character.setInfested(chaLstAllCharacters[actLstActions[i].intDestination]);
                            tbResult.Text += "【" + actLstActions[i].strCharacterName + "】施放了感染，【" + chaLstAllCharacters[actLstActions[i].intDestination].strCharacterName + "】现在的状态是【感染】。" + Environment.NewLine;
                            actLstActions[i].intResult = 1;
                        }
                        else if (chaLstAllCharacters[actLstActions[i].intDestination].intDebuff1 == -1)
                        {
                            tbResult.Text += "【" + actLstActions[i].strCharacterName + "】施放了感染，但【" + chaLstAllCharacters[actLstActions[i].intDestination].strCharacterName + "】早已死亡。" + Environment.NewLine;
                            actLstActions[i].intResult = -1;
                        }
                        else if (chaLstAllCharacters[actLstActions[i].intDestination].intDebuff1 == 1)
                        {
                            tbResult.Text += "【" + actLstActions[i].strCharacterName + "】施放了感染，但【" + chaLstAllCharacters[actLstActions[i].intDestination].strCharacterName + "】早已被感染了。" + Environment.NewLine;
                            actLstActions[i].intResult = -1;
                        }
                        else
                        {
                            tbResult.Text += "【" + actLstActions[i].strCharacterName + "】施放了感染，不过【" + chaLstAllCharacters[actLstActions[i].intDestination].strCharacterName + "】幸运地逃过了一劫。" + Environment.NewLine;
                            actLstActions[i].intResult = -1;
                        }
                    }
                }
            }
        }
        for (int i = 0; i < actLstActions.Count; i++)
        {
            if (actLstActions[i].intResult == 0)
            {
                if (Character.findRoleForCharacter(actLstActions[i].strCharacterName, chaLstAllCharacters) == Hunter)
                {
                    //处理猎人特有技能
                    //银弹
                    if (actLstActions[i].strSpellName == strSilverBullet)
                    {
                        if (rand.NextDouble() <= Spell.getChance(strSilverBullet, splLstAllSpells) && chaLstAllCharacters[actLstActions[i].intDestination].intDebuff1 != -1)
                        {
                            chaLstAllCharacters[actLstActions[i].intDestination] = Character.setDead(chaLstAllCharacters[actLstActions[i].intDestination]);
                            tbResult.Text += "【" + actLstActions[i].strCharacterName + "】施放了银弹，【" + chaLstAllCharacters[actLstActions[i].intDestination].strCharacterName + "】死亡。" + Environment.NewLine;
                            actLstActions[i].intResult = 1;
                        }
                        else if (chaLstAllCharacters[actLstActions[i].intDestination].intDebuff1 == -1)
                        {
                            tbResult.Text += "【" + actLstActions[i].strCharacterName + "】施放了银弹，但【" + chaLstAllCharacters[actLstActions[i].intDestination].strCharacterName + "】早已死亡。" + Environment.NewLine;
                            actLstActions[i].intResult = -1;
                        }
                        else
                        {
                            tbResult.Text += "【" + actLstActions[i].strCharacterName + "】施放了银弹，不过【" + chaLstAllCharacters[actLstActions[i].intDestination].strCharacterName + "】幸运地逃过了一劫。" + Environment.NewLine;
                            actLstActions[i].intResult = -1;
                        }
                    }
                        //圣水
                    else if (actLstActions[i].strSpellName == strHolyWater)
                    {
                        if (rand.NextDouble() <= Spell.getChance(strHolyWater, splLstAllSpells)&&chaLstAllCharacters[actLstActions[i].intDestination].intDebuff1==1)
                        {
                            chaLstAllCharacters[actLstActions[i].intDestination] = Character.setCured(chaLstAllCharacters[actLstActions[i].intDestination]);
                            tbResult.Text += "【" + actLstActions[i].strCharacterName + "】喝下了圣水，【感染】的症状消失了。" + Environment.NewLine;
                            actLstActions[i].intResult = 1;
                        }
                        else
                        {
                            tbResult.Text += "【" + actLstActions[i].strCharacterName + "】喝下了圣水，咦？没什么效果啊。到底是圣水过期了还是他本来就没病？" + Environment.NewLine;
                            actLstActions[i].intResult = -1;
                        }
                    }
                    //罗盘
                    else if (actLstActions[i].strSpellName == strCompass)
                    {
                        if (rand.NextDouble() <= Spell.getChance(strCompass, splLstAllSpells))
                        {
                            int intClueIndex = -1;
                            while (intClueIndex == -1)
                            {
                                intClueIndex = Character.clueAbout(chaLstAllCharacters, rand);
                            }
                            tbResult.Text += "【" + actLstActions[i].strCharacterName + "】使用了罗盘，获得了关于【" + chaLstAllCharacters[intClueIndex].strCharacterName + "】的真实线索！" + Environment.NewLine;
                            actLstActions[i].intResult = 1;
                        }
                        else
                        {
                            tbResult.Text += "【" + actLstActions[i].strCharacterName + "】使用了罗盘，咦？没有反应？" + Environment.NewLine;
                            actLstActions[i].intResult = -1;
                        }
                    }
                }
            }
        }
        for (int i = 0; i < actLstActions.Count; i++)
        {
            if (actLstActions[i].intResult == 0)
            {
                if (actLstActions[i].strSpellName == strVisit)
                {
                    //处理拜访
                    for (int j = 0; j < actLstActions.Count; j++)
                    {
                        if (actLstActions[j].strCharacterName == chaLstAllCharacters[actLstActions[i].intDestination].strCharacterName)
                        {
                            actLstActions[i].intResult = 1;
                            tbResult.Text += "【" + actLstActions[i].strCharacterName + "】拜访了【" + chaLstAllCharacters[actLstActions[i].intDestination].strCharacterName + "】，结果对方不在家！" + Environment.NewLine;
                            break;
                        }
                    }
                    if (actLstActions[i].intResult == 0)
                    {
                        tbResult.Text += "【" + actLstActions[i].strCharacterName + "】拜访了【" + chaLstAllCharacters[actLstActions[i].intDestination].strCharacterName + "】，双方进行了愉快的会谈！" + Environment.NewLine;
                        actLstActions[i].intResult = 1;
                    }
                }
            }
        }
        //处理感染、饥饿、迷惘、怜悯等
        for (int i = 0; i < chaLstAllCharacters.Count; i++)
        {
            if (chaLstAllCharacters[i].intDebuff1 > 0)
            {
                chaLstAllCharacters[i].intDebuff1Counter--;
            }
            if (chaLstAllCharacters[i].intDebuff2 > 0)
            {
                chaLstAllCharacters[i].intDebuff2Counter--;
            }
            if (chaLstAllCharacters[i].intDebuff3 > 0)
            {
                chaLstAllCharacters[i].intDebuff3Counter--;
            }
            if (chaLstAllCharacters[i].intDebuff1 == 1)
            {
                if (chaLstAllCharacters[i].intDebuff1Counter == 0)
                {
                    chaLstAllCharacters[i].intDebuff1Counter--;
                    chaLstAllCharacters[i].intDebuff1 = 0;
                    chaLstAllCharacters[i].intRole = Werewolf;
                    tbResult.Text += "【" + chaLstAllCharacters[i].strCharacterName + "】的身体产生变异，新的【狼人】出现了！";
                }
                else
                {
                    tbResult.Text += "【" + chaLstAllCharacters[i].strCharacterName + "】的感染期还剩 " + Convert.ToString(chaLstAllCharacters[i].intDebuff1Counter + 1) + " 天";
                }
            }
        }
        //更新玩家状态栏
        lDayNo.Text = (Convert.ToInt16(lDayNo.Text) + 1).ToString();
        int intGameNo = Convert.ToInt16(lGameNo.Text);
        int intDayNo = Convert.ToInt16(lDayNo.Text);
        Status.updateIntoDatabase(intGameNo, Convert.ToInt16(lDayNo.Text), chaLstAllCharacters);
        tbPlayerList.Text = ((Status)this.Session["Status"]).updateAll(intGameNo, intDayNo);
        newDay(intDayNo, ((Status)this.Session["Status"]));
        Status.updateLogs(intGameNo, intDayNo, tbResult.Text);
    }

    protected void btnJudgeAction_Click(object sender, EventArgs e)
    {
        if (ddlJudgeAction.SelectedValue == "" || ddlJudgeTarget.SelectedValue == "")
        {
            ddlJudgeTarget.SelectedValue = "";
            ddlJudgeAction.SelectedValue = "";
            return;
        }
        if (ddlJudgeAction.SelectedValue == strFire)
        {
            int deadIndex = Character.findIndexForCharacter(ddlJudgeTarget.SelectedValue, ((Status)this.Session["Status"]).chaLstCharacter);
            ((Status)this.Session["Status"]).chaLstCharacter[deadIndex] = Character.setDead(((Status)this.Session["Status"]).chaLstCharacter[deadIndex]);
        }
        if (ddlJudgeAction.SelectedValue == strInheritance)
        {
            int index2beHunter = Character.findIndexForCharacter(ddlJudgeTarget.SelectedValue, ((Status)this.Session["Status"]).chaLstCharacter);
            ((Status)this.Session["Status"]).chaLstCharacter[index2beHunter] = Character.setHunter(((Status)this.Session["Status"]).chaLstCharacter[index2beHunter]);
        }
        newDay(0, ((Status)this.Session["Status"]));
        tbPlayerList.Text = ((Status)this.Session["Status"]).updateFromStatus();
    }
}