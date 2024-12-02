using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterHunter
{
    public class Hunter : Character
    {
        public string Name { get; set; }
        public int Score { get; set; }
        public IState State { get; set; }

        public Hunter(int x, int y, string name) : base(x,y)
        {
            X = x;
            Y = y;
            Name = name;
            MaxHP = 30;
            CurrentHP = MaxHP;
            picaxe = null;
            sword = null;
            shield = null;
            Strength = 7;
            Armor = 4;
            FreezeTime = 1000; // Default freeze time 1 second
            State = new NormalState();  // Default state
        }
        
        public override bool Move(int newX, int newY, Map map)
        {
            if (map.MapData[newX, newY] == ' ')
            {
                X = newX;
                Y = newY;
            }
            else if (map.MapData[newX, newY] == '#')  // Si el Hunter intenta mover hacia una pared
            {
                // Verificar si tiene un pico en el inventario para romper la pared
                BreakWall(map, newX, newY);
            }
            else if ((map.MapData[newX, newY] == 'h') || (map.MapData[newX, newY] == 'w') || (map.MapData[newX, newY] == 'p') || (map.MapData[newX, newY] == 'x'))
            {
                AddToInventory(map.MapData[newX, newY]);

                // Si el objeto es recogido, lo eliminamos del mapa (por ejemplo, reemplazándolo con un espacio vacío)
                map.MapData[newX, newY] = ' ';

            }
            return true;
        }


        private void AddToInventory(char item)
        {
            Object itemObject = null;
            switch (item)
            {
                case 'x':
                    Pickaxe pickaxe = new Pickaxe(); break;
                case 'w':
                    Sword sword = new Sword(); break;
                case 'h':
                    Shield shield = new Shield(); break;
                case 'p':
                    Potions potion = new Potions(); break;

            }
            for (int i = 0; i < inventory.Length; i++)
            {


                if (inventory[i] ==null)  // Si el espacio está vacío
                {
                    inventory[i] = item;  // Añadir el objeto al inventario
                    Console.WriteLine($"Objeto '{item}' añadido al inventario.");
                    return;
                }
            }
            Console.WriteLine("Inventario lleno. No se puede añadir más objetos.");
        }
        private bool HasPickaxe()
        {
            for (int i = 0; i < inventory.Length; i++)
            {
                if (inventory[i] == 'x') // 'x' representa el pico
                {
                    return true;
                }
            }
            return false;
        }
        private void BreakWall(Map map, int newX, int newY)
        {
            if (HasPickaxe())
            {
                map.MapData[newX, newY] = ' ';  // Rompe la pared cambiando el carácter a un espacio
                Console.WriteLine("Pared rota con el pico!");
            }
            else
            {
                Console.WriteLine("No tienes un pico para romper la pared.");
            }
        }
        public void DrinkPotion(Potions potion)
        {
            // Modify state based on the potion type
            switch (potion.Type)
            {
                case PotionType.Strength:
                    State = new StrongState();
                    break;
                case PotionType.Poisoned:
                    State = new PoisonedState();
                    break;
                case PotionType.Invisibility:
                    State = new InvisibleState();
                    break;
                case PotionType.Speed:
                    State = new FastState();
                    break;
            }
        }
    }

}
