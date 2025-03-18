using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DP7
{
    public class GameForm : Form
    {
        private Character _character;
        private Label lblState;
        private Label lblEnergy;

        // Dictionary to track key states
        private Dictionary<Keys, bool> _keyState = new Dictionary<Keys, bool>();

        // Mouse button tracking with separate flags for "just clicked" and "is held"
        private bool _leftMouseDown = false;
        private bool _rightMouseDown = false;
        private bool _leftMouseClicked = false;
        private bool _rightMouseClicked = false;
        private Point _mousePosition = Point.Empty;

        // Timer for game loop
        private Timer _gameTimer = new Timer();

        public GameForm()
        {
            Text = "State Pattern Stimulation Game";
            Width = 600;
            Height = 400;

            PictureBox characterBox = new PictureBox
            {
                Image = Image.FromFile("C:\\Users\\khoa2\\source\\repos\\DP7\\monster.png"),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Width = 50,
                Height = 50,
                Left = 250,
                Top = 200
            };
            Controls.Add(characterBox);

            _character = new Character(characterBox);
            _character.OnStateChange += UpdateStateLabel;
            _character.OnEnergyChange += UpdateEnergyLabel;

            lblState = new Label { Text = "State: Idle", Left = 10, Top = 10, Width = 200 };
            Controls.Add(lblState);

            lblEnergy = new Label { Text = "Energy: 200", Left = 10, Top = 50, Width = 200 };
            Controls.Add(lblEnergy);

            _keyState[Keys.W] = false;
            _keyState[Keys.A] = false;
            _keyState[Keys.S] = false;
            _keyState[Keys.D] = false;
            _keyState[Keys.ShiftKey] = false;
            _keyState[Keys.Space] = false;
            _keyState[Keys.E] = false;
            KeyDown += GameForm_KeyDown;
            KeyUp += GameForm_KeyUp;
            MouseDown += GameForm_MouseDown;
            MouseUp += GameForm_MouseUp;
            MouseMove += GameForm_MouseMove;
            MouseClick += GameForm_MouseClick;
            _gameTimer.Interval = 16;
            _gameTimer.Tick += GameLoop;
            _gameTimer.Start();
            KeyPreview = true;
        }

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (_keyState.ContainsKey(e.KeyCode))
                _keyState[e.KeyCode] = true;
        }

        private void GameForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (_keyState.ContainsKey(e.KeyCode))
                _keyState[e.KeyCode] = false;
        }

        private void GameForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _leftMouseDown = true;
            }
            else if (e.Button == MouseButtons.Right)
            {
                _rightMouseDown = true;
            }

            _mousePosition = e.Location;
        }

        private void GameForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _leftMouseDown = false;
            }
            else if (e.Button == MouseButtons.Right)
            {
                _rightMouseDown = false;
            }
        }

        private void GameForm_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _leftMouseClicked = true;
            }
            else if (e.Button == MouseButtons.Right)
            {
                _rightMouseClicked = true;
            }
        }

        private void GameForm_MouseMove(object sender, MouseEventArgs e)
        {
            _mousePosition = e.Location;
        }

        private void GameLoop(object sender, EventArgs e)
        {
            if (_keyState[Keys.Space])
            {
                _character.RecoverEnergy();
                _keyState[Keys.Space] = false;
            }

            if (_keyState[Keys.E])
            {
                _character.PickItem();
                _keyState[Keys.E] = false;
            }

            if (_leftMouseClicked)
            {
                Console.WriteLine("Left mouse clicked");
                _character.Attack();
                _leftMouseClicked = false;
            }

            _character.isRunning = _keyState[Keys.ShiftKey];
            int dx = 0;
            int dy = 0;
            if (_keyState[Keys.A]) dx -= 2;
            if (_keyState[Keys.D]) dx += 2;
            if (_keyState[Keys.W]) dy -= 2;
            if (_keyState[Keys.S]) dy += 2;

            _character.isMoving = dx != 0 || dy != 0;
            if (!_character.IsDead)
                _character.Move(dx, dy);
        }

        private void UpdateStateLabel(string state)
        {
            lblState.Text = "State: " + state;
        }

        private void UpdateEnergyLabel(int energy)
        {
            lblEnergy.Text = "Energy: " + energy;
        }
    }
}