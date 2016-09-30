using Bunchkins.Domain.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bunchkins.Domain.Cards
{
    public interface IAnytimeSpell
    {
        void Cast(Player player);
    }
}
