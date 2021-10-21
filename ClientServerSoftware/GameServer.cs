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
        public static NetworkStream Stream = null;
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
                ServerPlayer.Type = TypePlayer.Cross;
                formGame.PlayerThis = ServerPlayer;
                formGame.Text = "Крестики нолики (Сервер)";
                FormGame = formGame;
                FormGame.SetInfo("Ждём второго игрока ...");
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        public static void Process()
        {
            TcpClient client = null;
            client = list.AcceptTcpClient();
            while (true)
            {
                try
                {
                    Stream = client.GetStream();
                    Stream.Read(bytes, 0, bytes.Length);
                    var obj = Carrier.Deserialize(bytes);
                    if(obj is Player)
                    {
                        ClientPlayer = (Player)obj;
                        FormGame.Player2 = ClientPlayer;
                        FormGame.SetInfo($"С вами играет {ClientPlayer.Name}");
                        Carrier.Send(Stream, ServerPlayer);
                        FormGame.NewGame();
                    }
                    if (obj is Step)
                    {
                        Step step = obj as Step;
                        FormGame.NextStep(step);
                    }
                }
                catch (Exception ex)
                {
                    FormGame.SetInfo($"Соединение не установлено");
                    break;
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
                    listenThread?.Abort();
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
