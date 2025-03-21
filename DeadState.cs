﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DP7
{
    public class DeadState : ICharacterState
    {
        public void Attack(Character character) => character.Message = "Nhân vật đã chết, không thể tấn công!";
        public void PickItem(Character character) => character.Message = "Nhân vật đã chết, không thể nhặt vật phẩm!";
        public void RecoverEnergy(Character character) => character.Message = "Nhân vật đã chết, không thể hồi năng lượng!";
        public void Move(Character character, int dx, int dy) => character.Message = "Nhân vật đã chết, không thể di chuyển";
        public string GetStateName() => "Dead";
    }
}
