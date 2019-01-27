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
    class MapPresentation
    {
        Window mainWindow;
        Canvas mainCanvas;
        Map map;
        Button backButton;
        Button changeButton;
        Button[,] cells;
        Button deleteButton;
        public MapPresentation(int _mapId, Window _mainWindow, Canvas _mainCanvas)
        {
            map = new Map(_mapId, MainWindow.connectionString);
            map.loadField(MainWindow.connectionString);
            map.loadAuthor(MainWindow.connectionString);

            mainWindow = _mainWindow;
            mainCanvas = _mainCanvas;

            cells = new Button[map.Height, map.Width];
            for(int i = 0; i < map.Height; ++i)
            {
                for (int j = 0; j < map.Width; ++j)
                {
                    cells[i, j] = new Button() { Content = map.field[i, j], Background = Brushes.Azure };
                    mainCanvas.Children.Add(cells[i, j]);
                }
            }

            backButton = new Button() { Content = "Back" };
            mainCanvas.Children.Add(backButton);
            backButton.Click += backClick;

            deleteButton = new Button() { Content = "Delete map", Foreground = Brushes.Red };
            mainCanvas.Children.Add(deleteButton);
            deleteButton.Click += deleteClick;

            mainWindow.SizeChanged += windowSizeChanged;
            windowSizeChanged(null, null);
        }

        public void windowSizeChanged(object sender, EventArgs e)
        {
            double containerHeight = mainCanvas.ActualHeight;
            double containerWidth = mainCanvas.ActualWidth;
            double buttonFontSize = Math.Min(containerHeight / 20, containerWidth / 15);

            double cellHeight = containerHeight * 0.9 / map.Height;
            double cellWidth = containerWidth / map.Width;
            double cellFontSize = Math.Min(cellHeight, cellWidth) / 2;

            Thickness margin = new Thickness();
            for(int i = 0; i < map.Height; ++i)
            {
                for(int j = 0; j < map.Width; ++j)
                {
                    cells[i, j].Margin = margin;
                    margin.Left += cellWidth;
                    cells[i, j].Height = cellHeight;
                    cells[i, j].Width = cellWidth;
                    cells[i, j].FontSize = buttonFontSize;
                }
                margin.Left = 0;
                margin.Top += cellHeight;
            }

            margin.Top = containerHeight * 0.9;
            margin.Left = 0;
            backButton.Margin = margin;
            backButton.Height = containerHeight / 10;
            backButton.Width = containerWidth / 2;
            backButton.FontSize = buttonFontSize;

            margin.Left = containerWidth / 2;
            deleteButton.Margin = margin;
            deleteButton.Height = containerHeight / 10;
            deleteButton.Width = containerWidth / 2;
            deleteButton.FontSize = buttonFontSize;
        }

        void deleteClick(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure?", "Confirm deltetion", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                Map.deleteById(map.Id, MainWindow.connectionString);
                backClick(null, null);
            }
        }

        void backClick(object sender, EventArgs e)
        {
            removeDependences();
            MapManager mapManager = new MapManager(mainWindow, mainCanvas);
        }

        void removeDependences()
        {
            mainWindow.SizeChanged -= windowSizeChanged;
            mainCanvas.Children.Clear();
        }
    }
}
