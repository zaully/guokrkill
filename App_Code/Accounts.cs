using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Accounts
/// </summary>
public class Accounts
{
	public Accounts()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static string md5(string strInput, int intCode=32)
    {
        if (intCode == 16)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(strInput, "MD5").Substring(8, 16); 
        }
        else
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(strInput, "MD5"); 
        }
    }
    public int registerAccount(string strAccountName, string strPassword, int intIsJudge)
    {
        int intResult = -1;
        if (intIsJudge == 1)
        {
            mysql my = new mysql();
            intResult = my.insertIntoJudge(strAccountName, md5(strPassword));
            if (intResult == 0)
            {
                return 0;
            }
            else if (intResult == 1)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
        return -1;
    }
    public static int checkAccount(string strAccountName, string strPassword, int intIsJudge)
    {
        mysql myCheckPlayer = new mysql();
        string strMd5Password = myCheckPlayer.getPlayerMd5Password(strAccountName);
        if (strMd5Password == "-1")
        {
            return -1;
        }
        string md5ed = "";
        if (strPassword.Length < 10)
        {
            md5ed = md5(strPassword);
        }
        else
        {
            md5ed = strPassword;
        }
        if (md5ed == strMd5Password)
        {
            return 0;
        }
        return -1;
    }
}