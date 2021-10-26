using System;
using System.Windows.Forms;

namespace ClientServerSoftware
{
    public partial class FormConnectGame : Form
    {
        private FormGame formGame;
        public FormConnectGame(FormGame formGame)
        {
            InitializeComponent();
            this.formGame = formGame;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GameClient.Connect(textBoxIP.Text, Convert.ToInt32(textBoxPort.Text), textBoxName.Text, formGame);
            this.Hide();
        }
    }
}
