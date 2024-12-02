using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterHunter
{
    public class Random
    {
        private static Random _instance;
        private Random _random;

        private Random()
        {
            _random = new Random();
        }

        public static Random Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Random();
                }
                return _instance;
            }
        }

        public int Next(int minValue, int maxValue)
        {
            return _random.Next(minValue, maxValue);
        }
    }


}
