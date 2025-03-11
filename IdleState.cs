using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DP7
{
    public class IdleState : ICharacterState
    {
        public void Attack(Character character)
        {
            Console.WriteLine("Nhân vật tấn công!");
        }
        public void PickItem() => MessageBox.Show("Nhân vật nhặt vật phẩm!");
        public void RecoverEnergy(Character character)
        {
            if (character.Energy < 100)
                character.Energy += 1;
            else
                MessageBox.Show("Năng lượng đã đầy!");
        }
        public void Move(Character character, int dx, int dy)
        {
            if (character.isMoving )//&& !character.isRunning)
            {
                character.SetState(new MovingState());
                character.Move(dx, dy);
            }
            else if (character.isRunning)
            {
                character.isMoving = false;
                character.SetState(new RunningState());
                character.Move(dx, dy);
            }
        }
        public string GetStateName() => "Idle"; 
    }
}
