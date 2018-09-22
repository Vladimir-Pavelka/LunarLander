using System.Drawing;
using System.Windows.Forms;
using LunarLander;

namespace Gui
{
    public partial class LanderGui : Form
    {
        private const int SimulationIntervalMs = 1000 / 50; // 50 fps

        private readonly Timer _redrawTimer = new Timer();
        private readonly Image _landerImage = Image.FromFile(@"C:\Users\pavelkav\Desktop\Personal\lander.png");
        private Point _landerPosition = new Point(0, 0);

        private readonly Bitmap _buffer;

        private readonly Lander _lander = Lander.Still;

        public LanderGui()
        {
            InitializeComponent();

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            _buffer = new Bitmap(canvas.Width, canvas.Height);

            _redrawTimer.Interval = SimulationIntervalMs;
            _redrawTimer.Tick += (sender, args) => OnFrame();
            _redrawTimer.Enabled = true;
        }

        private void OnFrame()
        {
            UpdateSimulation();
            Redraw();
        }

        private void UpdateSimulation()
        {
            _lander.AdvanceTime(SimulationIntervalMs);
        }

        private void Redraw()
        {
            using (var graphics = Graphics.FromImage(_buffer))
            {
                graphics.FillRectangle(Brushes.Black, new Rectangle(0, 0, canvas.Width, canvas.Height));
                DrawLander(graphics);
                using (var canvasGraphics = canvas.CreateGraphics())
                    canvasGraphics.DrawImageUnscaled(_buffer, 0, 0);
            }
        }

        private void DrawLander(Graphics graphics)
        {
            graphics.DrawImage(_landerImage, _landerPosition);
        }

        private void LanderGui_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    _lander.IsMainEngineFiring = true;
                    break;

                case Keys.A:
                    _landerPosition = new Point(_landerPosition.X - 10, _landerPosition.Y);
                    break;

                case Keys.D:
                    _landerPosition = new Point(_landerPosition.X + 10, _landerPosition.Y);
                    break;

                case Keys.W:
                    _landerPosition = new Point(_landerPosition.X, _landerPosition.Y - 10);
                    break;

                case Keys.S:
                    _landerPosition = new Point(_landerPosition.X, _landerPosition.Y + 10);
                    break;
            }
        }
    }
}
