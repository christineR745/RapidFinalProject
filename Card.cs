using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidFinalProject
{
    public class Card
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public char Type { get; private set; }

        //public string Image { get; private set; }
        //public int Cost {get; Private set;}

        public Card(string name, string desc, char type)
        {
            Name = name;
            Description = desc;
            Type = type;
        }

        virtual public void Effect()
        {
            //This method includes any strange effects a card has when it is played
        }
    }
}
