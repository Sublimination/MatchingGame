using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace MatchingGame
{
    public partial class Form1 : Form
    {
        //Init variables
        Random rnd = new Random();
        Label firstClicked = null;
        Label secondClicked = null;
        
        //Init list with letters for Webdings icons
        List<string> icons = new List<string>()
        {
            "!", "!", "N", "N", ",", ",", "k", "k",
            "b", "b", "v", "v", "w", "w", "z", "z"
        };

        //Function to assign random tiles to labels
        private void AssignIconsToSquares()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLable = control as Label;
                if (iconLable != null)
                {
                    int randomNumber = rnd.Next(icons.Count);
                    iconLable.Text = icons[randomNumber];
                    iconLable.ForeColor = iconLable.BackColor;
                    icons.RemoveAt(randomNumber);
                }
            }
        }

   
        public Form1()
        {
            InitializeComponent();
            AssignIconsToSquares();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label_Click(object sender, EventArgs e)
        {
            Label clickedLabel = sender as Label;

            if (timer1.Enabled == true)
                return;

            if (clickedLabel != null)
            {
                if (clickedLabel.ForeColor == Color.Black)
                    return;
                if (firstClicked == null) 

                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;
                    return;
                }
                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;

                CheckForWinner();

                if (firstClicked.Text == secondClicked.Text)
                {
                    System.Media.SystemSounds.Asterisk.Play();
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }

                timer1.Start();
                return;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            firstClicked = null;
            secondClicked = null;
        }
        private void CheckForWinner()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                };
            }
            MessageBox.Show("You matched all the icons!", "Congratulations!");
            Close();
        }
    }
}
