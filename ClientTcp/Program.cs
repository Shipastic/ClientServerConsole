using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ClientTcp
{
    class Program
    {
        static void Main(string[] args)
        {
            #region TCP
            //const string ip = "127.0.0.1";

            //const int port = 8080;
            //var tcpEndpoint = new IPEndPoint(IPAddress.Parse(ip), port);
            //var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //Console.WriteLine("Введите сообщение");

            //var message = Console.ReadLine();

            //var data = Encoding.UTF8.GetBytes(message);

            ////сделать подключение для сокета
            //tcpSocket.Connect(tcpEndpoint);
            ////Отправить сообщение
            //tcpSocket.Send(data);

            //var buffer = new byte[256];
            //var size = 0; 
            //var answer = new StringBuilder();

            //do
            //{
            //    //получаем число байт из связанного буфера 
            //    size = tcpSocket.Receive(buffer);
            //    answer.Append(Encoding.UTF8.GetString(buffer, 0, size)); //добавляем в строку строку длиной size байт
            //}
            //while (tcpSocket.Available > 0);

            ////выведем сообщение на консоль
            //Console.WriteLine(answer.ToString());

            ////закрываем сокет
            //tcpSocket.Shutdown(SocketShutdown.Both);
            //tcpSocket.Close();
            //Console.ReadLine();
            #endregion

            const string ip = "127.0.0.1";

            const int port = 8082;

            // точка подключения
            var udpEndpoint = new IPEndPoint(IPAddress.Parse(ip), port);

            //создаем и настраиваем сокет
            var udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            udpSocket.Bind(udpEndpoint);

            while (true)
            {
                Console.WriteLine("Введите сообщение");
                var message = Console.ReadLine();
                var serverEndpoint = new IPEndPoint(IPAddress.Parse(ip), 8081);
                udpSocket.SendTo(Encoding.UTF8.GetBytes(message),serverEndpoint);

                var buffer = new byte[256];
                var size = 0;
                var data = new StringBuilder();
                EndPoint senderEndPoint = new IPEndPoint(IPAddress.Parse(ip), 8081);// сохраняем адрес клиента
                do
                {
                    size = udpSocket.ReceiveFrom(buffer, ref senderEndPoint);
                    data.Append(Encoding.UTF8.GetString(buffer));
                }
                while (udpSocket.Available > 0);

                Console.WriteLine(data);
                Console.ReadLine();
            }
        }
    }
}
