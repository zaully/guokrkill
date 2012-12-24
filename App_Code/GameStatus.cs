using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for GameStatus
/// </summary>
public class GameStatus
{
	public GameStatus()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static int newGame(string[] strArrayPlayer, List<int> intListRole)
    {//返回新GameNo
        int intNextGameNo = mysql.getLatestGameID() + 1;
        List<int> intListPlayerID = new List<int>();
        List<string> strListPlayer = new List<string>();
        for (int i = 0; i < intListRole.Count(); i++)
        {
            strListPlayer.Add(strArrayPlayer[i]);
            intListPlayerID.Add(mysql.findPlayerID(strArrayPlayer[i]));
            if (intListPlayerID[intListPlayerID.Count - 1] == -1)
            {
                //if we cannot find the character
            }
        }
        if (mysql.newGame() != -1 && mysql.insertBatchCharacters(intNextGameNo, intListPlayerID, strListPlayer, intListRole) != -1)
        {
            return (intNextGameNo);
        }
        else
        {
            return -1;
        }
    }
}