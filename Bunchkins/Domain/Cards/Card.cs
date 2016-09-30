using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bunchkins.Domain.Cards
{
    public abstract class Card
    {
        internal string slot;

        public int CardId { get; set; }
        public string PictureUrl { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
