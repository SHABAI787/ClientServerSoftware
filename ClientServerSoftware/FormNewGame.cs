using System;
using System.Windows.Forms;

namespace ClientServerSoftware
{
    public partial class FormNewGame : Form
    {
        private FormGame formGame;
        public FormNewGame(FormGame formGame)
        {
            InitializeComponent();
            this.formGame = formGame;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            formGame.panel1.Hide();
            int port = Convert.ToInt32(textBoxPort.Text);
            if (GameServer.State == GameServerState.Stop)
                GameServer.StartGame(port, textBoxName.Text, formGame);
            else
            {
                GameServer.StopGame();
                GameServer.StartGame(port, textBoxName.Text, formGame);
            }
            this.Hide();
        }
    }
}
