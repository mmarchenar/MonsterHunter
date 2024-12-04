namespace MonsterHunter
{
    public class Monster : Character
    {
        // Constructor to initialize a Monster at a given position
        public Monster(int x, int y) : base(x, y)
        {
            X = x;
            Y = y;
            MaxHP = 20;      // Maximum health points of the monster
            CurrentHP = MaxHP; // Setting current health to max value
            Strength = 5;    // Strength of the monster, used for damage calculation
            Armor = 2;       // Armor value to reduce damage from attacks
            FreezeTime = 2000; // Time in milliseconds the monster is frozen after an attack (default: 2 seconds)
        }

        // Method to handle movement of the monster
        public override bool Move(int newX, int newY, Map map)
        {
            // Get the current hunter from the map
            Hunter hunter1 = map.currentHunter;

            // Ensure the new position is within the bounds of the map
            if (newX < 0 || newX >= map.Width || newY < 0 || newY >= map.Height)
            {
                // If the new position is out of bounds, do not move the monster
                return false;
            }
            else if((map.currentHunter.X == this.X+1 && map.currentHunter.Y == this.Y)|| (map.currentHunter.X == this.X - 1 && map.currentHunter.Y == this.Y)|| (map.currentHunter.X == this.X && map.currentHunter.Y == this.Y+1)|| (map.currentHunter.X == this.X && map.currentHunter.Y == this.Y-1))
            {
                attack(hunter1, map);
                    return false;
            }

            // Check if the monster is trying to move into a wall ('#')
            if (map.MapData[newY, newX] == '#')
            {
                // If it's a wall, the monster can't move here
                return false;
            }
            // If the monster is on the same space as the hunter, it attacks the hunter
            else if (hunter1.X == newX && hunter1.Y == newY)
            {
                attack(hunter1, map);  // Attack the hunter
            }
            else
            {
                // Otherwise, the monster moves to the new coordinates
                this.X = newX;
                this.Y = newY;
            }
            return true; // Return true indicating successful move
        }

        // Method to handle the monster attacking the hunter
        public void attack(Hunter target, Map map)
        {
            // Calculate damage dealt to the hunter (based on monster's strength and hunter's armor)
            int hit = this.Strength - target.Armor;
            target.CurrentHP -= hit;  // Apply damage to the hunter
            map.info.Add($"The monster dealt {hit} damage.");

            // Check if the hunter's shield breaks after the attack
            if (target.shieldH != null && target.shieldH.BreakAfterAttack())
            {
                // If the shield breaks, reduce the hunter's armor and nullify the shield
                target.Armor -= target.shieldH.Armor;
                target.shieldH = null;
                map.info.Add("Your shield broke!"); // Inform the hunter that the shield broke
            }

            // Check if the hunter has died (HP <= 0)
            if (target.IsDead())
            {
                target = null;
                map.info.Add("You died :("); // Inform the player that they died
                
            }
        }
    }
}
