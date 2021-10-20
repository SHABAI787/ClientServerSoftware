using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ClientServerSoftware
{
    public static class Carrier
    {
        public static object Deserialize(byte[] data)
        {
            object obj = null;
            try
            {
                using (var memoryStream = new MemoryStream(data))
                    obj = (new BinaryFormatter()).Deserialize(memoryStream);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

            return obj;
        }

        public static void Send(NetworkStream networkStream, object obj)
        {
            var data = Serialize(obj);
            networkStream.Write(data, 0, data.Length);
        }

        public static byte[] Serialize(object anySerializableObject)
        {
            using (var memoryStream = new MemoryStream())
            {
                (new BinaryFormatter()).Serialize(memoryStream, anySerializableObject);
                return memoryStream.ToArray();
            }
        }
    }
}
