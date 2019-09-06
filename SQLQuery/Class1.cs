using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLQuery
{
    public class SQLStatement
    {

    }

    public class Select : SQLStatement
    {
        //Read
        public string SelectAll()
        {
            return "SELECT * FROM GAME";
        }
    }

    public class Insert : SQLStatement
    {
        public string InsertIntoGame(string name, string genre, string type, string review)
        {
            return String.Format("INSERT INTO[dbo].[Game]([Name], [Genre], [Type],[Review])VALUES('{0}', '{1}', '{2}', '{3}')", name, genre, type, review);
        }
    }

    public class Update : SQLStatement
    {
        public string UpdateRowByName(string column, string value, string name)
        {
            return String.Format("UPDATE[dbo].[Game] SET {0} = '{1}' WHERE Name='{2}'", column, value, name);
        }
    }

    public class Delete : SQLStatement
    {
        public string DeleteFromGame(string name)
        {
            return String.Format("DELETE FROM[dbo].[Game] WHERE Name ='{0}'", name);
        }
    }
}

