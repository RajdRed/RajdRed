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
    /// Interaction logic for Klass.xaml
    /// </summary>
    public partial class Klass : UserControl
    {
        private MainWindow _mainWindow;
        private Canvas _canvas;
        private bool _onField = false;
        private bool _isSelected = false;
        private Point _posOfMouseOnHit;
        private Point _posOfShapeOnHit;

        public List<Nod> _noder = new List<Nod>();
        

        public Klass(MainWindow w, Point pt)
        {
            InitializeComponent();

            _mainWindow = w;
            _canvas = w.GetCanvas();

			setKlassColors();

            Canvas.SetZIndex(this, 1);
			Canvas.SetLeft(this, pt.X - 50);
			Canvas.SetTop(this, pt.Y - 10);

            MouseMove += Klass_MouseMove;
            MouseUp += Klass_MouseUp;
            SizeChanged += Klass_SizeChanged;

            _canvas.Children.Add(this);

            createNodes();
        }

		public MainWindow GetMainWindow()
		{
			return _mainWindow;
		}

        /// <summary>
        /// Skapar alla noder tillhörande klassen
        /// </summary>
        private void createNodes()
        {   
            int[] intervall = new int[4] { 20, 40, 60, 80};
            //Left
            _noder.Add(new Nod(this, OnSide.Left, new Point(0, intervall[0])));
            _noder.Add(new Nod(this, OnSide.Left, new Point(0, intervall[1])));
            _noder.Add(new Nod(this, OnSide.Left, new Point(0, intervall[2])));
            _noder.Add(new Nod(this, OnSide.Left, new Point(0, intervall[3])));

            //Right
            _noder.Add(new Nod(this, OnSide.Right, new Point(MinWidth, intervall[0])));
            _noder.Add(new Nod(this, OnSide.Right, new Point(MinWidth, intervall[1])));
            _noder.Add(new Nod(this, OnSide.Right, new Point(MinWidth, intervall[2])));
            _noder.Add(new Nod(this, OnSide.Right, new Point(MinWidth, intervall[3])));
            
            //Top
            _noder.Add(new Nod(this, OnSide.Top, new Point(intervall[0], 0)));
            _noder.Add(new Nod(this, OnSide.Top, new Point(intervall[1], 0)));
            _noder.Add(new Nod(this, OnSide.Top, new Point(intervall[2], 0)));
            _noder.Add(new Nod(this, OnSide.Top, new Point(intervall[3], 0)));
            
            //Bottom
            _noder.Add(new Nod(this, OnSide.Bottom, new Point(intervall[0], MinHeight)));
            _noder.Add(new Nod(this, OnSide.Bottom, new Point(intervall[1], MinHeight)));
            _noder.Add(new Nod(this, OnSide.Bottom, new Point(intervall[2], MinHeight)));
            _noder.Add(new Nod(this, OnSide.Bottom, new Point(intervall[3], MinHeight)));

            foreach (Nod nod in _noder) {
                NodeGrid.Children.Add(nod);
            }
        }

        /// <summary>
        /// Händer när användaren klickar ned på Klassen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Klass_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Cursor = Cursors.SizeAll;

            if (e.ClickCount == 2)
            {
                if (_onField)
                {
                    openSettingsMenu(e);
                }
            }
            else
            {
                CaptureMouse();
                Point pt = e.GetPosition(_canvas);

                _isSelected = true;
                _canvas.Children.Remove(this);
                _canvas.Children.Add(this);
                _posOfMouseOnHit = pt;
                _posOfShapeOnHit.X = Canvas.GetLeft(this);
                _posOfShapeOnHit.Y = Canvas.GetTop(this);
            }
        }

        /// <summary>
        /// När storlek på klass ändras, ändra förhållandet mellan noder.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Klass_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            foreach (var nod in _noder)
            {
                nod.SetPositionWithMargin();
                nod.UpdateLinjePosition();
            }
        }


        /// <summary>
        /// Öppnar egenskapsfönster
        /// </summary>
        /// <param name="e"></param>
        private void openSettingsMenu(MouseEventArgs e)
        {
            Grid g = new Grid() { Width = _canvas.ActualWidth, Height = _canvas.ActualHeight, Background = Brushes.Black, Opacity = 0.2 };
            Canvas.SetLeft(g, 0);
            Canvas.SetTop(g, 0);

            ClassSettings cs = new ClassSettings(this, g);
			g.MouseDown += (sender, eventArgs) => { CloseSettings(cs, g); };	//Skapar ett mouseDown-event för Grid g som anropar CloseSettings

            Point posOnCanvas = e.GetPosition(_canvas) - _posOfMouseOnHit + _posOfShapeOnHit;
            double x = (posOnCanvas.X + ActualWidth / 2) - cs.Width / 2;
            double y = (posOnCanvas.Y + ActualHeight / 2) - cs.Height / 2;

            if (cs.Width + x > _mainWindow.ActualWidth)
                Canvas.SetLeft(cs, x - (x + cs.Width - _mainWindow.ActualWidth));
            else if (x < 0)
                Canvas.SetLeft(cs, x - x);
            else
                Canvas.SetLeft(cs, x);

            if (cs.Height + y > _mainWindow.ActualHeight)
                Canvas.SetTop(cs, y - (y + cs.Height - _mainWindow.ActualHeight));
            else if (y < 0)
                Canvas.SetTop(cs, y - y);
            else
                Canvas.SetTop(cs, y);

            _canvas.Children.Add(g);
            _canvas.Children.Add(cs);
        }

        /// <summary>
        /// Händer när en Klass flyttas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Klass_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsMouseCaptured && _isSelected)
            {
                Point pt = e.GetPosition(_canvas);
                Canvas.SetLeft(this, (pt.X - _posOfMouseOnHit.X) + _posOfShapeOnHit.X);
                Canvas.SetTop(this, (pt.Y - _posOfMouseOnHit.Y) + _posOfShapeOnHit.Y);

                Point posOnCanvas = pt - _posOfMouseOnHit + _posOfShapeOnHit;

                if (_onField && posOnCanvas.Y <= 100)
                    Canvas.SetTop(this, 100.1);

                if (posOnCanvas.X <= 0)
                    Canvas.SetLeft(this, 0.1);

                foreach (Nod n in _noder)
                {
                    n.UpdateLinjePosition();
                }
            }
        }

        /// <summary>
        /// Händer när användaren släpper musnedtryckning på Klass
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Klass_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ReleaseMouseCapture();

            if (_isSelected == true)
            {
                Point pt = e.GetPosition(_canvas);
                Point posOnCanvas = pt - _posOfMouseOnHit + _posOfShapeOnHit;

                if (posOnCanvas.Y <= 100 && !_onField)
                {
                    Delete();
                }
                else
                {
                    _onField = true;
                }
            }

            _isSelected = false;
        }

        /// <summary>
        /// Tar bort Klass
        /// </summary>
        public void Delete()
        {
            foreach (Nod n in _noder)
            {
                if (n.IsBindToLinje())
                {
                    n.resetNodFromKlass(); 
                }
            }
            _mainWindow.DeleteKlass(this);
            
        }

        /// <summary>
        /// Stänger egenskapsfönstret
        /// </summary>
        /// <param name="cs"></param>
        /// <param name="g"></param>
        public void CloseSettings(ClassSettings cs, Grid g)
        {
            _canvas.Children.Remove(cs);
            _canvas.Children.Remove(g);
        }

        /// <summary>
        /// Sparar egenskaper
        /// </summary>
        /// <param name="cs"></param>
        public void Save(ClassSettings cs)
        {
            ClassName.Content = cs.ClassName.Text;
            Attributes.Text = cs.Attributes.Text;
            Methods.Text = cs.Methods.Text;
        }

        /// <summary>
        /// VISAR yttre område på Klass från var användaren drar en linje ifrån
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InnerGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            NodeGrid.Visibility = Visibility.Visible;
            Cursor = Cursors.SizeAll;
        }

        /// <summary>
        /// GÖMMER yttre område på Klass från var användaren drar en linje ifrån
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OuterGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            NodeGrid.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Hämtar alla noder kopplade till Klassen
        /// </summary>
        /// <returns></returns>
        public List<Nod> GetNods()
        {
            return _noder;
        }

        /// <summary>
        /// Returnerar vilken sida ett musklick befinner sig om Klassen. Musklicket måste ske på yttre området
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public OnSide GetSideByPoint(Point p)
        {
            if (p.X <= InnerBorder.Margin.Left 
                && (p.Y >= InnerBorder.Margin.Left && p.Y < this.ActualHeight-InnerBorder.Margin.Left))
            {
                return OnSide.Left;
            }
            else if (p.X >= this.ActualWidth-InnerBorder.Margin.Left 
                && (p.Y >= InnerBorder.Margin.Left && p.Y < this.ActualHeight-InnerBorder.Margin.Left)) {
                    return OnSide.Right;
                }
            else if (p.Y >= this.ActualHeight - InnerBorder.Margin.Left
              && (p.X >= InnerBorder.Margin.Left && p.X < this.ActualWidth - InnerBorder.Margin.Left)
                )
            {
                return OnSide.Bottom;
            }
            else if (p.Y <= InnerBorder.Margin.Left
              && (p.X >= InnerBorder.Margin.Left && p.X < this.ActualWidth - InnerBorder.Margin.Left))
            {
                return OnSide.Top;
            }

            return OnSide.Corner;
        }

        /// <summary>
        /// Sätter klassens färger
        /// </summary>
		public void setKlassColors()
		{
			bgTopRow.SetCurrentValue(Control.BackgroundProperty, GetMainWindow().Colors.KlassNameBg);
			bgMidRow.SetCurrentValue(Control.BackgroundProperty, GetMainWindow().Colors.KlassAttributesBg);
			bgBotRow.SetCurrentValue(Control.BackgroundProperty, GetMainWindow().Colors.KlassMethodsBg);
		}

        private void InnerGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
        }

        /// <summary>
        /// Fastställer nod på grid. Nod "försvinner ej vid klass_mouseleave" (se LooseNode)
        /// </summary>
        /// <param name="n"></param>
        public void MakeNodAlwaysVisable(Nod n)
        {
            if (NodeGrid.Children.Contains(n))
            {
                NodeGrid.Children.Remove(n);
                NodeAttachedGrid.Children.Add(n);
            }
        }

        /// <summary>
        /// Lossar nod från grid. Nod "försvinner vid klass_mouseleave" (se SetNode)
        /// </summary>
        /// <param name="n"></param>
        public void MakeNodToggle(Nod n)
        {
            if (NodeAttachedGrid.Children.Contains(n))
            {
                NodeAttachedGrid.Children.Remove(n);
                NodeGrid.Children.Add(n);
            }
        }

        public static void LooseNodFromKlass(Klass k, Nod n)
        {

            if (k.NodeGrid.Children.Contains(n))
            {
                k.NodeGrid.Children.Remove(n);
            }
            else if (k.NodeAttachedGrid.Children.Contains(n))
            {
                k.NodeAttachedGrid.Children.Remove(n);
            }

            k._canvas.Children.Add(n);
            k._noder.Remove(n);
        }

        /// <summary>
        /// Nod lossar från canvas och sätts på Klass. Om "attached": MakeNodAlwaysVisible()
        /// </summary>
        /// <param name="n"></param>
        /// <param name="set"></param>
        public static void AttachNodToKlass(Klass k, Nod n, bool attached)
        {
            if (attached)
            {
                k.NodeAttachedGrid.Children.Add(n);
            }
            else
            {
                k.NodeGrid.Children.Add(n);
            }

            k._noder.Add(n);
        }
    }
}
