using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Utilities
/// </summary>
public class Utilities
{
	public Utilities()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static string alert(string strAlert)
    {
        return ("<script>alert('" + strAlert + "')</script>");
    }

    public static int GetRandomSeed()
    {
        byte[] bytes = new byte[4];
        System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
        rng.GetBytes(bytes);
        return BitConverter.ToInt32(bytes, 0);
    }
    //读出时进行转换
    public static string ISO8859_GB2312(string read)
    {
        //声明字符集
        System.Text.Encoding iso8859, gb2312;
        //iso8859
        iso8859 = System.Text.Encoding.GetEncoding("gbk");
        //国标2312
        gb2312 = System.Text.Encoding.GetEncoding("UTF-8");
        byte[] iso;
        iso = iso8859.GetBytes(read);
        //返回转换后的字符
        return gb2312.GetString(iso);
    }
}