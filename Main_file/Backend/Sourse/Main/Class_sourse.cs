using System;
using System.Runtime.InteropServices.Marshalling;

public class User
{
	private int userId;
    private string userLogin;
	//private string userEmail;	
	private string userPassword;
	private int userMoney;




    public void chengelogin(string login)
    {
        userLogin = login;
    }
    public string Getlogin()
    {
        return userLogin;
    }
    public void chengePassword(string Passw)
    {
        userPassword = Passw;
    }
    public string GetPassword()
    {
        return userPassword;
    }
    public void chengeMoney(int id)
    {
        userId = id;
    }
    public int GetMoney()
    {
        return userId;
    }

    public void chengeId( int id)
	{
		userId = id;
	}
	public int GetID()
	{
		return userId;
	}

}
