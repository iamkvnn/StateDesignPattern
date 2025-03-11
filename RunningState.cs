﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DP7
{
    public class RunningState : ICharacterState
    {
        public void Attack(Character character)
        {
            Console.WriteLine("Nhân vật tấn công!");
            character.Energy -= 5;
            if (character.Energy <= 0)
            {
                character.Energy = 0;
                character.SetState(new DeadState());
            }
        }
        public void PickItem() => MessageBox.Show("Nhân vật nhặt vật phẩm khi chạy!");
        public void RecoverEnergy(Character character) => MessageBox.Show("Không thể hồi năng lượng khi chạy!");
        public void Move(Character character, int dx, int dy)
        {
            if (character.isMoving)
            {
                character.SetState(new MovingState());
                character.Move(dx, dy);
            }
            else if (!character.isRunning)
            {
                character.SetState(new IdleState());
                character.Move(dx, dy);
            }
            else
            {
                character.UpdatePosition(dx * 2, dy * 2);
                character.Energy -= 2;
                if (character.Energy <= 0)
                {
                    character.Energy = 0;
                    character.SetState(new DeadState());
                }
            }
        }
        public string GetStateName() => "Running";
    }
}
