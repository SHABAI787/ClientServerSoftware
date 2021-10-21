using ClientServerSoftware.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientServerSoftware
{
    public partial class FormGame : Form
    {
        public static List<Step> steps = new List<Step>();
        public FormGame()
        {
            InitializeComponent();
        }

        private void создатьИгруToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormNewGame(this).ShowDialog();
        }

        private void FormGame_Load(object sender, EventArgs e)
        {

        }

        public void NextStep(Step step)
        {
            steps.Add(step);
            switch (step.Index)
            {
                case StepIndex.I1: SetImage(pBox1, step.Player.Type); break;
                case StepIndex.I2: SetImage(pBox2, step.Player.Type); break;
                case StepIndex.I3: SetImage(pBox3, step.Player.Type); break;
                case StepIndex.I4: SetImage(pBox4, step.Player.Type); break;
                case StepIndex.I5: SetImage(pBox5, step.Player.Type); break;
                case StepIndex.I6: SetImage(pBox6, step.Player.Type); break;
                case StepIndex.I7: SetImage(pBox7, step.Player.Type); break;
                case StepIndex.I8: SetImage(pBox8, step.Player.Type); break;
            }
        }

        private void SetImage(PictureBox pictureBox, TypePlayer typePlayer)
        {
            if (typePlayer == TypePlayer.Cross)
                pictureBox.Image = Resources.Cross;
            else
                pictureBox.Image = Resources.Zero;
        }

        private void FormGame_FormClosed(object sender, FormClosedEventArgs e)
        {
            GameServer.StopGame();
        }

        //public static void EndGame(GameOver step)
        //{

        //}
    }
}
