using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AntService
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
            this.ServiceName = "Antivirus";
        }

        protected override void OnStart(string[] args)
        {
            this.client_service__Interface();
        }

        protected override void OnStop()
        {
        }

        private void client_service__Interface() {
            int port = 8014;
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            listenSocket.Bind(ipPoint);
            listenSocket.Listen(10);

            while (true)
            {
                Socket handler = listenSocket.Accept();
                
                // получаем данные
                StringBuilder builder = new StringBuilder();
                int bytes = 0; 
                byte[] data = new byte[128]; 

                do
                {
                    bytes = handler.Receive(data);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                }
                while (handler.Available > 0);

                // действия с данными
                this.switchCommands(builder.ToString());

                // отправляем ответ
                //string message = "";
                //data = Encoding.Unicode.GetBytes(message);
                //handler.Send(data);

                // закрываем сокет
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
        }

        private void switchCommands(string command)
        {
            try
            {
                string typeOfCommand = command.Split('_')[0];
                switch (typeOfCommand)
                {
                    case "scan":
                        this.scanCommandsHandler(command);
                        return;
                    case "monitoring":
                        this.monitoringCommandsHandler(command);
                        return;
                    case "plans":
                        this.plansCommandsHandler(command);
                        return;
                    default:
                        return;
                }
            } finally { }
        }

        private void scanCommandsHandler(string command)
        {
            string typeOfCommand = command.Split('_')[1];
            switch (typeOfCommand)
            {
                case "000":
                    Thread thread = new Thread(new ThreadStart(
                        () => Scanner.scanning(command)
                    ));
                    thread.Start();
                    // add to all processes
                    return;
                case "001":
                    // find in processes and stopvc
                    return;
            }
        }

        private void monitoringCommandsHandler(string command)
        {

        }

        private void plansCommandsHandler(string command)
        {

        }
    }
}
