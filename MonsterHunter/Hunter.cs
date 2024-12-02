using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MonsterHunter
{
    public class Hunter : Character
    {
        public string Name { get; set; }
        public int Score { get; set; }
        public IState State { get; set; }
        private Pickaxe pickaxeH { get; set; }
        private Sword swordH { get; set; }
        private Potions potionsH { get; set; }
        public Shield shieldH {get; set; }
        public bool isInvisible {get;set;}
        public Map currentMap { get; set;}
        public Hunter(int x, int y, string name, Map map) : base(x,y)
        {
            X = x;
            Y = y;
            Score = 0;
            Name = name;
            MaxHP = 30;
            CurrentHP = MaxHP;
            pickaxeH = null;
            swordH = null;
            shieldH = null;
            isInvisible = false;
            Strength = 7;
            Armor = 4;
            FreezeTime = 1000; // Default freeze time 1 second
            State = new NormalState();  // Default state
        }

       
        public override bool Move(int newX, int newY, Map map)
        {
            Monster[] target = Monsters.FindMonstersAtPosition(newX, newY);
            if (target != null)
            {
                foreach (Monster monster in target)
                {
                    attack(monster);
                }
            }
            else if (map.MapData[newX, newY] == '#')  
            {
                if (isInvisible == false)
                {
                    if (hasPickaxe())
                    {
                        breakWall(map, newX, newY);
                    }
                    else
                    {
                        Console.WriteLine("You don't have a pickaxe to break the wall");
                        return false;
                    }
                }
                else
                {

                }
            } 
            else if ((map.MapData[newX, newY] == 'h') || (map.MapData[newX, newY] == 'w') || (map.MapData[newX, newY] == 'p') || (map.MapData[newX, newY] == 'x'))
            {
                AddToInventory(map.MapData[newX, newY]);
            }
            else
            {
                X = newX;
                Y = newY;
            }
            return true;
        }
        
        private void AddToInventory(char item)
        {
            switch (item)
            {
                case 'x':
                    Pickaxe pickaxe = new Pickaxe(); 
                    pickaxeH = pickaxe;
                    Score += 50;
                    Console.WriteLine("You picked up a pickaxe!");
                    break;
                case 'w':
                    if (swordH != null) { this.Strength -= swordH.Strength; }
                    Sword sword = new Sword();
                    swordH = sword;
                    this.Strength += sword.Strength;
                    Score += 50;
                    Console.WriteLine("You picked up a Sword: +"+ sword.Strength+" Strength!");  break;
                case 'h':
                    if (shieldH != null) { this.Armor -= shieldH.Armor; }
                    Shield shield = new Shield(); 
                    shieldH = shield;
                    this.Armor += shield.Armor;
                    Score += 50;
                    Console.WriteLine("You picked up a Sword: +" + shield.Armor + " Defense!"); break;
                case 'p':
                    Potions potion = new Potions();
                    this.drinkPotion(potion);
                    Score += 25; break;

            }

        }

        private bool hasPickaxe()
        {
            if (this.pickaxeH != null) { return true; }

            return false;
        }
        private void breakWall(Map map, int newX, int newY)
        {

                if (this.pickaxeH.BreakAfterUse()) {
                    this.pickaxeH = null;
                    Console.WriteLine("Your pickaxe broke!");
                }
                Console.WriteLine("Wall broken!");
            
        }
        public void drinkPotion(Potions potion)
        {
            // Modify state based on the potion type
            switch (potion.Type)
            {
                case PotionType.Strength:
                    State = new StrongState(this);
                    break;
                case PotionType.Poison:
                    State = new PoisonedState(this);
                    break;
                case PotionType.Invisibility:
                    State = new InvisibleState(this);
                    break;
                case PotionType.Speed:
                    State = new FastState(this);
                    break;
            }
        }

        public void attack(Monster target) {
            int hit = this.Strength - target.Armor;
            target.CurrentHP -= hit;
            Console.WriteLine($"You dealt {hit} damage");
            if (this.swordH.BreakAfterAttack())
            {
                this.Strength -= swordH.Strength;
                this.swordH = null;
                Console.WriteLine("Your sword broke!!");
            }
            if (target.IsDead())
            {
                target = null;
                this.Score += 100;
                Console.WriteLine("The monster died!!");
            }

        }
        


    }

}
