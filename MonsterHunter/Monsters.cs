using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterHunter
{
    public class Monsters
    {
        private List<Monster> _monsters = new List<Monster>();

        public void AddMonster(Monster monster)
        {
            _monsters.Add(monster);
        }

        public List<Monster> FindMonstersAtPosition(int x, int y)
        {
            return _monsters.Where(m => m.X == x && m.Y == y).ToList();
        }
    }

}
