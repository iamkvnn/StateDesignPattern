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
        public void Attack(Character character) => character.Message = "Nhân vật tấn công khi đứng yên";
        public void PickItem(Character character) => character.Message = "Nhân vật nhặt vật phẩm";
        public void RecoverEnergy(Character character)
        {
            if (character.Energy < 200)
            {
                character.Message = "Đang phục hồi năng lượng";
                character.Energy += 1;
            }
            else
                character.Message = "Năng lượng đã đầy!";
        }
        public void Move(Character character, int dx, int dy)
        {
            if (character.isRunning && character.isMoving)//&& !character.isRunning)
            {
                character.SetState(new RunningState());
                character.Move(dx, dy);
            }
            else if (character.isMoving)
            {
                character.SetState(new MovingState());
                character.Move(dx, dy);
            }
        }
        public string GetStateName() => "Idle"; 
    }
}
