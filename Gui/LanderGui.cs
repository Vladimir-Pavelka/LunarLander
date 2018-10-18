using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using LunarLander;

namespace Gui
{
    public partial class LanderGui : Form
    {
        private const int SimulationIntervalMs = 1000 / 30; // 50 fps

        private readonly Timer _redrawTimer = new Timer();
        private readonly Image _landerImage = Image.FromFile("lander.png");
        private Bitmap _buffer;
        private Graphics _graphics;

        private readonly Lander _lander = Lander.Still;

        private readonly IDictionary<int, Image> _rotationCache;

        public LanderGui()
        {
            InitializeComponent();

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            _buffer = new Bitmap(canvas.Width, canvas.Height);
            _graphics = Graphics.FromImage(_buffer);

            _rotationCache = Enumerable.Range(0, 360)
                .Select(angle => new { Key = angle, Value = Utils.RotateImage(_landerImage, -angle) })
                .ToDictionary(x => x.Key, x => x.Value);


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
            _graphics.FillRectangle(Brushes.Black, new Rectangle(0, 0, canvas.Width, canvas.Height));
            DrawLander(_graphics);
            using (var canvasGraphics = canvas.CreateGraphics())
                canvasGraphics.DrawImageUnscaled(_buffer, 0, 0);
        }

        private void DrawLander(Graphics graphics)
        {
            var landerPosition = new Point((int)_lander.Position.X + 550, -(int)_lander.Position.Y + 200);
            var rotatedLanderImage = _rotationCache[(int)_lander.OrientationAngle];

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

                case Keys.Space:
                    _lander.Halt();
                    break;

                case Keys.F5:
                    _lander.Reset();
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
            _graphics.Dispose();
            _buffer = new Bitmap(canvas.Width, canvas.Height);
            _graphics = Graphics.FromImage(_buffer);
        }
    }
}