using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ClientServerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            #region TCP
            //const string ip = "127.0.0.1";

            //const int port = 8080;

            //// точка подключения
            //var tcpEndpoint = new IPEndPoint(IPAddress.Parse(ip), port);

            ////создаем и настраиваем сокет
            //var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            ////переводим сокет в режим ожидания(прослушивания порта)
            //tcpSocket.Bind(tcpEndpoint);
            //tcpSocket.Listen(5);    //5 - очередь подключаемых соединений - 5 машин


            ////процесс прослушивание должен быть постоянным
            //while (true)
            //{
            //    var listener = tcpSocket.Accept();// создается под кадого клиента, затем уничтожается
            //    var buffer = new byte[256];
            //    var size = 0; //кол-во байт сообщения
            //    var data = new StringBuilder();//собираем строку из кусков по 256 байт
            //    do
            //    {
            //        //получаем число байт из связанного буфера 
            //        size = listener.Receive(buffer);
            //        data.Append(Encoding.UTF8.GetString(buffer, 0, size)); //добавляем в строку строку длиной size байт
            //    }
            //    while (listener.Available > 0);//пока есть данные для добавления в data

            //    Console.WriteLine(data);

            //    //Дать ответ
            //    listener.Send(Encoding.UTF8.GetBytes("Успешно"));

            //    //Закрываем соединение
            //    listener.Shutdown(SocketShutdown.Both);
            //    listener.Close();



            //}
            #endregion

            #region UDP
            const string ip = "127.0.0.1";

            const int port = 8081;

            // точка подключения
            var udpEndpoint = new IPEndPoint(IPAddress.Parse(ip), port);

            //создаем и настраиваем сокет
            var udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            udpSocket.Bind(udpEndpoint);

            while (true)
            {
                var buffer = new byte[256];
                var size = 0;
                var data = new StringBuilder();
                EndPoint senderEndPoint = new IPEndPoint(IPAddress.Any, 0);// сохраняем адрес клиента
                do
                {
                    size = udpSocket.ReceiveFrom(buffer, ref senderEndPoint);
                    data.Append(Encoding.UTF8.GetString(buffer));
                }
                while (udpSocket.Available > 0);
                udpSocket.SendTo(Encoding.UTF8.GetBytes("Сообщение получено"), senderEndPoint);

                Console.WriteLine(data);
            }
            #endregion
        }
    }
}
