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
    class AddMapSecondPage
    {
        Window mainWindow;
        Canvas mainCanvas;
        String mapName;
        int mapHeight;
        int mapWidth;
        String mapDescription;

        Button backButton;
        Button confirmButton;

        TextBox[, ] field;

        public AddMapSecondPage(Window _mainWindow, Canvas _mainCanvas, String _mapName, int _mapHeight, int _mapWidth, String _mapDescription)
        {
            mainWindow = _mainWindow;
            mainCanvas = _mainCanvas;
            mapName = _mapName;
            mapHeight = _mapHeight;
            mapWidth = _mapWidth;
            mapDescription = _mapDescription;

            backButton = new Button() { Content = "Back" };
            mainCanvas.Children.Add(backButton);
            backButton.Click += backClick;

            confirmButton = new Button() { Content = "Confirm" };
            mainCanvas.Children.Add(confirmButton);
            confirmButton.Click += confirmClick;

            field = new TextBox[mapHeight, mapWidth];
            for(int i = 0; i < mapHeight; ++i)
            {
                for(int j = 0; j < mapWidth; ++j)
                {
                    field[i, j] = new TextBox() { Text = "0", Background = Brushes.Beige, MaxLength = 1 };
                    mainCanvas.Children.Add(field[i, j]);
                }
            }

            mainWindow.SizeChanged += windowSizeChanged;
            windowSizeChanged(null, null);
        }

        void windowSizeChanged(object sender, EventArgs e)
        {
            double containerHeight = mainCanvas.ActualHeight;
            double containerWidth = mainCanvas.ActualWidth;
            double buttonFontSize = Math.Min(containerHeight / 20, containerWidth / 15);

            double cellHeight = containerHeight * 0.9 / mapHeight;
            double cellWidth = containerWidth / mapWidth;
            double cellFontSize = Math.Min(cellHeight, cellWidth) / 2;

            Thickness margin = new Thickness() { Top = containerHeight * 0.9 };

            backButton.Margin = margin;
            backButton.Height = containerHeight / 10;
            backButton.Width = containerWidth / 2;
            backButton.FontSize = buttonFontSize;

            margin.Left += containerWidth / 2;
            confirmButton.Margin = margin;
            confirmButton.Height = containerHeight / 10;
            confirmButton.Width = containerWidth / 2;
            confirmButton.FontSize = buttonFontSize;

            margin.Top = 0;
            for(int i = 0; i < mapHeight; ++i)
            {
                margin.Left = 0;
                for (int j = 0; j < mapWidth; ++j)
                {
                    field[i, j].Margin = margin;
                    field[i, j].FontSize = cellFontSize;
                    field[i, j].Height = cellHeight;
                    field[i, j].Width = cellWidth;
                    margin.Left += cellWidth;
                }
                margin.Top += cellHeight;
            }
        }

        void backClick(object sender, EventArgs e)
        {
            removeDependence();
            AddMapFirstPage firstPage = new AddMapFirstPage(mainWindow, mainCanvas);
        }

        void confirmClick(object sender, EventArgs e)
        {
            string queryString = "INSERT INTO [dbo].[MapInfo] (Name, AuthorId, Description, Height, Width) OUTPUT inserted.Id VALUES('" + mapName + "', "+ MainWindow.authorizedId.ToString() +", '" + mapDescription + "', " + mapHeight.ToString() + ", " + mapWidth.ToString() + ");";
            SqlConnection conn = new SqlConnection(MainWindow.connectionString);
            conn.Open();
            SqlCommand command = new SqlCommand(queryString, conn);
            int inserted_id = (int)command.ExecuteScalar();
            
            removeDependence();
            KypcachDataSetTableAdapters.MapInfoTableAdapter mapInfoAdapter = new KypcachDataSetTableAdapters.MapInfoTableAdapter();
            KypcachDataSetTableAdapters.CellTableAdapter cellInfoAdapter = new KypcachDataSetTableAdapters.CellTableAdapter();
            mapInfoAdapter.InsertMap(mapName, 1, mapDescription, mapHeight, mapWidth);
            string astring = "";
            for(int i = 0; i < mapHeight; ++i)
            {
                for(int j = 0; j < mapWidth; ++j)
                {
                    queryString = "INSERT INTO [dbo].[Cell] Values('" + field[i, j].Text + "', " + i.ToString() + ", " + j.ToString() + ", " + inserted_id.ToString() + ");";
                    SqlCommand comm = new SqlCommand(queryString, conn);
                    comm.ExecuteNonQuery();
                    astring += queryString + '\n';
                    cellInfoAdapter.Insert(field[i, j].Text, i, j, inserted_id);
                }
            }
            conn.Close();
            backClick(null, null);
        }

        void removeDependence()
        {
            mainWindow.SizeChanged -= windowSizeChanged;
            mainCanvas.Children.Clear();
        }
    }
}
