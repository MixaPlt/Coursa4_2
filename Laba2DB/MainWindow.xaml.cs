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

        public MainWindow()
        {
            InitializeComponent();
            mainWindow.SizeChanged += windowSizeChanged;
            MainMenu mainMenu = new MainMenu(mainWindow, mainCanvas);
            
        }

        void windowSizeChanged(object sender, EventArgs e)
        {
            

        }
    }
}
