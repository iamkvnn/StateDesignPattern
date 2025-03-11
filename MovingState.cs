using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DP7
{
    public class MovingState : ICharacterState
    {

        public void Attack(Character character)
        {
            Console.WriteLine("Nhân vật tấn công!");
            character.Energy -= 2;
            if (character.Energy <= 0)
            {
                character.Energy = 0;
                character.SetState(new DeadState());
            }
        }
        public void PickItem() => MessageBox.Show("Nhân vật nhặt vật phẩm");
        public void RecoverEnergy(Character character) => Console.WriteLine("Khong the hoi nang luong khi chay");
        public void Move(Character character, int dx, int dy)
        {
            if (character.isRunning)
            {
                character.isMoving = false;
                character.SetState(new RunningState());
                character.Move(dx, dy);
            }
            else if (!character.isMoving)
            {
                character.SetState(new IdleState());
                character.Move(dx, dy);
            }
            else
                character.UpdatePosition(dx, dy);
        }
        public string GetStateName() => "Moving";
    }
}
