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
            character.Message = "Nhân vật tấn công khi di chuyển, tiêu hao 2 năng lượng";
            character.Energy -= 2;
            if (character.Energy <= 0)
            {
                character.Energy = 0;
                character.SetState(new DeadState());
            }
        }
        public void PickItem(Character character) => character.Message = "Đã nhặt vật phẩm trong lúc di chuyển";
        public void RecoverEnergy(Character character) => character.Message = "Không thể hồi năng lượng khi di chuyển";
        public void Move(Character character, int dx, int dy)
        {
            if (!character.isMoving)
            {
                character.SetState(new IdleState());
                character.Move(dx, dy);
            }
            else if (character.isRunning)
            {
                character.SetState(new RunningState());
                character.Move(dx, dy);
            }
            else
                character.UpdatePosition(dx, dy);
        }
        public string GetStateName() => "Moving";
    }
}
