using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ClientServerSoftware
{
    public static class GameServer
    {
        private static byte[] bytes = new byte[1024];
        private static TcpListener list = null;
        private static Thread listenThread = null;
        public static GameServerState State;
        public static Player ClientPlayer = null;  
        public static Player ServerPlayer = null;  
        public static FormGame FormGame = null;  
        public static void StartGame(int port, string namePlayer, FormGame formGame)
        {
            try
            {
                list = new TcpListener(IPAddress.Any, port);
                list.Start();
                listenThread = new Thread(Process);
                listenThread.IsBackground = true;
                listenThread.Start();
                State = GameServerState.Start;
                ServerPlayer = new Player();
                ServerPlayer.Name = namePlayer;
                FormGame = formGame;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        public static void Process()
        {
            TcpClient client = null;
            NetworkStream stream = null;

            while (true)
            {
                try
                {
                    client = list.AcceptTcpClient();
                    stream = client.GetStream();
                    stream.Read(bytes, 0, bytes.Length);
                    var obj = Carrier.Deserialize(bytes);
                    if(obj is Player)
                    {
                        ClientPlayer = (Player)obj;
                        Carrier.Send(stream, ServerPlayer);
                    }
                    if (obj is Step)
                    {
                        Step step = obj as Step;
                        FormGame.NextStep(step);
                    }
                }
                catch (Exception ex)
                {
                }
                bytes = new byte[1024];
            }
        }

        public static void StopGame()
        {
            try
            {
                if (list != null)
                {
                    list.Stop();
                    listenThread.Abort();
                }
                State = GameServerState.Stop;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
    }

    public enum GameServerState
    {
        Stop,
        Start,
    }
}
