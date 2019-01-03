using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace Laba2DB
{
    class UserProfile
    {
        Canvas mainCanvas;
        Window mainWindow;
        Button backButton;
        Button confirmButton;

        Label nameLabel;
        Label levelLabel;
        Label expereanceLabel;

        TextBox nameBox;

        int userId;

        User user;

        public UserProfile(Window _mainWindow, Canvas _mainCanvas, int _userId)
        {

            mainWindow = _mainWindow;
            mainCanvas = _mainCanvas;
            userId = _userId;

            user = User.loadUserById(userId, MainWindow.connectionString);

            backButton = new Button() { Content = "Back" };
            mainCanvas.Children.Add(backButton);
            backButton.Click += backClick;

            confirmButton = new Button() { Content = "Confirm" };
            mainCanvas.Children.Add(confirmButton);
            confirmButton.Click += confirmClick;

            nameLabel = new Label() { Content = "Name: ", Foreground = Brushes.DarkOrange };
            mainCanvas.Children.Add(nameLabel);

            levelLabel = new Label() { Content = "Level: " + Math.Floor(Math.Sqrt(user.expereance)).ToString(), Foreground = Brushes.DarkOrange };
            mainCanvas.Children.Add(levelLabel);

            nameBox = new TextBox() { Text = user.name };
            mainCanvas.Children.Add(nameBox);

            mainWindow.SizeChanged += windowSizeChanged;
            windowSizeChanged(null, null);
        }

        void windowSizeChanged(object sender, EventArgs e)
        {
            double containerHeight = mainCanvas.ActualHeight;
            double containerWidth = mainCanvas.ActualWidth;
            double buttonFontSize = Math.Min(containerHeight / 20, containerWidth / 15);
            Thickness margin = new Thickness() { Top = containerHeight * 0.9 };

            backButton.Margin = margin;
            backButton.FontSize = buttonFontSize;
            backButton.Height = containerHeight / 10;
            backButton.Width = containerWidth / 2;

            margin.Left += containerWidth / 2;
            confirmButton.Margin = margin;
            confirmButton.Height = containerHeight / 10;
            confirmButton.Width = containerWidth / 2;
            confirmButton.FontSize = buttonFontSize;

            margin.Top = 0;
            margin.Left = 0;
            nameLabel.Margin = margin;
            nameLabel.FontSize = buttonFontSize;

            margin.Top += containerHeight / 10;
            levelLabel.Margin = margin;
            levelLabel.FontSize = buttonFontSize;

            margin.Top = nameLabel.Margin.Top + 10;
            margin.Left = containerWidth / 3;
            nameBox.FontSize = buttonFontSize;
            nameBox.Margin = margin;
        }

        void backClick(object sender, EventArgs e)
        {
            removeDependence();
            UserManagerMenu userManagerMenu = new UserManagerMenu(mainWindow, mainCanvas);
        }

        void confirmClick(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(MainWindow.connectionString);
            string queryString = "UPDATE [dbo].[UserInfo] SET Name = '" + nameBox.Text + "' WHERE ID = " + userId + ";";
            MessageBox.Show(queryString);
            conn.Open();
            SqlCommand command = new SqlCommand(queryString, conn);
            command.ExecuteNonQuery();
            conn.Close();
            backClick(null, null);
        }

        void removeDependence()
        {
            mainWindow.SizeChanged -= windowSizeChanged;

            backButton.Click -= backClick;
            backButton = null;

            mainCanvas.Children.Clear();
        }
    }
}
