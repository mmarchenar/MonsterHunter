using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterHunter
{
    public enum PotionType
    {
        Strength,
        Poison,
        Invisibility,
        Speed
    }
    public class Potions
    {
        public PotionType Type { get; private set; }

        public Potions()
        {
            int roll = Random.Instance.Next(1, 7);
            Type = roll switch
            {
                1 => PotionType.Poison,
                2 or 3 => PotionType.Speed,
                4 or 5 => PotionType.Invisibility,
                _ => PotionType.Strength
            };
        }
    }

}
