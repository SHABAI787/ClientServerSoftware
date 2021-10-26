using ClientServerSoftware.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace ClientServerSoftware
{
    public partial class FormGame : Form
    {
        private FormNewGame formNewGame;

        public static List<Step> steps = new List<Step>();
        public Player PlayerThis = null;
        public Player Player2 = null;
        public bool IsClient = false;
        public bool ThisPlayerSetStep = true;
        public FormGame()
        {
            InitializeComponent();
            formNewGame = new FormNewGame(this);
        }

        private void создатьИгруToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formNewGame.ShowDialog();
        }

        private void FormGame_Load(object sender, EventArgs e)
        {

        }

        public void NextStep(Step step, bool thisPlayerSetStep = true)
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
                case StepIndex.I9: SetImage(pBox9, step.Player.Type); break;
            }
            ThisPlayerSetStep = thisPlayerSetStep;
        }

        private void CheckStep(TypePlayer typePlayer)
        {
            List<Step> stepsCross = steps.Where(s => s.Player.Type == typePlayer).ToList();
            if (stepsCross.Any(s => s.Index == StepIndex.I1)
                && stepsCross.Any(s => s.Index == StepIndex.I2)
                && stepsCross.Any(s => s.Index == StepIndex.I3)) ShowLine(Lines.L1, typePlayer);
            if (stepsCross.Any(s => s.Index == StepIndex.I4)
                && stepsCross.Any(s => s.Index == StepIndex.I5)
                && stepsCross.Any(s => s.Index == StepIndex.I6)) ShowLine(Lines.L2, typePlayer);
            if (stepsCross.Any(s => s.Index == StepIndex.I7)
              && stepsCross.Any(s => s.Index == StepIndex.I8)
              && stepsCross.Any(s => s.Index == StepIndex.I9)) ShowLine(Lines.L3, typePlayer);

            if (stepsCross.Any(s => s.Index == StepIndex.I1)
             && stepsCross.Any(s => s.Index == StepIndex.I4)
             && stepsCross.Any(s => s.Index == StepIndex.I7)) ShowLine(Lines.L4, typePlayer);
            if (stepsCross.Any(s => s.Index == StepIndex.I2)
            && stepsCross.Any(s => s.Index == StepIndex.I5)
            && stepsCross.Any(s => s.Index == StepIndex.I8)) ShowLine(Lines.L5, typePlayer);
            if (stepsCross.Any(s => s.Index == StepIndex.I3)
            && stepsCross.Any(s => s.Index == StepIndex.I6)
            && stepsCross.Any(s => s.Index == StepIndex.I9)) ShowLine(Lines.L6, typePlayer);

            if (stepsCross.Any(s => s.Index == StepIndex.I1)
           && stepsCross.Any(s => s.Index == StepIndex.I5)
           && stepsCross.Any(s => s.Index == StepIndex.I9))
            {
                pBox4.Hide();
                pBox7.Hide();
                pBox8.Hide();
                pBox2.Hide();
                pBox3.Hide();
                pBox6.Hide();
                ShowGameOver(typePlayer);
            }
            if (stepsCross.Any(s => s.Index == StepIndex.I3)
          && stepsCross.Any(s => s.Index == StepIndex.I5)
          && stepsCross.Any(s => s.Index == StepIndex.I7))
            {
                pBox1.Hide();
                pBox2.Hide();
                pBox4.Hide();
                pBox6.Hide();
                pBox8.Hide();
                pBox9.Hide();
                ShowGameOver(typePlayer);
            }
        }

        private void ShowGameOver(TypePlayer typePlayer)
        {
            if (typePlayer == PlayerThis.Type)
                MessageBox.Show(this, "Победа!", "Победа", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show(this, $"Победил {Player2.Name}", "Поражение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            panel1.Enabled = false;
        }

        public void SetInfo(string text)
        {
            this.Invoke((MethodInvoker)delegate
            {
                labelInfo.Text = text;
            });
        }

        public void NewGame()
        {
            this.Invoke((MethodInvoker)delegate
            {
                panel1.Visible = true;
                pBox1.Show(); pBox1.Image = null;
                pBox2.Show(); pBox2.Image = null;
                pBox3.Show(); pBox3.Image = null;
                pBox4.Show(); pBox4.Image = null;
                pBox5.Show(); pBox5.Image = null;
                pBox6.Show(); pBox6.Image = null;
                pBox7.Show(); pBox7.Image = null;
                pBox8.Show(); pBox8.Image = null;
                pBox9.Show(); pBox9.Image = null;

                pBox10.Visible = false;
                pBox11.Visible = false;
                pBox12.Visible = false;
                pBox13.Visible = false;
                pBox14.Visible = false;
                pBox15.Visible = false;
                steps.Clear();
                panel1.Enabled = true;
            });
        }

        private void ShowLine(Lines line, TypePlayer typePlayer)
        {
            this.Invoke((MethodInvoker)delegate
            {
                switch (line)
                {
                    case Lines.L1: pBox10.Visible = true; break;
                    case Lines.L2: pBox11.Visible = true; break;
                    case Lines.L3: pBox12.Visible = true; break;
                    case Lines.L4: pBox13.Visible = true; break;
                    case Lines.L5: pBox14.Visible = true; break;
                    case Lines.L6: pBox15.Visible = true; break;
                }
                ShowGameOver(typePlayer);
            });
        }

        private void SetImage(PictureBox pictureBox, TypePlayer typePlayer)
        {
            this.Invoke((MethodInvoker)delegate
            {
                if (typePlayer == TypePlayer.Cross)
                    pictureBox.Image = Resources.Cross;
                else
                    pictureBox.Image = Resources.Zero;

                CheckStep(TypePlayer.Cross);
                CheckStep(TypePlayer.Zero);
            });
        }

        private void FormGame_FormClosed(object sender, FormClosedEventArgs e)
        {
            GameServer.StopGame();
        }

        private void подключитьсяКИгреToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormConnectGame(this).ShowDialog();
        }

        private void pBox_Click(object sender, EventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;
            if (pictureBox.Image == null && ThisPlayerSetStep)
            {
                Step step = new Step();
                step.Player = this.PlayerThis;
                step.Index = (StepIndex)Convert.ToInt32(pictureBox.Tag);
                if (IsClient)
                    Carrier.Send(GameClient.Client.GetStream(), step);
                else
                    Carrier.Send(GameServer.Stream, step);
                NextStep(step, false);
            }
        }
    }

    public enum Lines
    {
        L1 = 10,
        L2 = 11,
        L3 = 12,
        L4 = 13,
        L5 = 14,
        L6 = 15,
        L7 = 16,
        L8 = 17,
    }
}
