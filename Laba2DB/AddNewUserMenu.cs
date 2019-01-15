using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Data.SqlClient;

namespace Laba2DB
{
    class AddNewUserMenu
    {
        Window mainWindow;
        Canvas mainCanvas;

        Button backButton;
        Button confirmButton;

        Label enterName;
        Label enterPassword;
        Label enterPermission;

        TextBox nameBox;
        TextBox passwordBox;
        TextBox permissionBox;

        public AddNewUserMenu(Window _mainWindow, Canvas _mainCanvas)
        {
            mainWindow = _mainWindow;
            mainCanvas = _mainCanvas;

            backButton = new Button() { Content = "Back" };
            mainCanvas.Children.Add(backButton);
            backButton.Click += back;

            confirmButton = new Button { Content = "Confirm" };
            mainCanvas.Children.Add(confirmButton);
            confirmButton.Click += confirmClick;

            enterName = new Label() { Content = "Enter user's name" };
            mainCanvas.Children.Add(enterName);

            enterPassword = new Label() { Content = "Enter password" };
            mainCanvas.Children.Add(enterPassword);

            enterPermission = new Label() { Content = "Enter user's\npermission level" };
            mainCanvas.Children.Add(enterPermission);

            nameBox = new TextBox();
            mainCanvas.Children.Add(nameBox);
            nameBox.Background = Brushes.LightGreen;
            nameBox.VerticalContentAlignment = VerticalAlignment.Center;

            passwordBox = new TextBox();
            mainCanvas.Children.Add(passwordBox);
            passwordBox.Background = Brushes.LightGreen;
            passwordBox.VerticalContentAlignment = VerticalAlignment.Center;

            permissionBox = new TextBox();
            mainCanvas.Children.Add(permissionBox);
            permissionBox.Background = Brushes.LightGreen;
            permissionBox.VerticalContentAlignment = VerticalAlignment.Center;

            mainWindow.SizeChanged += windowSizeChanged;
            windowSizeChanged(mainWindow, null);
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

            margin.Left = containerWidth / 2;
            confirmButton.Margin = margin;
            confirmButton.FontSize = buttonFontSize;
            confirmButton.Height = containerHeight / 10;
            confirmButton.Width = containerWidth / 2;

            margin.Top = containerHeight / 10;
            margin.Left = 0;
            enterName.Margin = margin;

            margin.Top += containerHeight  / 10;
            enterPassword.Margin = margin;

            margin.Top += containerHeight  / 10;
            enterPermission.Margin = margin;

            margin.Top = containerHeight / 10;
            margin.Left = containerWidth / 3;
            nameBox.Margin = margin;
            nameBox.Height = containerHeight / 18;
            nameBox.Width = containerWidth / 3;

            margin.Top += containerHeight / 10;
            passwordBox.Margin = margin;
            passwordBox.Height = containerHeight / 18;
            passwordBox.Width = containerWidth / 3;

            margin.Top += containerHeight / 10;
            permissionBox.Margin = margin;
            permissionBox.Height = containerHeight / 18;
            permissionBox.Width = containerWidth / 3;
        }

        void confirmClick(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(MainWindow.connectionString);
            conn.Open();
            SqlCommand command = new SqlCommand("INSERT INTO [dbo].[UserInfo] (Name, Password, Permission, Expereance) VALUES('" + nameBox.Text.ToString() + "', '" + passwordBox.Text + "', " + permissionBox.Text + ", 0)", conn);
            command.ExecuteNonQuery();
            conn.Close();

            back(null, null);
        }

        void back(object sender, EventArgs e)
        {
            removeDependence();
            UserManagerMenu userManagerMenu = new UserManagerMenu(mainWindow, mainCanvas);
        }

        void removeDependence()
        {
            mainWindow.SizeChanged -= windowSizeChanged;
            mainCanvas.Children.Clear();
        }
    }
}
