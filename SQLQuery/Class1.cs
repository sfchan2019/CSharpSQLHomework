using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLQuery
{
    public class SQLStatement
    {
        public static string Insert(string name, string genre, string type, string review)
        {
            return String.Format("INSERT INTO[dbo].[Game]([Name], [Genre], [Type],[Review])VALUES('{0}', '{1}', '{2}', '{3}')", name, genre, type, review);
        }
        public static string Select()
        {
            return "SELECT * FROM GAME";
        }
    }
    public class SQLConnection
    {

    }
}
