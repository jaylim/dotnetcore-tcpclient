using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Text;

namespace tcpclient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Connecting to server");
            TcpClient client = new TcpClient("127.0.0.1", 3030);
            NetworkStream stream = client.GetStream();
            while (client.Connected) {
                byte[] buffer = new byte[1024];
                if (stream.Read(buffer, 0, buffer.Length) > 0) {
                    int idx = Array.IndexOf(buffer, (byte) '\n');
                    string msg = Encoding.ASCII.GetString(buffer, 0, idx);
                    if (msg.Equals("BEAT")) {
                        Console.WriteLine("Status: OK");
                    }
                } else {
                    client.Close();
                    Console.WriteLine("Status: Server connection reset.");
                }
            }
        }
    }
}
