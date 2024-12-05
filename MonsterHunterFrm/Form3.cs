using System; // Importing system functionalities
using System.Collections.Generic; // Importing collections for using lists, dictionaries, etc.
using System.ComponentModel; // Importing components for Windows Forms
using System.Data; // Importing data functionalities
using System.Drawing; // Importing graphics functionalities
using System.Linq; // Importing LINQ functionalities for data manipulation
using System.Text; // Importing functionalities for string manipulation
using System.Threading.Tasks; // Importing functionalities for asynchronous programming
using System.Windows.Forms; // Importing Windows Forms functionalities
using MonsterHunter; // Importing the MonsterHunter namespace

namespace MonsterHunterFrm // Defining the namespace for the form
{
    public partial class Form3 : Form // Partial class definition for Form3 inheriting from Form
    {
        public Form3() // Constructor for Form3
        {
            InitializeComponent(); // Initializes the form components
        }

        public void change() // Method to update UI elements based on game state
        {
            try
            {
                Map mapA = Core.mapCore; // Access the current map from Core

                progressBar1.Minimum = 0; // Set minimum value of progress bar
                progressBar1.Maximum = mapA.currentHunter.MaxHP; // Set maximum value based on hunter's max HP
                progressBar1.Value = mapA.currentHunter.CurrentHP; // Set current value of progress bar to hunter's current HP

                currenthp.Text = $"{mapA.currentHunter.CurrentHP}"; // Update current HP text
                currenthp.ForeColor = Color.Green; // Set text color to green for healthy HP

                actualscore.Text = $"{mapA.currentHunter.Score}"; // Update score display
                actuallevel.Text = $"{Core.level}"; // Update level display
                actualname.Text = $"{mapA.currentHunter.Name}"; // Update name display

                tb_infos.Clear(); // Clear previous information in the text box

                foreach (string item in mapA.info) // Iterate through info list in map
                {
                    tb_infos.AppendText(item + Environment.NewLine); // Append each info item to the text box with a new line
                }

                if (mapA.currentHunter.CurrentHP < 6) // Check if current HP is below a threshold
                {
                    currenthp.Text = $"{mapA.currentHunter.CurrentHP}"; // Update current HP text again if low
                    currenthp.ForeColor = Color.Red; // Change text color to red to indicate danger
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating UI: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            // Placeholder for form load event handling (currently not used)
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {
            // Placeholder for progress bar click event handling (currently not used)
        }

        private void label2_Click(object sender, EventArgs e)
        {
            // Placeholder for label2 click event handling (currently not used)
        }

        private void label3_Click(object sender, EventArgs e)
        {
            // Placeholder for label3 click event handling (currently not used)
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Placeholder for label1 click event handling (currently not used)
        }

        private void label4_Click(object sender, EventArgs e)
        {
            // Placeholder for label4 click event handling (currently not used)
        }

        private void label5_Click(object sender, EventArgs e)
        {
            // Placeholder for label5 click event handling (currently not used)
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Placeholder for text box text changed event handling (currently not used)
        }

        private void label6_Click(object sender, EventArgs e)
        {
            // Placeholder for label6 click event handling (currently not used)
        }
    }
}