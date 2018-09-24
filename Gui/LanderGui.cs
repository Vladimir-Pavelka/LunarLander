using System.Drawing;
using System.Windows.Forms;
using LunarLander;

namespace Gui
{
    public partial class LanderGui : Form
    {
        private const int SimulationIntervalMs = 1000 / 50; // 50 fps

        private readonly Timer _redrawTimer = new Timer();
        private readonly Image _landerImage = Image.FromFile("lander.png");
        private Bitmap _buffer;

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
            var landerPosition = new Point((int)_lander.Position.X + 550, -(int)_lander.Position.Y + 200);
            using (var rotatedLanderImage = Utils.RotateImage(_landerImage, (float)_lander.OrientationAngle))
                graphics.DrawImage(rotatedLanderImage, landerPosition);
        }

        private void LanderGui_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    _lander.IsMainEngineFiring = true;
                    break;

                case Keys.A:
                    _lander.IsRightThrusterFiring = true;
                    break;

                case Keys.D:
                    _lander.IsLeftThrusterFiring = true;
                    break;
            }
        }

        private void LanderGui_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    _lander.IsMainEngineFiring = false;
                    break;

                case Keys.A:
                    _lander.IsRightThrusterFiring = false;
                    break;

                case Keys.D:
                    _lander.IsLeftThrusterFiring = false;
                    break;
            }
        }

        private void LanderGui_SizeChanged(object sender, System.EventArgs e)
        {
            _buffer.Dispose();
            _buffer = new Bitmap(canvas.Width, canvas.Height);
        }
    }
}