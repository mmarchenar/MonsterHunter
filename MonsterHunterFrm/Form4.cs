using MonsterHunter; // Importing the MonsterHunter namespace
using System; // Importing system functionalities
using System.Collections.Generic; // Importing collections for using lists, dictionaries, etc.
using System.ComponentModel; // Importing components for Windows Forms
using System.Data; // Importing data functionalities
using System.Drawing; // Importing graphics functionalities
using System.Linq; // Importing LINQ functionalities for data manipulation
using System.Text; // Importing functionalities for string manipulation
using System.Threading.Tasks; // Importing functionalities for asynchronous programming
using System.Windows.Forms; // Importing Windows Forms functionalities
using System.Xml.Linq; // Importing XML functionalities

namespace MonsterHunterFrm // Defining the namespace for the form
{
    public partial class Form4 : Form // Partial class definition for Form4 inheriting from Form
    {
        public Form4() // Constructor for Form4
        {
            InitializeComponent(); // Initializes the form components
        }

        public void UpdateLeaderboard(string playerName, int playerScore) // Method to update the leaderboard with new scores
        {
            try
            {
                string filePath = "leaderboard.txt"; // Path to the leaderboard file
                List<(string Name, int Score)> scores = new List<(string, int)>(); // List to hold player names and scores

                // Check if leaderboard file exists and read scores
                if (File.Exists(filePath))
                {
                    string[] lines = File.ReadAllLines(filePath); // Read all lines from the leaderboard file
                    foreach (var line in lines)
                    {
                        var parts = line.Split('|'); // Split each line into name and score parts
                        if (parts.Length == 2 && int.TryParse(parts[1], out int score))
                        {
                            scores.Add((parts[0], score)); // Add valid scores to the list
                        }
                    }
                }

                // Add the new score to the list
                scores.Add((playerName, playerScore));

                // Sort scores in descending order and keep top 10
                scores = scores.OrderByDescending(s => s.Score).Take(10).ToList();

                // Write updated scores back to file
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (var (Name, Score) in scores)
                    {
                        writer.WriteLine($"{Name}|{Score}"); // Write each name and score to the file
                    }
                }

                UpdateLeaderboardDisplay(scores); // Update the display with new leaderboard data
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating leaderboard: {ex.Message}"); // Log any errors that occur during processing
            }
        }

        private void UpdateLeaderboardDisplay(List<(string Name, int Score)> scores) // Method to update the leaderboard display in the UI
        {
            textBox1.Clear(); // Clear previous entries in the text box

            foreach (var (Name, Score) in scores)
            {
                textBox1.AppendText($"{Name}: {Score}\r\n"); // Append each player's score to the text box
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Placeholder for label1 click event handling (currently not used)
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Placeholder for text box text changed event handling (currently not used)
        }

        private void PlayerName_Click(object sender, EventArgs e)
        {
            // Placeholder for PlayerName label click event handling (currently not used)
        }

        private void label1_Click_1(object sender, EventArgs e)
        {
            // Placeholder for another label1 click event handling (currently not used)
        }
    }
}