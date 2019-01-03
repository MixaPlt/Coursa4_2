using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Laba2DB
{
    class MainMenu
    {
        Canvas mainCanvas;
        Window mainWindow;
        Button usersManagerButton;
        Button mapsManagerButton;
        Button gameSessionManageerButton;

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

            gameSessionManageerButton = new Button() { Content = "Manage game sessions" };
            mainCanvas.Children.Add(gameSessionManageerButton);

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
            gameSessionManageerButton.Margin = margin;
            gameSessionManageerButton.Height = buttonHeight;
            gameSessionManageerButton.Width = buttonWidth;
            gameSessionManageerButton.FontSize = fontSize * 0.9;
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
