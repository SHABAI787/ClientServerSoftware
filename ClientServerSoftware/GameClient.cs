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
        private static TcpClient client = null;
        private static NetworkStream stream = null;
        private static Thread listenThread = null;
        public static Player ClientPlayer = null;
        public static Player ServerPlayer = null;
        public static StateClient State;
        public static void Connect(string iPAddress, int port, string namePlayer, FormGame formGame)
        {
            FormGame = formGame;
            ClientPlayer = new Player();
            ClientPlayer.Name = namePlayer;
            try
            {
                client = new TcpClient(iPAddress, port);
                stream = client.GetStream();
                listenThread = new Thread(Process);
                Carrier.Send(stream, ClientPlayer);
                listenThread.Start();
                State = StateClient.Connect;
            }
            catch (Exception ex)
            {

            }
        }

        public static void Disconnect()
        {
            stream.Close();
            client.Close();
            State = StateClient.Disconnect;
        }

        public static void Process()
        {
            Byte[] data = new Byte[1024];

            try
            {
                while (true)
                {
                    int sizeReadBytes = stream.Read(data, 0, data.Length);
                    if (sizeReadBytes == 0)
                        break;

                    var obj = Carrier.Deserialize(data);

                    if (obj is Player)
                    {
                        ServerPlayer = (Player)obj;
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
            }
        }

        public enum StateClient
        {
            Disconnect,
            Connect,
        }
    }
}
