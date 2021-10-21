using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClientServerSoftware
{
    public static class GameClient
    {
        private static FormGame FormGame = null;
        private static Thread listenThread = null;
        public static NetworkStream Stream = null;
        public static TcpClient Client = null;
        public static Player ClientPlayer = null;
        public static Player ServerPlayer = null;
        public static StateClient State;
        public static void Connect(string iPAddress, int port, string namePlayer, FormGame formGame)
        {
            FormGame = formGame;
            ClientPlayer = new Player();
            ClientPlayer.Name = namePlayer;
            ClientPlayer.Type = TypePlayer.Zero;
            formGame.PlayerThis = ClientPlayer;
            formGame.IsClient = true;
            formGame.Text = "Крестики нолики (Клиент)";
            try
            {
                Client = new TcpClient(iPAddress, port);
                Stream = Client.GetStream();
                listenThread = new Thread(Process);
                Carrier.Send(Stream, ClientPlayer);
                listenThread.Start();
                State = StateClient.Connect;
            }
            catch (Exception ex)
            {

            }
        }

        public static void Disconnect()
        {
            Stream.Close();
            Client.Close();
            State = StateClient.Disconnect;
        }

        public static void Process()
        {
            Byte[] data = new Byte[1024];

            try
            {
                while (true)
                {
                    int sizeReadBytes = Stream.Read(data, 0, data.Length);
                    if (sizeReadBytes == 0)
                        break;

                    var obj = Carrier.Deserialize(data);

                    if (obj is Player)
                    {
                        ServerPlayer = (Player)obj;
                        FormGame.Player2 = ServerPlayer;
                        FormGame.SetInfo($"С вами играет {ServerPlayer.Name}");
                        FormGame.NewGame();
                    }

                    if (obj is Step)
                    {
                        Step step = obj as Step;
                        FormGame.NextStep(step);
                    }
                }
                Disconnect();
            }
            catch (Exception ex)
            {
                FormGame.SetInfo($"Соединение не установлено");
            }
        }

        public enum StateClient
        {
            Disconnect,
            Connect,
        }
    }
}
