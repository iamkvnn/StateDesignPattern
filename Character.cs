using System;
using System.Collections.Generic;
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
        private int _energy = 100;
        public bool isRunning { get; set; } = false;
        public bool isMoving { get; set; } = false;

        public Character(PictureBox pictureBox)
        {
            PictureBox = pictureBox;
            _state = new IdleState();
        }

        public void SetState(ICharacterState newState)
        {
            _state = newState;
            OnStateChange?.Invoke(_state.GetStateName());
        }

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
            _state.PickItem();
        }

        public void Attack()
        {
            _state.Attack(this);
        }

        public void UpdatePosition(int dx, int dy)
        {
            PictureBox.Left += dx;
            PictureBox.Top += dy;
        }
    }
}
