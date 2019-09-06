using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SQLProject;

namespace WPFSQLApplication
{
    public partial class MainWindow : Window
    {
        SQLProject.Application application;
        SQLProject.Select select;
        SQLProject.Insert insert;
        SQLProject.Update update;
        SQLProject.Delete delete;
        List<TextBox> textBoxes;
        public MainWindow()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            application = new SQLProject.Application();
            select = new SQLProject.Select();
            insert = new SQLProject.Insert();
            update = new SQLProject.Update();
            delete = new SQLProject.Delete();

            textBoxes = new List<TextBox>();
            textBoxes.Add(nameField);
            textBoxes.Add(genreField);
            textBoxes.Add(typeField);
            textBoxes.Add(reviewField);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SQL_STATEMENT option = (SQL_STATEMENT)Enum.ToObject(typeof(SQL_STATEMENT), SqlOption.SelectedIndex);
            Game gameObject = new Game(nameField.Text, genreField.Text, typeField.Text, reviewField.Text);
            List<string> result;

            switch (option)
            {
                case SQL_STATEMENT.INSERT:
                    application.RunSQL(insert.InsertIntoGame(gameObject));
                    break;
                case SQL_STATEMENT.UPDATE:
                    application.RunSQL(update.UpdateRowByName(genreField.Text, typeField.Text, nameField.Text));
                    break;
                case SQL_STATEMENT.SELECT:
                    application.RunSQL(select.SelectAll(), out result);
                    if(result != null)
                    PrintToListBox(result);
                    break;
                case SQL_STATEMENT.DELETE:
                    application.RunSQL(delete.DeleteFromGame(nameField.Text));
                    break;
                default:
                    break;
            }
            ClearTextBoxes();
        }

        private void PrintToListBox(List<string> list)
        {
            if (list == null)
                return;
            listBox.Items.Clear();
            string builder = application.PrintRow("Game", "Genre", "Type", "Review");
            listBox.Items.Add(builder);
            foreach (string item in list)
            listBox.Items.Add(item);
        }

        private void ClearTextBoxes()
        {
            foreach (TextBox tb in textBoxes)
                tb.Clear();
        }

        private void DisableTextBoxes()
        {
            foreach (TextBox tb in textBoxes)
                tb.IsEnabled = false;
        }

        private void EnableTextBoxes()
        {
            foreach (TextBox tb in textBoxes)
                tb.IsEnabled = true;
        }

        private void SqlOption_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            SQL_STATEMENT option = (SQL_STATEMENT)Enum.ToObject(typeof(SQL_STATEMENT), cb.SelectedIndex);
            switch (option)
            {
                case SQL_STATEMENT.INSERT:
                    EnableTextBoxes();
                    break;
                case SQL_STATEMENT.UPDATE:
                    EnableTextBoxes();
                    nameField.Text = "Game name";
                    genreField.Text = "Column to update";
                    typeField.Text = "Value";
                    reviewField.Clear();
                    reviewField.IsEnabled = false;
                    break;
                case SQL_STATEMENT.SELECT:
                    DisableTextBoxes();
                    break;
                case SQL_STATEMENT.DELETE:
                    EnableTextBoxes();
                    break;
                default:
                    break;
            }
        }
    }

    public enum SQL_STATEMENT
    {
        INSERT, UPDATE, SELECT, DELETE
    }
}
