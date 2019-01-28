using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.IO;

namespace Laba2DB
{
    class MainMenu
    {
        Canvas mainCanvas;
        Window mainWindow;
        Button usersManagerButton;
        Button mapsManagerButton;
        Button zvitButton;

        public MainMenu(Window _mainWindow, Canvas _mainCanvas)
        {
            mainWindow = _mainWindow;
            mainCanvas = _mainCanvas;

            usersManagerButton = new Button() { Content = "Manage users" };
            mainCanvas.Children.Add(usersManagerButton);
            usersManagerButton.Click += userManagerClick;

            mapsManagerButton = new Button() { Content = "Manage maps" };
            mainCanvas.Children.Add(mapsManagerButton);
            mapsManagerButton.Click += mapManagerClick;

            zvitButton = new Button() { Content = "Today's users" };
            mainCanvas.Children.Add(zvitButton);

            mainWindow.SizeChanged += windowSizeChanged;
            if (mainWindow.IsLoaded)
                windowSizeChanged(null, null);
        }

        void windowSizeChanged(object sender, EventArgs e)
        {
            double containerHeight = mainCanvas.ActualHeight;
            double containerWidth = mainCanvas.ActualWidth;
            double buttonHeight = Math.Min(containerHeight, containerWidth) / 5;
            double buttonWidth = buttonHeight * 3;
            double fontSize = buttonHeight / 3.5;
            Thickness margin = new Thickness();

            margin.Top = (containerHeight - buttonHeight * 3) / 2;
            margin.Left = (containerWidth - buttonWidth) / 2;

            usersManagerButton.Margin = margin;
            usersManagerButton.Height = buttonHeight;
            usersManagerButton.Width = buttonWidth;
            usersManagerButton.FontSize = fontSize;

            margin.Top += buttonHeight;
            mapsManagerButton.Margin = margin;
            mapsManagerButton.Height = buttonHeight;
            mapsManagerButton.Width = buttonWidth;
            mapsManagerButton.FontSize = fontSize;

            margin.Top += buttonHeight;
            zvitButton.Margin = margin;
            zvitButton.Height = buttonHeight;
            zvitButton.Width = buttonWidth;
            zvitButton.FontSize = fontSize * 0.9;
            zvitButton.Click += zvitClick;
        }

        void userManagerClick(object sender, EventArgs e)
        {
            removeDependence();
            UserManagerMenu userManagerMenu = new UserManagerMenu(mainWindow, mainCanvas);
        }

        void mapManagerClick(object sender, EventArgs e)
        {
            removeDependence();
            MapManager mapManager = new MapManager(mainWindow, mainCanvas);
        }

        void zvitClick(object sender, EventArgs e)
        {
            List<User> list = new List<User>();
            string queryString = "SELECT * FROM [dbo].[UserInfo] WHERE Registration_date = CONVERT(date, GETDATE());";
            SqlConnection connection = new SqlConnection(MainWindow.connectionString);

            connection.Open();
            SqlCommand command = new SqlCommand(queryString, connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new User(Int32.Parse(reader["id"].ToString()),
                    reader["Name"].ToString(),
                    Int32.Parse(reader["Permission"].ToString()),
                    Int32.Parse(reader["Expereance"].ToString()),
                    new Date(reader["Registration_date"].ToString())
                    ));

                list[list.Count - 1].password = reader["Password"].ToString();
            }
            connection.Close();
            StreamWriter sw = new StreamWriter("report.txt");
            foreach(User user in list)
            {
                sw.WriteLine(user.name.ToString());
            }
            sw.Close();
            MessageBox.Show("Report saved in file report.txt");
        }

        void removeDependence()
        {
            mainWindow.SizeChanged -= windowSizeChanged;
            mainCanvas.Children.Clear();
        }

        ~MainMenu()
        {
            
        }
    }
}
