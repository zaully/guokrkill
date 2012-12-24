using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;

/// <summary>
/// Summary description for mysql
/// </summary>
/// 
public class mysql
{
    const string strDataSource = "";
    //const string strDataSource = "";
    const string strDatabase = "";
    const string strDatabaseUserID = "";
    const string strPW = "";

    public mysql()
    {
	//
	// TODO: Add constructor logic here
	//
    }

    public string getPlayerMd5Password(string strAccountName)
    {
        string strConnection = "Server=" + strDataSource + ";Database=" + strDatabase + ";Uid=" + strDatabaseUserID + ";Pwd=" + strPW;
        MySqlConnection myConnect = new MySqlConnection(strConnection);
        MySqlCommand myCheck = new MySqlCommand("select password from Player where username='" + strAccountName + "';");
        MySqlDataReader myPasswordReader;
        myCheck.Connection = myConnect;
        try
        {
            myConnect.Open();
            myPasswordReader = myCheck.ExecuteReader();
        }
        catch
        {
            return ("-1");
        }
        if (!myPasswordReader.HasRows)
        {
            return ("-1");
        }
        string strReturn = "";
        while (myPasswordReader.Read())
        {
            strReturn = myPasswordReader.GetString(myPasswordReader.GetOrdinal("password"));
        }
        myConnect.Close();
        return (strReturn);
    }

    public int insertIntoJudge(string strAccountName, string strPassword)
    {
        string strConnection = "Server=" + strDataSource + ";Database=" + strDatabase + ";Uid=" + strDatabaseUserID + ";Pwd=" + strPW;
        MySqlConnection myConnection = new MySqlConnection(strConnection);
        MySqlCommand myGetName = new MySqlCommand("select username from Player where username='" + strAccountName + "';");
        string strInsert = "insert into Player(username,password) values('" + strAccountName + "','" + strPassword + "');";
        MySqlCommand myCom = new MySqlCommand(strInsert);
        myCom.Connection = myConnection;
        myGetName.Connection = myConnection;
        MySqlDataReader accountnameReader;
        try
        {
            myConnection.Open();
            accountnameReader = myGetName.ExecuteReader();
            if (accountnameReader.HasRows)
            {
                return 1;
            }
            myConnection.Close();
            myConnection.Open();
            myCom.ExecuteNonQuery();
            myConnection.Close();
            return 0;
        }
        catch (MySqlException e)
        {
            Console.WriteLine(e.Message);
        }
        return -1;
    }

    public static int newGame()
    {
        string strConnection = "Server=" + strDataSource + ";Database=" + strDatabase + ";Uid=" + strDatabaseUserID + ";Pwd=" + strPW;
        MySqlConnection myConnection = new MySqlConnection(strConnection);
        MySqlCommand myComm = new MySqlCommand("insert into guokrhunt.Game(GameName) values('');");
        myComm.Connection = myConnection;
        try
        {
            myConnection.Open();
            myComm.ExecuteNonQuery();
            myConnection.Close();
        }
        catch
        {
            return -1;
        }
        return 0;
    }

    public static int getLatestGameID()
    {
        string strConnection = "Server=" + strDataSource + ";Database=" + strDatabase + ";Uid=" + strDatabaseUserID + ";Pwd=" + strPW;
        MySqlConnection myConnection = new MySqlConnection(strConnection);
        MySqlCommand myComm = new MySqlCommand("select Id from guokrhunt.Game order by Id desc");
        MySqlDataReader myGameNoReader;
        myComm.Connection = myConnection;
        int intGameNo = -1;
        try
        {
            myConnection.Open();
            myGameNoReader = myComm.ExecuteReader();
            if (myGameNoReader.Read())
            {
                intGameNo = myGameNoReader.GetInt16(myGameNoReader.GetOrdinal("Id"));
            }
        }
        catch
        {
        }
        return(intGameNo);
    }

    public static int insertNewCharacter(int intGameNo, int intPlayerID, string strCharacterName)
    {
        int intResult = -1;
        MySqlConnection myConnection = new MySqlConnection("Server=" + strDataSource + ";Database=" + strDatabase + ";Uid=" + strDatabaseUserID + ";Pwd=" + strPW);
        MySqlCommand myComm = new MySqlCommand("insert into guokrhunt.Character(GameNo,PlayerID,CharacterName) values (" + intGameNo + "," + intPlayerID + ",'" + strCharacterName + "')");
        myComm.Connection = myConnection;
        try
        {
            myConnection.Open();
            intResult = myComm.ExecuteNonQuery();
        }
        catch
        {
        }
        return (intResult);
    }

    public static int insertBatchCharacters(int intGameNo, List<int> intListPlayerID, List<string> strListCharacterName, List<int> intListRole)
    {
        int intResult = -1;
        MySqlConnection myConnection = new MySqlConnection("Server=" + strDataSource + ";Database=" + strDatabase + ";Uid=" + strDatabaseUserID + ";Pwd=" + strPW);
        string strBatchInsertCommand = "";
        for (int i = 0; i < intListPlayerID.Count; i++)
        {
            strBatchInsertCommand += "insert into guokrhunt.Character(GameNo,DayNo,PlayerID,CharacterName,InitialRole,CurrentRole,Status1,Status1Counter,Status2,Status2Counter,Status3,Status3Counter) values (" + intGameNo + ",0," + intListPlayerID[i] + ",'" + strListCharacterName[i] + "'," + intListRole[i] + "," + intListRole[i] + ",0,0,0,0,0,0);";
        }
        MySqlCommand myComm = new MySqlCommand(strBatchInsertCommand);
        myComm.Connection = myConnection;
        try
        {
            myConnection.Open();
            intResult = myComm.ExecuteNonQuery();
        }
        catch
        {
        }
        return (intResult);
    }

    public static int findPlayerID(string strPlayerName)
    {
        int intResult = -1;
        MySqlConnection myConnection = new MySqlConnection("Server=" + strDataSource + ";Database=" + strDatabase + ";Uid=" + strDatabaseUserID + ";Pwd=" + strPW);
        MySqlCommand myComm = new MySqlCommand("select id from guokrhunt.Player where Charactername ='" + strPlayerName + "'");
        MySqlDataReader myReader;
        myComm.Connection = myConnection;
        try
        {
            myConnection.Open();
            myReader = myComm.ExecuteReader();
            if (myReader.Read())
            {
                intResult = myReader.GetInt16(myReader.GetOrdinal("id"));
            }
        }
        catch
        {
        }
        return (intResult);        
    }

    public static int updateBatchPlayers(string strSQL)
    {
        MySqlConnection myConnection = new MySqlConnection("Server=" + strDataSource + ";Database=" + strDatabase + ";Uid=" + strDatabaseUserID + ";Pwd=" + strPW);
        MySqlCommand myComm = new MySqlCommand();
        myComm.CommandText = strSQL;
        try
        {
            myComm.Connection = myConnection;
            myConnection.Open();
            myComm.ExecuteNonQuery();
        }
        catch
        {
            return -1;
        }
        return 0;
    }

    public static List<Character> getBatchPlayers(int intGameNo, int intDayNo, string strGetType)
    {
        MySqlConnection myConnection = new MySqlConnection("Server=" + strDataSource + ";Database=" + strDatabase + ";Uid=" + strDatabaseUserID + ";Pwd=" + strPW);
        MySqlCommand myComm = new MySqlCommand();
        if (strGetType == "All")
        {
            myComm.CommandText = "select * from guokrhunt.Character where GameNo = " + intGameNo + " and DayNo =" + intDayNo + ";";
        }
        List<Character> chaLstPlayers = new List<Character>();
        MySqlDataReader myPlayerReader;
        try
        {
            myComm.Connection = myConnection;
            myConnection.Open();
            myPlayerReader = myComm.ExecuteReader();
            while (myPlayerReader.Read())
            {
                chaLstPlayers.Add(new Character());
                chaLstPlayers[chaLstPlayers.Count - 1].intInitialRole = myPlayerReader.GetInt16(myPlayerReader.GetOrdinal("InitialRole"));
                chaLstPlayers[chaLstPlayers.Count - 1].intDebuffCount = 0;
                chaLstPlayers[chaLstPlayers.Count - 1].strCharacterName = myPlayerReader.GetString(myPlayerReader.GetOrdinal("CharacterName"));
                chaLstPlayers[chaLstPlayers.Count - 1].intRole = myPlayerReader.GetInt16(myPlayerReader.GetOrdinal("CurrentRole"));
                chaLstPlayers[chaLstPlayers.Count - 1].intDebuff1 = myPlayerReader.GetInt16(myPlayerReader.GetOrdinal("Status1"));
                if (chaLstPlayers[chaLstPlayers.Count - 1].intDebuff1 != 0)
                {
                    chaLstPlayers[chaLstPlayers.Count - 1].intDebuffCount++;
                }
                chaLstPlayers[chaLstPlayers.Count - 1].intDebuff2 = myPlayerReader.GetInt16(myPlayerReader.GetOrdinal("Status2"));
                if (chaLstPlayers[chaLstPlayers.Count - 1].intDebuff2 != 0)
                {
                    chaLstPlayers[chaLstPlayers.Count - 1].intDebuffCount++;
                }
                chaLstPlayers[chaLstPlayers.Count - 1].intDebuff3 = myPlayerReader.GetInt16(myPlayerReader.GetOrdinal("Status3"));
                if (chaLstPlayers[chaLstPlayers.Count - 1].intDebuff3 != 0)
                {
                    chaLstPlayers[chaLstPlayers.Count - 1].intDebuffCount++;
                }
                chaLstPlayers[chaLstPlayers.Count - 1].intDebuff1Counter = myPlayerReader.GetInt16(myPlayerReader.GetOrdinal("Status1Counter"));
                chaLstPlayers[chaLstPlayers.Count - 1].intDebuff2Counter = myPlayerReader.GetInt16(myPlayerReader.GetOrdinal("Status2Counter"));
                chaLstPlayers[chaLstPlayers.Count - 1].intDebuff3Counter = myPlayerReader.GetInt16(myPlayerReader.GetOrdinal("Status3Counter"));
            }
        }
        catch
        {
        }
        return chaLstPlayers;
    }
    
    public static string findPlayerName(int intID)
    {
        string strResult = "-1";
        MySqlConnection myConnection = new MySqlConnection("Server=" + strDataSource + ";Database=" + strDatabase + ";Uid=" + strDatabaseUserID + ";Pwd=" + strPW);
        MySqlCommand myComm = new MySqlCommand("select gamename from guokrhunt.Game");
        MySqlDataReader myReader;
        myComm.Connection = myConnection;
        try
        {
            myConnection.Open();
            myReader = myComm.ExecuteReader();
            if (myReader.Read())
            {
                strResult = myReader.GetString(myReader.GetOrdinal("gamename"));
            }
        }
        catch
        {
        }
        return (strResult);
        /*
        string strResult = "-1";
        MySqlConnection myConnection = new MySqlConnection("Server=" + strDataSource + ";Database=" + strDatabase + ";Uid=" + strDatabaseUserID + ";Pwd=" + strPW);
        MySqlCommand myComm = new MySqlCommand("select charactername from guokrhunt.Character where Id =" + intID + "");
        MySqlDataReader myReader;
        myComm.Connection = myConnection;
        try
        {
            myConnection.Open();
            myReader = myComm.ExecuteReader();
            if (myReader.Read())
            {
                strResult = myReader.GetString(myReader.GetOrdinal("charactername"));
            }
        }
        catch
        {
        }
        return (strResult);*/
    }

    public static List<Spell> getJudgeSpells()
    {
        List<Spell> splLstJudge = new List<Spell>();
        MySqlConnection myConnection = new MySqlConnection("Server=" + strDataSource + ";Database=" + strDatabase + ";Uid=" + strDatabaseUserID + ";Pwd=" + strPW);
        MySqlCommand myComm = new MySqlCommand("select * from Spell where caster=16");
        MySqlDataReader myReader;
        myComm.Connection = myConnection;
        try
        {
            myConnection.Open();
            myReader = myComm.ExecuteReader();
            while (myReader.Read())
            {
                Spell spell = new Spell();
                spell.strSpellName = myReader.GetString(myReader.GetOrdinal("Name"));
                spell.strDescription = myReader.GetString(myReader.GetOrdinal("Description"));
                spell.intCaster = myReader.GetInt16(myReader.GetOrdinal("Caster"));
                spell.intTarget = myReader.GetInt16(myReader.GetOrdinal("Target"));
                spell.intChance = myReader.GetInt16(myReader.GetOrdinal("Chance"));
                spell.intTargetPredetermined = myReader.GetInt16(myReader.GetOrdinal("isTargetPredetermined"));
                splLstJudge.Add(spell);
            }
        }
        catch
        {
        }
        return splLstJudge;
    }

    public static List<Spell> getSpells()
    {
        List<Spell> lstSpell = new List<Spell>();
        MySqlConnection myConnection = new MySqlConnection("Server=" + strDataSource + ";Database=" + strDatabase + ";Uid=" + strDatabaseUserID + ";Pwd=" + strPW);
        MySqlCommand myComm = new MySqlCommand("select * from guokrhunt.Spell where Usable=1");
        MySqlDataReader myReader;
        myComm.Connection = myConnection;
        try
        {
            myConnection.Open();
            myReader = myComm.ExecuteReader();
            while (myReader.Read())
            {
                Spell spell = new Spell();
                spell.strSpellName = myReader.GetString(myReader.GetOrdinal("Name"));
                spell.strDescription = myReader.GetString(myReader.GetOrdinal("Description"));
                spell.intCaster = myReader.GetInt16(myReader.GetOrdinal("Caster"));
                spell.intTarget = myReader.GetInt16(myReader.GetOrdinal("Target"));
                spell.intChance = myReader.GetInt16(myReader.GetOrdinal("Chance"));
                spell.intTargetPredetermined = myReader.GetInt16(myReader.GetOrdinal("isTargetPredetermined"));
                lstSpell.Add(spell);
            }
        }
        catch
        {
        }
        return lstSpell;
    }

    public static int runSql(string strSQL)
    {
        MySqlConnection myConnection = new MySqlConnection("Server=" + strDataSource + ";Database=" + strDatabase + ";Uid=" + strDatabaseUserID + ";Pwd=" + strPW);
        MySqlCommand myComm = new MySqlCommand("");
        myComm.Connection = myConnection;
        myComm.CommandText = strSQL;
        int intResult = -1;
        try
        {
            myConnection.Open();
            intResult = myComm.ExecuteNonQuery();
        }
        catch
        {
        }
        return intResult;
    }
}
