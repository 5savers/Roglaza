using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;

//Reference 
//http://blog.tigrangasparian.com/2012/02/09/getting-started-with-sqlite-in-c-part-one/
namespace Roglaza.Classes
{
    public class DbQueries
    {
        public static string ScreenShots_Table_create_query = @"
CREATE TABLE `ScreenShots` (
	`Id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`Date`	TEXT,
	`Image`	BLOB,
	`Uploaded`	INTEGER DEFAULT 0,
	`Sent`	INTEGER DEFAULT 0,
	`Url`	TEXT
);";
        public static string CamShots_Table_create_query = @"
CREATE TABLE `CamShots` (
	`Id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`Date`	TEXT,
	`Image`	BLOB,
	`Uploaded`	INTEGER DEFAULT 0,
	`Sent`	INTEGER DEFAULT 0,
	`Url`	TEXT
);";
        public static string History_Table_Create_query = @"
CREATE TABLE `History` (
	`ID`	INTEGER PRIMARY KEY AUTOINCREMENT,
	`Url`	TEXT,
	`Browser`	TEXT,
	`Date`	TEXT
);";



    }
   public class DBManager
    {
       public static SQLiteConnection con = new SQLiteConnection();
       public static SQLiteCommand com = new SQLiteCommand();
       public static SQLiteDataAdapter Adap = new SQLiteDataAdapter(); 
       public static string DbPath = "store.db";

       public static string  GetConnectionString()
       {

           return "Data Source="+DbPath+";Version=3;";
       }
      
    
       public static bool CreateNewDataBase()
       {
          
           try
           {

               ExecuteNoneQuery(DbQueries.CamShots_Table_create_query);
               ExecuteNoneQuery(DbQueries.ScreenShots_Table_create_query);
               ExecuteNoneQuery(DbQueries.History_Table_Create_query);


               return true;
           }catch
           {
               return false;
           }
       }

       public static int ExecuteNoneQuery(string q)
       {
           try
           {
               con = new SQLiteConnection(GetConnectionString());
               con.Open();
               com=new SQLiteCommand( q,con);
               int res = com.ExecuteNonQuery(); 
               con.Close();
               return res;
           }catch{
               return 0;
           }
       }
       public static SQLiteDataReader ExecuteReader(string q)
       {
           try
           {
               con = new SQLiteConnection(GetConnectionString());
               con.Open(); 
               com = new SQLiteCommand(q, con);
               var res = com.ExecuteReader();
               con.Close();
               return res;
           }
           catch
           {
               return null;
           }
       }

    }
}
