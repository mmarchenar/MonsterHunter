using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MonsterHunterFrm
{
    public partial class Form2 : Form
    {
        // Constructor for Form2
        public Form2()
        {
            InitializeComponent();
            try
            {
                // Populate the comboBox with available maps from the Core
                foreach (String map in Core.availableMaps)
                {
                    comboBox1.Items.Add(map);
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during initialization
                MessageBox.Show("Error initializing form: " + ex.Message);
            }
        }

        // Event handler for form load event
        private void Form2_Load(object sender, EventArgs e)
        {
            try
            {
                // Additional initialization can be done here if needed
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during form load
                MessageBox.Show("Error loading form: " + ex.Message);
            }
        }

        // Event handler for button1 click event
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the name from textBox1 and assign it to Core.Name
                String Name = textBox1.Text;
                Core.Name = Name;

                // Dispose of the form after setting the name
                Dispose();
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur when button1 is clicked
                MessageBox.Show("Error processing button click: " + ex.Message);
            }
        }

        // Event handler for label1 click event (currently not used)
        private void label1_Click(object sender, EventArgs e)
        {
            try
            {
                // Code for handling label click can be added here if needed
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during label click
                MessageBox.Show("Error processing label click: " + ex.Message);
            }
        }

        // Event handler for button2 click event (to close the form)
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Dispose of the form when button2 is clicked
                Dispose();
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur when button2 is clicked
                MessageBox.Show("Error processing button click: " + ex.Message);
            }
        }

        // Event handler for comboBox selection change event
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Get the selected map from comboBox1 and assign it to Core.chosenMap
                String chosenMap = comboBox1.SelectedItem as String;
                Core.chosenMap = chosenMap;
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during selection change
                MessageBox.Show("Error selecting map: " + ex.Message);
            }
        }

        // Event handler for textBox1 text changed event (currently not used)
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // Code for handling text changes can be added here if needed
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during text change
                MessageBox.Show("Error processing text change: " + ex.Message);
            }
        }
    }
}