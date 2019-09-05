using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using SQLQuery;


namespace SQLProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Application app = new Application();
            app.Run();
        }
    }

    class Application
    {
        public void Run()
        {
            Game game1 = new Game();
            game1.Name = "Room Escape";
            game1.Genre = "Puzzle";
            game1.Type = "Single Player";
            game1.Review = "Worst Game Ever!";
            //AttachDbFilename=c:\folder\database.mdf
            string connectionStr = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CSharpGame;AttachDbFilename=|DataDirectory|\\CSharpGame.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            SqlConnection sqlConn = new SqlConnection(connectionStr);
            try
            {
                sqlConn.Open();
                Console.WriteLine(sqlConn.State);

                //string queryStr = SQLStatement.InsertGameObject(game1);
                //SqlCommand insertCommand = new SqlCommand(queryStr, sqlConn);
                //insertCommand.ExecuteReader();

                string queryStr = SQLStatement.Select();
                SqlCommand selectCommand = new SqlCommand(queryStr, sqlConn);
                SqlDataReader reader = selectCommand.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(String.Format("{0, 10}, {1, 10}, {2, 10}, {3, 10}",
                            reader.GetString(0),
                            reader.GetString(1),
                            reader.GetString(2),
                            reader.GetString(3)));
                    }
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }
                reader.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlConn.Close();
            }
        }
    }

    //public class SQLStatement
    //{

    //    public static string InsertGameObject(Game game)
    //    {
    //        return String.Format("INSERT INTO[dbo].[Game]([Name], [Genre], [Type],[Review])VALUES('{0}', '{1}', '{2}', '{3}')", game.Name, game.Genre, game.Type, game.Review);
    //    }
    //}

    public class Game
    {
        string name;
        string genre;
        string type;
        string review;

        public string Name { get { return name; } set { name = value; } }
        public string Genre { get { return genre; } set { genre = value; } }
        public string Type { get { return type; } set { type = value; } }
        public string Review { get { return review; } set { review = value; } }
    }
}
