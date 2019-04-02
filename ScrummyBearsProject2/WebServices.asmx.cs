﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;

namespace ScrummyBearsProject2
{
    /// <summary>
    /// Summary description for WebServices
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class WebServices : System.Web.Services.WebService
    {

       [WebMethod(EnableSession = true)]

       public bool LogOn(string username, string pass)
        {
            bool success = false;

            string sqlConnectString = System.Configuration.ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;
            string sqlSelect = "SELECT Username FROM User WHERE Username=@nameValue and Password=@passValue";

            MySqlConnection sqlConnection = new MySqlConnection(sqlConnectString);
            MySqlCommand sqlCommand = new MySqlCommand(sqlSelect, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@nameValue", HttpUtility.UrlDecode(username));
            sqlCommand.Parameters.AddWithValue("@passValue", HttpUtility.UrlDecode(pass));

            MySqlDataAdapter sqlDa = new MySqlDataAdapter(sqlCommand);
            DataTable sqlDt = new DataTable();
            sqlDa.Fill(sqlDt);

            if(sqlDt.Rows.Count > 0)
            {
                //if a user is found, store them in the session
                Session["Username"] = sqlDt.Rows[0]["username"];
                success = true;
            }
            return success;
        }
<<<<<<< HEAD
        //EXAMPLE OF A SELECT, AND RETURNING "COMPLEX" DATA TYPES
        [WebMethod(EnableSession = true)]
        public Account[] GetAccounts()
=======

        [WebMethod(EnableSession = true)]
        public Survey[] GetSurveys()
>>>>>>> John
        {
            //check out the return type.  It's an array of Account objects.  You can look at our custom Account class in this solution to see that it's 
            //just a container for public class-level variables.  It's a simple container that asp.net will have no trouble converting into json.  When we return
            //sets of information, it's a good idea to create a custom container class to represent instances (or rows) of that information, and then return an array of those objects.  
            //Keeps everything simple.

<<<<<<< HEAD
            //WE ONLY SHARE ACCOUNTS WITH LOGGED IN USERS!
            if (Session["id"] != null)
            {
                DataTable sqlDt = new DataTable("accounts");

                string sqlConnectString = System.Configuration.ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;
                string sqlSelect = "select id, userid, pass, firstname, lastname, email from account where active=1 order by lastname";
=======
            if (Session["Username"] != null)
            {
                DataTable sqlDt = new DataTable("surveys");

                string sqlConnectString = System.Configuration.ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;
                string sqlSelect = "SELECT SurveyID, Sname FROM Survey";
>>>>>>> John

                MySqlConnection sqlConnection = new MySqlConnection(sqlConnectString);
                MySqlCommand sqlCommand = new MySqlCommand(sqlSelect, sqlConnection);

                //gonna use this to fill a data table
                MySqlDataAdapter sqlDa = new MySqlDataAdapter(sqlCommand);
                //filling the data table
                sqlDa.Fill(sqlDt);

                //loop through each row in the dataset, creating instances
                //of our container class Account.  Fill each acciount with
                //data from the rows, then dump them in a list.
<<<<<<< HEAD
                List<Account> accounts = new List<Account>();
                for (int i = 0; i < sqlDt.Rows.Count; i++)
                {
                    //only share user id and pass info with admins!
                    if (Convert.ToInt32(Session["admin"]) == 1)
                    {
                        accounts.Add(new Account
                        {
                            id = Convert.ToInt32(sqlDt.Rows[i]["id"]),
                            userId = sqlDt.Rows[i]["userid"].ToString(),
                            password = sqlDt.Rows[i]["pass"].ToString(),
                            firstName = sqlDt.Rows[i]["firstname"].ToString(),
                            lastName = sqlDt.Rows[i]["lastname"].ToString(),
                            email = sqlDt.Rows[i]["email"].ToString()
                        });
                    }
                    else
                    {
                        accounts.Add(new Account
                        {
                            id = Convert.ToInt32(sqlDt.Rows[i]["id"]),
                            firstName = sqlDt.Rows[i]["firstname"].ToString(),
                            lastName = sqlDt.Rows[i]["lastname"].ToString(),
                            email = sqlDt.Rows[i]["email"].ToString()
                        });
                    }
                }
                //convert the list of accounts to an array and return!
                return accounts.ToArray();
=======
                List<Survey> surveys = new List<Survey>();
                for (int i = 0; i < sqlDt.Rows.Count; i++)
                {
                    //only share user id and pass info with admins!

                    surveys.Add(new Survey
                    {
                        surveyId = sqlDt.Rows[i]["SurveyID"].ToString(),
                        surveyName = sqlDt.Rows[i]["Sname"].ToString()
                    });
                }
                //convert the list of accounts to an array and return!
                return surveys.ToArray();
>>>>>>> John
            }
            else
            {
                //if they're not logged in, return an empty array
<<<<<<< HEAD
                return new Account[0];
=======
                return new Survey[0];
>>>>>>> John
            }
        }
    }
}
