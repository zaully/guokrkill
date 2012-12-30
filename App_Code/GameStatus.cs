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
        List<int> intListPlayerIDtmp = new List<int>();
        List<string> strListPlayertmp = new List<string>();
        for (int i = 0; i < intListRole.Count(); i++)
        {
            strListPlayertmp.Add(strArrayPlayer[i]);
            intListPlayerIDtmp.Add(mysql.findPlayerID(strArrayPlayer[i]));
            if (intListPlayerIDtmp[intListPlayerIDtmp.Count - 1] == -1)
            {
                //if we cannot find the character
            }
        }
        Random rand = new Random(Utilities.GetRandomSeed());
        int intIndex = -1;
        while (intListPlayerIDtmp.Count > 0)
        {
            intIndex = rand.Next(intListPlayerIDtmp.Count);
            intListPlayerID.Add(intListPlayerIDtmp[intIndex]);
            strListPlayer.Add(strListPlayertmp[intIndex]);
            intListPlayerIDtmp.RemoveAt(intIndex);
            strListPlayertmp.RemoveAt(intIndex);
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