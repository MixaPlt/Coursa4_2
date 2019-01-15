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
using System.Windows.Shapes;

namespace Laba2DB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Projects\\DBCourse\\Laba2DB\\Kypcach.mdf;Integrated Security = True";
        public static int authorizedId = -1;
        Label loginLabel;
        TextBox loginBox;
        Button submitButton;
        Label passwordLabel;
        PasswordBox passwordBox;
        public MainWindow()
        {
            InitializeComponent();

            loginLabel = new Label() { Content = "Login", HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center };
            mainCanvas.Children.Add(loginLabel);

            loginBox = new TextBox() { HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center };
            mainCanvas.Children.Add(loginBox);

            passwordLabel = new Label() { Content = "Password", HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center };
            mainCanvas.Children.Add(passwordLabel);

            passwordBox = new PasswordBox() { HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center };
            mainCanvas.Children.Add(passwordBox);

            submitButton = new Button() { Content = "Submit" };

            mainCanvas.Children.Add(submitButton);
            submitButton.Click += submitClick;
            mainWindow.SizeChanged += windowSizeChanged;
            if (mainWindow.IsLoaded)
                windowSizeChanged(null, null);
        }

        void windowSizeChanged(object sender, EventArgs e)
        {
            double containerHeight = mainCanvas.ActualHeight;
            double containerWidth = mainCanvas.ActualWidth;
            double fontSize = Math.Min(containerHeight / 15, containerWidth / 15);
            double buttonHeight = Math.Min(containerHeight, containerWidth) / 7;
            double buttonWidth = buttonHeight * 4;
            Thickness margin = new Thickness();
            margin.Left = (containerWidth - buttonWidth) / 2;
            margin.Top = (containerHeight - buttonHeight * 5) / 2;

            loginLabel.Margin = margin;
            loginLabel.FontSize = fontSize;
            loginLabel.Width = buttonWidth;
            loginLabel.Height = buttonHeight;

            margin.Top += buttonHeight;
            loginBox.Height = buttonHeight;
            loginBox.Width = buttonWidth;
            loginBox.Margin = margin;
            loginBox.FontSize = fontSize;

            margin.Top += buttonHeight;
            passwordLabel.Margin = margin;
            passwordLabel.FontSize = fontSize;
            passwordLabel.Width = buttonWidth;
            passwordLabel.Height = buttonHeight;

            margin.Top += buttonHeight;
            passwordBox.Height = buttonHeight;
            passwordBox.Width = buttonWidth;
            passwordBox.Margin = margin;
            passwordBox.FontSize = fontSize;

            margin.Top += buttonHeight;
            submitButton.Margin = margin;
            submitButton.Height = buttonHeight;
            submitButton.Width = buttonWidth;
            submitButton.FontSize = fontSize;
        }

        void submitClick(object sender, EventArgs e)
        {
            try
            {
                List<User> users = User.loadUsersByName(loginBox.Text, connectionString);
                foreach (User user in users)
                {
                    if(user.password == passwordBox.Password)
                    {
                        authorizedId = user.id;
                    }
                }
                if (authorizedId >= 0)
                {
                    removeDependencies();
                    MainMenu mainMenu = new MainMenu(mainWindow, mainCanvas);
                }
                else
                {
                    MessageBox.Show("Incorrect login or password\nTry again");
                }
            }
            catch { MessageBox.Show("Incorrect login or password\nTry again!"); }
        }

        void removeDependencies()
        {
            mainWindow.SizeChanged -= windowSizeChanged;
            mainCanvas.Children.Clear();
        }
    }
}
