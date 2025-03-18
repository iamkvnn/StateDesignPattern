using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DP7
{
    public class Character
    {
        private ICharacterState _state;
        public PictureBox PictureBox { get; private set; }
        public event Action<string> OnStateChange;
        public event Action<int> OnEnergyChange;
        public event Action<string> OnMessageChange;
        private int _energy = 200;
        private string _msg;
        public bool isRunning { get; set; } = false;
        public bool isMoving { get; set; } = false;
        private Label _messageLabel;
        private Timer _messageTimer;
        private Form _parentForm;

        public Character(PictureBox pictureBox)
        {
            PictureBox = pictureBox;
            _state = new IdleState();
            _parentForm = pictureBox.FindForm();
            _messageLabel = new Label
            {
                AutoSize = true,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                TextAlign = ContentAlignment.MiddleCenter,
                Visible = false
            };

            if (_parentForm != null)
            {
                _parentForm.Controls.Add(_messageLabel);
                _messageLabel.BringToFront();
            }

            _messageTimer = new Timer
            {
                Interval = 1000 
            };
            _messageTimer.Tick += (sender, e) =>
            {
                _messageLabel.Visible = false;
                _messageTimer.Stop();
            };
        }

        public void SetState(ICharacterState newState)
        {
            _state = newState;
            OnStateChange?.Invoke(_state.GetStateName());
        }

        public bool IsDead => _state is DeadState;

        public int Energy
        {
            get => _energy;
            set
            {
                _energy = value;
                OnEnergyChange?.Invoke(_energy);
            }
        }

        public void Move(int dx, int dy)
        {
            _state.Move(this, dx, dy);
        }

        public void RecoverEnergy()
        {
            _state.RecoverEnergy(this);
        }

        public void PickItem()
        {
            _state.PickItem(this);
        }

        public void Attack()
        {
            _state.Attack(this);
        }

        public void UpdatePosition(int dx, int dy)
        {
            PictureBox.Left += dx;
            PictureBox.Top += dy;
            if (_messageLabel.Visible)
            {
                _messageLabel.Left = PictureBox.Left + (PictureBox.Width - _messageLabel.Width) / 2;
                _messageLabel.Top = PictureBox.Top - _messageLabel.Height - 5;
            }
        }

        public string Message
        {
            get => _msg;
            set
            {
                _msg = value;

                if (!string.IsNullOrEmpty(_msg))
                {
                    _messageLabel.Text = _msg;

                    _messageLabel.Left = PictureBox.Left + (PictureBox.Width - _messageLabel.Width) / 2;
                    _messageLabel.Top = PictureBox.Top - _messageLabel.Height - 5;

                    _messageLabel.Visible = true;

                    _messageTimer.Stop();
                    _messageTimer.Start();

                    OnMessageChange?.Invoke(_msg);
                }
            }
        }
    }
}
