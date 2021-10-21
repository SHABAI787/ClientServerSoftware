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
            int port = Convert.ToInt32(textBoxPort.Text);
            if (GameServer.State == GameServerState.Stop)
                GameServer.StartGame(port, textBoxName.Text, formGame);
            else
            {
                GameServer.StopGame();
                GameServer.StartGame(port, textBoxName.Text, formGame);
            }
        }
    }
}
