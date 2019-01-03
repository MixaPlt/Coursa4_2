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
    class AddMapFirstPage
    {
        Canvas mainCanvas;
        Window mainWindow;

        Button backButton;
        Button nextButton;

        Label enterNameLabel;
        Label enterHeight;
        Label enterWidth;
        Label enterDescription;

        TextBox nameBox;
        TextBox heightBox;
        TextBox widthBox;

        TextBox descriptionBlock;

        public AddMapFirstPage(Window _mainWindow, Canvas _mainCanvas)
        {
            mainWindow = _mainWindow;
            mainCanvas = _mainCanvas;

            backButton = new Button() { Content = "Back" };
            mainCanvas.Children.Add(backButton);
            backButton.Click += back;

            nextButton = new Button() { Content = "Next" };
            mainCanvas.Children.Add(nextButton);
            nextButton.Click += nextClick;

            Thickness margin = new Thickness();
            enterNameLabel = new Label
            {
                Content = "Enter map's name", Width = 400, Height = 50,
                VerticalContentAlignment = VerticalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                FontSize = 25
            };
            mainCanvas.Children.Add(enterNameLabel);

            margin.Top += 50;
            nameBox = new TextBox()
            {
                Margin = margin, Height = 50, Width = 400,
                VerticalContentAlignment = VerticalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                FontSize = 25
            };
            mainCanvas.Children.Add(nameBox);

            margin.Top += 50;
            enterHeight = new Label()
            {
                Content = "Enter height",
                Margin = margin,
                Height = 50,
                Width = 200,
                VerticalContentAlignment = VerticalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                FontSize = 25
            };
            mainCanvas.Children.Add(enterHeight);

            margin.Left += 200;
            enterWidth = new Label
            {
                Content = "Enter width",
                Margin = margin,
                Width = 200,
                Height = 50,
                VerticalContentAlignment = VerticalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                FontSize = 25
            };
            mainCanvas.Children.Add(enterWidth);

            margin.Top += 50;
            widthBox = new TextBox()
            {
                Margin = margin,
                Height = 50,
                Width = 200,
                VerticalContentAlignment = VerticalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                FontSize = 25
            };
            mainCanvas.Children.Add(widthBox);

            margin.Left -= 200;
            heightBox = new TextBox()
            {
                Margin = margin,
                Height = 50,
                Width = 200,
                VerticalContentAlignment = VerticalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                FontSize = 25
            };
            mainCanvas.Children.Add(heightBox);

            margin.Top += 50;
            enterDescription = new Label()
            {
                Content = "Add map's description",
                Margin = margin,
                Width = 400,
                Height = 50,
                VerticalContentAlignment = VerticalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                FontSize = 25
            };
            mainCanvas.Children.Add(enterDescription);

            margin.Top += 50;
            descriptionBlock = new TextBox
            {
                Background = Brushes.LightGray,
                Margin = margin,
                Width = 400,
                Height = 170,
                FontSize = 25,
                AcceptsReturn = true,
                TextWrapping = TextWrapping.Wrap
            };
            mainCanvas.Children.Add(descriptionBlock);

            mainWindow.SizeChanged += windowSizeChanged;
            windowSizeChanged(null, null);
        }

        void windowSizeChanged(object sender, EventArgs e)
        {
            double containerHeight = mainCanvas.ActualHeight;
            double containerWidth = mainCanvas.ActualWidth;
            double buttonFontSize = Math.Min(containerHeight / 20, containerWidth / 15);

            Thickness margin = new Thickness();
            margin.Top = containerHeight * 0.9;
            backButton.Margin = margin;
            backButton.FontSize = buttonFontSize;
            backButton.Height = containerHeight / 10;
            backButton.Width = containerWidth / 2;

            margin.Left += containerWidth / 2;
            nextButton.Margin = margin;
            nextButton.FontSize = buttonFontSize;
            nextButton.Height = containerHeight / 10;
            nextButton.Width = containerWidth / 2;
        }

        void back(object sender, EventArgs e)
        {
            removeDependence();
            MapManager mapManager = new MapManager(mainWindow, mainCanvas);
        }

        void nextClick(object sender, EventArgs e)
        {
            
            try
            {
                AddMapSecondPage addMapSecondPage = new AddMapSecondPage(
                    mainWindow, mainCanvas, nameBox.Text, Int32.Parse(heightBox.Text),
                    Int32.Parse(widthBox.Text), descriptionBlock.Text);
                removeDependence();
            }
            catch
            {
                MessageBox.Show("Incorrect data. Try again");
            }
        }

        void removeDependence()
        {
            mainWindow.SizeChanged -= windowSizeChanged;
            mainCanvas.Children.Remove(backButton);
            mainCanvas.Children.Remove(nextButton);
            mainCanvas.Children.Remove(enterNameLabel);
            mainCanvas.Children.Remove(enterWidth);
            mainCanvas.Children.Remove(enterHeight);
            mainCanvas.Children.Remove(enterDescription);
            mainCanvas.Children.Remove(heightBox);
            mainCanvas.Children.Remove(widthBox);
            mainCanvas.Children.Remove(descriptionBlock);
            mainCanvas.Children.Remove(nameBox);
        }
    }
}
