using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace DP7
{
    public class GameForm : Form
    {
        private Character _character;
        private Label lblState;
        private Label lblEnergy;

        public GameForm()
        {
            Text = "Game State Pattern";
            Width = 600;
            Height = 400;

            PictureBox characterBox = new PictureBox
            {
                Image = System.Drawing.Image.FromFile("C:\\Users\\khoa2\\source\\repos\\DP7\\monster.png"),
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

            lblEnergy = new Label { Text = "Energy: 100", Left = 10, Top = 50, Width = 200 };
            Controls.Add(lblEnergy);

            KeyDown += GameForm_KeyDown;
            KeyUp += GameForm_KeyUp;
            MouseClick += GameForm_MouseClick;
        }

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space) _character.RecoverEnergy();
            if (e.KeyCode == Keys.E) _character.PickItem();
            if (e.KeyCode == Keys.ShiftKey)
            {
                _character.isRunning = true;
            }
            switch(e.KeyCode)
            {
                case Keys.A:
                    _character.isMoving = true;
                    _character.Move(-5, 0);
                    break;
                case Keys.D:
                    _character.isMoving = true;
                    _character.Move(5, 0);
                    break;
                case Keys.W:
                    _character.isMoving = true;
                    _character.Move(0, -5);
                    break;
                case Keys.S:
                    _character.isMoving = true;
                    _character.Move(0, 5);
                    break;
            }
            lblEnergy.Text = "Energy: " + _character.Energy;
        }

        private void GameForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey)
            {
                _character.isRunning = false;
                _character.Move(0, 0);
            }
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.D || e.KeyCode == Keys.S || e.KeyCode == Keys.W)
            {
                _character.isMoving = false;
                _character.Move(0, 0);
            }
        }

        private void UpdateStateLabel(string state)
        {
            lblState.Text = "State: " + state;
        }

        private void UpdateEnergyLabel(int energy)
        {
            lblEnergy.Text = "Energy: " + energy;
        }

        private void GameForm_MouseClick(object sender, MouseEventArgs e)
        {
            _character.Attack();
        }
    }
}
