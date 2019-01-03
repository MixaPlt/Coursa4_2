using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Data;

namespace Laba2DB
{
    class UserManagerMenu
    {
        Canvas mainCanvas;
        Window mainWindow;
        DataGrid userInfoGrid;
        Button backButton;
        Button addNewUserButton;

        Grid[] items;

        KypcachDataSetTableAdapters.UserInfoTableAdapter userInfoTableAdapter;

        public UserManagerMenu(Window _mainWindow, Canvas _mainCanvas)
        {
            mainCanvas = _mainCanvas;
            mainWindow = _mainWindow;
            userInfoTableAdapter = new KypcachDataSetTableAdapters.UserInfoTableAdapter();

            userInfoGrid = new DataGrid();
            userInfoGrid.ItemsSource = User.loadUsers();
            userInfoGrid.Background = Brushes.Gray;
            userInfoGrid.IsReadOnly = true;
            userInfoGrid.MouseDoubleClick += tableDoubleClick;
            mainCanvas.Children.Add(userInfoGrid);

            backButton = new Button() { Content = "Back" };
            mainCanvas.Children.Add(backButton);
            backButton.Click += back;

            addNewUserButton = new Button() { Content = "Add new user" };
            mainCanvas.Children.Add(addNewUserButton);
            addNewUserButton.Click += addNewUserClick;

            mainWindow.SizeChanged += windowSizeChanged;
            windowSizeChanged(mainWindow, null);
        }

        void tableDoubleClick(object sender, EventArgs e)
        {
            int selectedId = ((KypcachDataSet.UserInfoRow)((DataRowView)userInfoGrid.Items[userInfoGrid.SelectedIndex]).Row).Id;
            removeDependence();
            UserProfile userProfile = new UserProfile(mainWindow, mainCanvas, selectedId);
        }

        void windowSizeChanged(object sender, EventArgs e)
        {
            double containerHeight = mainCanvas.ActualHeight;
            double containerWidth = mainCanvas.ActualWidth;
            double buttonFontSize = Math.Min(containerHeight / 20, containerWidth / 15);
            Thickness margin = new Thickness();
            userInfoGrid.Height = containerHeight * 0.9;
            userInfoGrid.FontSize = 24.5;

            margin.Top = containerHeight * 0.9;
            backButton.Margin = margin;
            backButton.Width = containerWidth / 2;
            backButton.Height = containerHeight / 10;
            backButton.FontSize = buttonFontSize;

            margin.Left = containerWidth / 2;
            addNewUserButton.Margin = margin;
            addNewUserButton.Height = containerHeight / 10;
            addNewUserButton.Width = containerWidth / 2;
            addNewUserButton.FontSize = buttonFontSize;
        }

        void back(object sender, EventArgs e)
        {
            removeDependence();
            MainMenu mainMenu = new MainMenu(mainWindow, mainCanvas);
        }

        void removeDependence()
        {
            mainWindow.SizeChanged -= windowSizeChanged;
            mainCanvas.Children.Clear();
        }

        void addNewUserClick(object sender, EventArgs e)
        {
            removeDependence();
            AddNewUserMenu addNewUserMenu = new AddNewUserMenu(mainWindow, mainCanvas);
        }
    }
}
