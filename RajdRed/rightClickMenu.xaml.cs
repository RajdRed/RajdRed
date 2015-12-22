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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RajdRed
{
    /// <summary>
    /// Interaction logic for rightClickMenu.xaml
    /// </summary>
    public partial class RightClickMenu : UserControl
    {
        private MainWindow _mainWindow;
        private Canvas canvas;
        private bool onField = false;
        public RightClickMenu(MainWindow w)
        {
            InitializeComponent();
            _mainWindow = w;
            canvas = w.getCanvas();            
            MouseRightButtonDown += RightClickMenu_MouseRightButtonDown;
            canvas.Children.Add(this);
        }
        public void RightClickMenu_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            //CaptureMouse();
            //Delete();            
        }
        private void Delete()
        {
            _mainWindow.DeleteKlass(this);
        }
    }
}
