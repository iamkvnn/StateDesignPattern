using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP7
{
    public interface ICharacterState
    {
        void Attack(Character character);
        void PickItem(Character character);
        void RecoverEnergy(Character character);
        void Move(Character character, int dx, int dy);
        string GetStateName();
    }
}
