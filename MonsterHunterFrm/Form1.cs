using System.Data;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Windows.Forms;
using MonsterHunter;

namespace MonsterHunterFrm
{
    public partial class Form1 : Form
    {
        private Map map; // Represents the game map
        private Monsters monsters; // Holds the monsters in the game
        private Hunter hunter; // Represents the player character
        private string playerName; // Stores the player's name
        private string mapChosen; // Stores the chosen map name
        private int level = 1; // Player's current level
        private int monFreezeTime = 2000; // Time in milliseconds for monster movement
        private bool gameOver = false; // Indicates if the game is over
        private System.Windows.Forms.Timer monsterMovementTimer; // Timer for monster movements
        private System.Windows.Forms.Timer playerMovementTimer; // Timer for player movements (not used)
        private System.Windows.Forms.Timer displayUpdateTimer; // Timer for updating display
        private System.Windows.Forms.Timer infoUpdateTimer; // Timer for updating info
        private Dictionary<char, Image> imageDictionary; // Dictionary to hold character images
        private Form4 highScore; // Form to display high scores

        private void InitializeImageDictionary()
        {
            try
            {
                imageDictionary = new Dictionary<char, Image>
                {
                    { 'H', Image.FromFile("hunter.png") }, // Image for hunter character
                    { 'M', Image.FromFile("monster.png") }, // Image for monster character
                    { 'w', Image.FromFile("sword.png") }, // Image for sword item
                    { 'x', Image.FromFile("pickaxe.png") }, // Image for pickaxe item
                    { 'h', Image.FromFile("shield.png") }, // Image for shield item
                    { 'p', Image.FromFile("potion.png") }, // Image for potion item
                    { 'G', Image.FromFile("goal.png") }, // Image for goal position
                    { '#', Image.FromFile("wall.png") }, // Image for wall
                    { ' ', Image.FromFile("path.png") } // Image for path
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing image dictionary: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MonsterMovementTimer_Tick(object sender, EventArgs e)
        {
            if (!gameOver)
            {
                MonsterMove(map, monsters); // Move monsters if the game is not over
            }
        }

        private void MonsterMove(Map map, Monsters monsters)
        {
            try
            {
                Monster[] monList = Monsters._monsters.ToArray(); // Convert list of monsters to array
                foreach (Monster mon in monList)
                {
                    int direction = MonsterHunter.Random.Instance.Next(0, 4); // Random direction for movement
                    int newX = mon.X; // Current X position of the monster
                    int newY = mon.Y; // Current Y position of the monster

                    switch (direction)
                    {
                        case 0: newY = (newY - 1 >= 0) ? newY - 1 : newY; break; // Move up if within bounds
                        case 1: newY = (newY + 1 < map.Width) ? newY + 1 : newY; break; // Move down if within bounds
                        case 2: newX = (newX - 1 >= 0) ? newX - 1 : newX; break; // Move left if within bounds
                        case 3: newX = (newX + 1 < map.Height) ? newX + 1 : newX; break; // Move right if within bounds
                    }

                    mon.Move(newX, newY, map); // Update monster's position on the map
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error moving monsters: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayMap(Map map)
        {
            int cellSize = 35; // Size of each cell in the display grid

            try
            {
                this.Controls.Clear(); // Clear previous controls on the form

                for (int y = 0; y < map.MapData.GetLength(1); y++)
                {
                    for (int x = 0; x < map.MapData.GetLength(0); x++)
                    {
                        char character = map.MapData[x, y]; // Get character from map data

                        if (map.currentHunter.X == x && map.currentHunter.Y == y)
                        {
                            if (imageDictionary.TryGetValue('H', out Image img))
                            {
                                PictureBox pictureBox = new PictureBox
                                {
                                    Image = img,
                                    SizeMode = PictureBoxSizeMode.StretchImage,
                                    Location = new Point(y * cellSize, x * cellSize),
                                    Size = new Size(cellSize, cellSize)
                                };
                                this.Controls.Add(pictureBox);
                            }
                        }
                        else if (Monsters.FindMonstersAtPosition(x, y).Length != 0 && (x != 0 && y != 0))
                        {
                            if (imageDictionary.TryGetValue('M', out Image img))
                            {
                                PictureBox pictureBox = new PictureBox
                                {
                                    Image = img,
                                    SizeMode = PictureBoxSizeMode.StretchImage,
                                    Location = new Point(y * cellSize, x * cellSize),
                                    Size = new Size(cellSize, cellSize)
                                };
                                this.Controls.Add(pictureBox);
                            }
                        }
                        else
                        {
                            if (imageDictionary.TryGetValue(character, out Image img))
                            {
                                PictureBox pictureBox = new PictureBox
                                {
                                    Image = img,
                                    SizeMode = PictureBoxSizeMode.StretchImage,
                                    Location = new Point(y * cellSize, x * cellSize),
                                    Size = new Size(cellSize, cellSize)
                                };
                                this.Controls.Add(pictureBox);
                            }
                        }
                    }
                }

                Debug.WriteLine($"x: {this.map.currentHunter.X} y: {this.map.currentHunter.Y}"); // Log hunter's position to debug output
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error displaying map: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public Form1()
        {
            try
            {
                InitializeComponent(); // Initialize form components

                this.DoubleBuffered = true; // Enable double buffering to reduce flicker

                InitializeImageDictionary(); // Load images into dictionary

                this.gameOver = false;

                Map map = new Map(); // Create a new instance of Map
                Core.mapCore = map;

                this.map = map;

                Core.availableMaps = map.AvailableMaps;

                Monsters monsters = new Monsters(); // Create a new instance of Monsters
                this.monsters = monsters;

                Form2 form2 = new Form2(); // Create second form instance
                Form3 form3 = new Form3(); // Create third form instance
                Form4 form4 = new Form4(); // Create fourth form instance

                this.highScore = form4;

                Core.inForm = form3;

                form2.ShowDialog(); // Show second form as dialog

                form3.Show();

                form3.Location = new Point(this.Location.X, this.Location.Y + this.Height);

                form4.Location = new Point(this.Location.X, this.Location.Y + this.Width + 100);

                map.LoadMap(Core.chosenMap, monsters, Core.Name);

                monsterMovementTimer = new System.Windows.Forms.Timer();

                monsterMovementTimer.Interval = monFreezeTime; // Set interval for monster movement

                monsterMovementTimer.Tick += MonsterMovementTimer_Tick; // Subscribe to Tick event

                monsterMovementTimer.Start(); // Start the timer

                displayUpdateTimer = new System.Windows.Forms.Timer();

                displayUpdateTimer.Interval = 100; // Set interval for display updates (e.g., 100 ms)

                displayUpdateTimer.Tick += DisplayUpdateTimer_Tick; // Subscribe to Tick event

                displayUpdateTimer.Start();

                infoUpdateTimer = new System.Windows.Forms.Timer();

                infoUpdateTimer.Interval = 100;

                infoUpdateTimer.Tick += infoUpdateTimer_Tick;

                infoUpdateTimer.Start();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing Form1: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void infoUpdateTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                Core.inForm.change(); // Update information in another form
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating info: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayUpdateTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                DisplayMap(map); // Refresh the map display based on current state

                if (this.map.currentHunter.Levelup == true)
                {
                    this.map.currentHunter.Levelup = false;
                    this.map.LoadMap(Core.chosenMap, this.monsters, Core.Name);
                    Core.level += 1;
                    this.monFreezeTime -= 100;
                    this.map.currentHunter.FreezeTime -= 50;
                    this.map.currentHunter.Score += 250;
                    this.highScore.UpdateLeaderboard(this.map.currentHunter.Name, this.map.currentHunter.Score);
                    this.highScore.Show();
                }

                if (this.map.currentHunter.IsDead())
                {
                    Core.gameOver = true;
                    this.gameOver = true;
                    this.highScore.UpdateLeaderboard(this.map.currentHunter.Name, this.map.currentHunter.Score);
                    this.highScore.Show();
                    this.Hide();
                    Core.inForm.Hide();
                    displayUpdateTimer.Stop();
                    infoUpdateTimer.Stop();
                    monsterMovementTimer.Stop();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating display: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e) { }

        private void label2_Click(object sender, EventArgs e) { }

        private void textBox1_TextChanged(object sender, EventArgs e) { }

        private void label3_Click(object sender, EventArgs e) { }

        private void panel1_Paint(object sender, PaintEventArgs e) { }

        private void label8_Click(object sender, EventArgs e) { }

        private void label4_Click(object sender, EventArgs e) { }

        private void label7_Click(object sender, EventArgs e) { }

        private void label5_Click(object sender, EventArgs e) { }

        private void OnKeyUp(object sender, KeyEventArgs e) { }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            Hunter hunter = this.map.currentHunter;

            if (gameOver) return; // Prevent movement if the game is over

            int newX = hunter.X; // Current X position of the hunter

            int newY = hunter.Y; // Current Y position of the hunter

            Debug.WriteLine($"current newx: {newX} current newY: {newY}");

            switch (e.KeyCode)
            {
                case Keys.Left: newY = (newY - 1 >= 0) ? newY - 1 : newY; Debug.WriteLine("Hunter is moving up"); break;
                case Keys.Right: newY = (newY + 1 < map.Width) ? newY + 1 : newY; Debug.WriteLine("Hunter is moving down"); break;
                case Keys.Up: newX = (newX - 1 >= 0) ? newX - 1 : newX; Debug.WriteLine("Hunter is moving left"); break;
                case Keys.Down: newX = (newX + 1 < map.Height) ? newX + 1 : newX; Debug.WriteLine("Hunter is moving right"); break;
            }

            try
            {
                hunter.Move(newX, newY, this.map); // Update hunter's position on the map
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error moving hunter: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}