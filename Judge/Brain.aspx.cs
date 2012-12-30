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
    static int Monster = 32;

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
    static string strSpy = "窥探";

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
        strArrayTemp = strArrayTemp.Replace(Environment.NewLine, "");
        strArrayTemp = strArrayTemp.Replace("\t", "");
        strArrayTemp = strArrayTemp.Replace(" ", "");
        strArrayTemp = strArrayTemp.Replace("；", ";");
        if (strArrayTemp[strArrayTemp.Length - 1] == ';')
        {
            strArrayTemp.Remove(strArrayTemp.Length - 1);
        }
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
        ddlInheritanceFrom.Visible = false;
        ddlInheritanceFrom.Items.Clear();
        ddlInheritanceFrom.Items.Add("");
        for (int i = 0; i < thisStat.chaLstCharacter.Count; i++)
        {
            if (thisStat.chaLstCharacter[i].intDebuff1 != -1)
            {
                ddlSource.Items.Add(thisStat.chaLstCharacter[i].strCharacterName);
                ddlJudgeTarget.Items.Add(thisStat.chaLstCharacter[i].strCharacterName);
            }
        }
        ddlAction.Enabled = true;
        ddlAction.Items.Add("");
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
        tbResult.Text = ((Status)this.Session["Status"]).CalculateResult(actLstActions);
        Status thisStatus = (Status)this.Session["Status"];
        //处理感染、饥饿、迷惘、怜悯等
        for (int i = 0; i < thisStatus.chaLstCharacter.Count; i++)
        {
            if (thisStatus.chaLstCharacter[i].intDebuff1 > 0)
            {
                thisStatus.chaLstCharacter[i].intDebuff1Counter--;
            }
            if (thisStatus.chaLstCharacter[i].intDebuff3 > 0)
            {
                thisStatus.chaLstCharacter[i].intDebuff3Counter--;
            }
            if (thisStatus.chaLstCharacter[i].intDebuff1 == 1)
            {
                if (thisStatus.chaLstCharacter[i].intDebuff1Counter == 0)
                {
                    thisStatus.chaLstCharacter[i].intDebuffCount--;
                    thisStatus.chaLstCharacter[i].intDebuff1 = 0;
                    thisStatus.chaLstCharacter[i].intRole = Werewolf;
                    tbResult.Text += "【" + thisStatus.chaLstCharacter[i].strCharacterName + "】的身体产生变异，新的【狼人】出现了！" + Environment.NewLine;
                }
                else
                {
                    tbResult.Text += "【" + thisStatus.chaLstCharacter[i].strCharacterName + "】的感染期还剩 " + Convert.ToString(thisStatus.chaLstCharacter[i].intDebuff1Counter) + " 天" + Environment.NewLine;
                }
            }
            if (thisStatus.chaLstCharacter[i].intDebuff3 == 5)
            {
                if (thisStatus.chaLstCharacter[i].intDebuff3Counter == 0)
                {
                    thisStatus.chaLstCharacter[i].intDebuffCount--;
                    thisStatus.chaLstCharacter[i].intDebuff3 = 0;
                    tbResult.Text += "【" + thisStatus.chaLstCharacter[i].strCharacterName + "】的【神佑】消失了。" + Environment.NewLine;
                }
                else
                {
                    tbResult.Text += "【" + thisStatus.chaLstCharacter[i].strCharacterName + "】的【神佑】还剩 " + Convert.ToString(thisStatus.chaLstCharacter[i].intDebuff3Counter) + " 天" + Environment.NewLine;
                }
            }
        }
        //更新玩家状态栏
        lDayNo.Text = (Convert.ToInt16(lDayNo.Text) + 1).ToString();
        int intGameNo = Convert.ToInt16(lGameNo.Text);
        int intDayNo = Convert.ToInt16(lDayNo.Text);
        Status.updateIntoDatabase(intGameNo, Convert.ToInt16(lDayNo.Text), thisStatus.chaLstCharacter);
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
        tbResult.Text = "";
        if (ddlJudgeAction.SelectedValue == strFire)
        {
            int deadIndex = Character.findIndexForCharacter(ddlJudgeTarget.SelectedValue, ((Status)this.Session["Status"]).chaLstCharacter);
            ((Status)this.Session["Status"]).chaLstCharacter[deadIndex] = Character.setDead(((Status)this.Session["Status"]).chaLstCharacter[deadIndex], 3);
            tbResult.Text += "【" + ((Status)this.Session["Status"]).chaLstCharacter[deadIndex].strCharacterName + "】被大家投死啦" + Environment.NewLine;
        }
        if (ddlJudgeAction.SelectedValue == strInheritance)
        {
            if (ddlInheritanceFrom.SelectedValue == "")
            {
                return;
            }
            int index2beHunter = Character.findIndexForCharacter(ddlJudgeTarget.SelectedValue, ((Status)this.Session["Status"]).chaLstCharacter);
            //((Status)this.Session["Status"]).chaLstCharacter[index2beHunter] = Character.setHunter(((Status)this.Session["Status"]).chaLstCharacter[index2beHunter]);
            ((Status)this.Session["Status"]).Inherit(index2beHunter, Character.findIndexForCharacter(ddlInheritanceFrom.SelectedValue, ((Status)this.Session["Status"]).chaLstCharacter));
            tbResult.Text += "【" + ((Status)this.Session["Status"]).chaLstCharacter[index2beHunter].strCharacterName + "】接下了【" + ((Status)this.Session["Status"]).chaLstCharacter[Character.findIndexForCharacter(ddlInheritanceFrom.SelectedValue, ((Status)this.Session["Status"]).chaLstCharacter)].strCharacterName + "】的衣钵！成为了新一代的猎人。" + Environment.NewLine;
        }
        int intGameNo = Convert.ToInt16(lGameNo.Text);
        int intDayNo = Convert.ToInt16(lDayNo.Text);
        Status.updateLogs(intGameNo, intDayNo, tbResult.Text);
        newDay(0, ((Status)this.Session["Status"]));
        tbPlayerList.Text = ((Status)this.Session["Status"]).updateFromStatus();
    }
    protected void ddlSource_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlAction.Items.Clear();
        ddlAction.Items.Add("");
        ddlTarget.Items.Clear();
        ddlTarget.Items.Add("");
        if (ddlSource.SelectedValue == "")
        {
            return;
        }
        Status thisStat = (Status)this.Session["Status"];
        int intSourceIndex = Character.findIndexForCharacter(ddlSource.SelectedValue, thisStat.chaLstCharacter);
        
        for (int i = 0; i < thisStat.splLstSpell.Count; i++)
        {
            if (Spell.canCast(intSourceIndex, -1, i, thisStat))
            {
                ddlAction.Items.Add(thisStat.splLstSpell[i].strSpellName);
            }
        }
    }
    protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlTarget.Items.Clear();
        ddlTarget.Items.Add("");
        if (ddlAction.SelectedValue == "")
        {
            return;
        }
        Status thisStat = (Status)this.Session["Status"];
        int intSourceIndex = Character.findIndexForCharacter(ddlSource.SelectedValue, thisStat.chaLstCharacter);
        int intSpellIndex = -1;
        for (int i = 0; i < thisStat.splLstSpell.Count; i++)
        {
            if (thisStat.splLstSpell[i].strSpellName == ddlAction.SelectedValue)
            {
                intSpellIndex = i;
            }
        }
        for (int i = 0; i < thisStat.chaLstCharacter.Count; i++)
        {
            if (Spell.canCast(intSourceIndex, i, intSpellIndex, thisStat))
            {
                ddlTarget.Items.Add(thisStat.chaLstCharacter[i].strCharacterName);
            }
        }
    }
    protected void ddlJudgeAction_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlJudgeAction.SelectedValue == strInheritance)
        {
            ddlInheritanceFrom.Visible = true;
            ddlInheritanceFrom.Items.Clear();
            ddlInheritanceFrom.Items.Add("");
            Status thisStat = (Status)this.Session["Status"];
            for (int i = 0; i < thisStat.chaLstCharacter.Count; i++)
            {
                if (thisStat.chaLstCharacter[i].intRole == Hunter && thisStat.chaLstCharacter[i].intDebuff1 == -1 && thisStat.chaLstCharacter[i].intDebuff2 == 0 && thisStat.chaLstCharacter[i].intDeathCause == 1)
                {
                    ddlInheritanceFrom.Items.Add(thisStat.chaLstCharacter[i].strCharacterName);
                }
            }
        }
        else
        {
            ddlInheritanceFrom.Visible = false;
        }
    }
    protected void ddlJudgeTarget_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}