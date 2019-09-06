using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


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

    public class Application
    {
        public void Run()
        {
            while (true)
            {
                Console.WriteLine("Select 1 to create new row, 2 to update the table, 3 to read the table, 4 to delete a row, 9 to exit");
                int option = Int32.Parse(Console.ReadLine());
                switch (option)
                {
                    case 1:
                        Game game1 = CreateGameObject();
                        Insert sqlHelper = new Insert();
                        RunSQL(sqlHelper.InsertIntoGame(game1.Name, game1.Genre, game1.Type, game1.Review));
                        break;
                    case 2:
                        Console.WriteLine("Enter the column to update: ");
                        string column = Console.ReadLine();
                        Console.WriteLine("Enter the value for the column: ");
                        string value = Console.ReadLine();
                        Console.WriteLine("Enter the name of the game to update");
                        string name = Console.ReadLine();
                        Update sqlHelper2 = new Update();
                        RunSQL(sqlHelper2.UpdateRowByName(column, value, name));
                        break;
                    case 3:
                        Select sqlHelper3 = new Select();
                        RunSQL(sqlHelper3.SelectAll());
                        break;
                    case 4:
                        Delete sqlHelper4 = new Delete();
                        Console.WriteLine("Enter the name of the game to delete");
                        string name2 = Console.ReadLine();
                        RunSQL(sqlHelper4.DeleteFromGame(name2));
                        break;
                    case 9:
                        return;
                    default:
                        break;
                }
            }
        }
        private List<string> PrintFromReader(SqlDataReader reader)
        {
            List<string> result = new List<string>();
            PrintRow("Game", "Genre", "Type", "Review");
            while (reader.Read())
            {
                string row = PrintRow(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3));
                result.Add(row);
            }
            return result;
        }

        private Game CreateGameObject()
        {
            Console.WriteLine("Enter the name of the game: ");
            string name = Console.ReadLine();
            Console.WriteLine("Enter the genre of the game: ");
            string genre = Console.ReadLine();
            Console.WriteLine("Enter the type of the game: ");
            string type = Console.ReadLine();
            Console.WriteLine("Enter the review of the game: ");
            string review = Console.ReadLine();
            return new Game(name, genre, type, review);
        }

        public string PrintRow(string str1, string str2, string str3, string str4)
        {
            return (String.Format("{0, 30} {1, 30} {2, 30} {3, 30}", str1, str2, str3, str4));
        }

        //Just run the SQLQuery
        public void RunSQL(string sqlStatment)
        {
            string dataSource = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = CSharpGame; AttachDbFilename =|DataDirectory|\\CSharpGame.mdf; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
            SqlConnection sqlConn = new SqlConnection(dataSource);
            try
            {
                sqlConn.Open();

                SqlCommand selectCommand = new SqlCommand(sqlStatment, sqlConn);
                SqlDataReader reader = selectCommand.ExecuteReader();

                if (reader.HasRows)
                {
                    PrintFromReader(reader);
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

        //Return sql state or query result
        public void RunSQL(string sqlStatment, out List<string> result)
        {
            result = null;
            string dataSource = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = CSharpGame; AttachDbFilename =|DataDirectory|\\CSharpGame.mdf; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
            SqlConnection sqlConn = new SqlConnection(dataSource);
            try
            {
                sqlConn.Open();
                result.Add(sqlConn.State.ToString());
                SqlCommand selectCommand = new SqlCommand(sqlStatment, sqlConn);
                SqlDataReader reader = selectCommand.ExecuteReader();

                if (reader.HasRows)
                {
                    result = PrintFromReader(reader);
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

        public Game(string name, string genre, string type, string review)
        {
            this.name = name;
            this.genre = genre;
            this.type = type;
            this.review = review;
        }
    }

    public class Select
    {
        //Read
        public string SelectAll()
        {
            return "SELECT * FROM GAME";
        }
    }

    public class Insert
    {
        public string InsertIntoGame(string name, string genre, string type, string review)
        {
            return String.Format("INSERT INTO[dbo].[Game]([Name], [Genre], [Type],[Review])VALUES('{0}', '{1}', '{2}', '{3}')", name, genre, type, review);
        }

        public string InsertIntoGame(Game game)
        {
            return String.Format("INSERT INTO[dbo].[Game]([Name], [Genre], [Type],[Review])VALUES('{0}', '{1}', '{2}', '{3}')", game.Name, game.Genre, game.Type, game.Review);
        }
    }

    public class Update
    {
        public string UpdateRowByName(string column, string value, string name)
        {
            return String.Format("UPDATE[dbo].[Game] SET {0} = '{1}' WHERE Name='{2}'", column, value, name);
        }
    }

    public class Delete
    {
        public string DeleteFromGame(string name)
        {
            return String.Format("DELETE FROM[dbo].[Game] WHERE Name ='{0}'", name);
        }
    } 
}
