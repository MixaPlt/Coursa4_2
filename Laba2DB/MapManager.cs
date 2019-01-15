using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Laba2DB
{
    class MapManager
    {
        Window mainWindow;
        Canvas mainCanvas;

        Button backButton;
        Button addNewMapButton;

        DataGrid mapsDataGrid;

        List<Map> maps;

        public MapManager(Window _mainWindow, Canvas _mainCanvas)
        {
            mainWindow = _mainWindow;
            mainCanvas = _mainCanvas;

            maps = Map.readAllMaps(MainWindow.connectionString);
            foreach(Map map in maps)
            {
                map.loadAuthor(MainWindow.connectionString);
            }
            MessageBox.Show(maps.Count.ToString());

            mapsDataGrid = new DataGrid() { IsReadOnly = true };
            mapsDataGrid.ItemsSource = maps;
            mainCanvas.Children.Add(mapsDataGrid);
            
            backButton = new Button() { Content = "Back" };
            mainCanvas.Children.Add(backButton);
            backButton.Click += back;

            addNewMapButton = new Button() { Content = "Add new map" };
            mainCanvas.Children.Add(addNewMapButton);
            addNewMapButton.Click += addNewMapClick;

            mainWindow.SizeChanged += windowSizeChanged;
            windowSizeChanged(null, null);
        }

        void windowSizeChanged(object sender, EventArgs e)
        {
            double containerHeight = mainCanvas.ActualHeight;
            double containerWidth = mainCanvas.ActualWidth;
            double buttonFontSize = Math.Min(containerHeight / 20, containerWidth / 15);
            mapsDataGrid.FontSize = 15.8;
            Thickness margin = new Thickness();

            mapsDataGrid.Width = containerWidth;

            margin.Top = containerHeight * 0.9;
            backButton.Margin = margin;
            backButton.Width = containerWidth / 2;
            backButton.Height = containerHeight / 10;
            backButton.FontSize = buttonFontSize;

            margin.Left = containerWidth / 2;
            addNewMapButton.Margin = margin;
            addNewMapButton.Height = containerHeight / 10;
            addNewMapButton.Width = containerWidth / 2;
            addNewMapButton.FontSize = buttonFontSize;
        }

        void back(object sender, EventArgs e)
        {
            removeDependence();
            MainMenu mainMenu = new MainMenu(mainWindow, mainCanvas);
        }

        void addNewMapClick(object sender, EventArgs e)
        {
            removeDependence();
            AddMapFirstPage addMapFirstPage = new AddMapFirstPage(mainWindow, mainCanvas);
        }

        void removeDependence()
        {
            mainWindow.SizeChanged -= windowSizeChanged;
            mainCanvas.Children.Clear();
        }
    }
}
