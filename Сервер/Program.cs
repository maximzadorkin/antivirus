using System;
using System.IO.MemoryMappedFiles;
using System.Threading;

namespace Сервер
{
    class Program
    {
        static int size = 2048;
        static MemoryMappedFile sharedMemory = MemoryMappedFile.CreateOrOpen("MZAntivirus_commands_fm", size);
        static string commandsMutexName = "MZAntivirus_commands_mutex";
        static Mutex commandsMutex = new Mutex(false, commandsMutexName);

        static void Main(string[] args)
        {
            client_service__Interface();
        }

        private static void client_service__Interface()
        {
            MemoryMappedViewAccessor file = sharedMemory.CreateViewAccessor(0, size);

            while (true)
            {
                commandsMutex.WaitOne();
                int messageSize = file.ReadInt32(0);
                char[] message = new char[messageSize];
                file.ReadArray<char>(0, message, 0, messageSize);
                switchCommands(message.ToString());
                Console.WriteLine(message);
                commandsMutex.ReleaseMutex();
            }
        }

        private static void switchCommands(string command)
        {
            try
            {
                string typeOfCommand = command.Split('_')[0];
                switch (typeOfCommand)
                {
                    case "scan":
                        scannerCommandsHandler(command);
                        return;
                    case "monitoring":
                        monitoringCommandsHandler(command);
                        return;
                    case "plans":
                        plansCommandsHandler(command);
                        return;
                    default:
                        return;
                }
            }
            finally { }
        }

        private static void scannerCommandsHandler(string command)
        {
            string typeOfCommand = command.Split('_')[1];
            switch (typeOfCommand)
            {
                case "000":
                    Thread thread = new Thread(new ThreadStart(() => new Scanner(command)));
                    thread.Start();
                    return;
                case "001":
                    Scanner.work = false;
                    return;
            }
        }

        private static void monitoringCommandsHandler(string command)
        {
            string typeOfCommand = command.Split('_')[1];
            switch (typeOfCommand)
            {
                case "000":
                    Thread thread = new Thread(new ThreadStart(() => new Monitoring(command)));
                    thread.Start();
                    return;
                case "001":
                    Monitoring.work = false;
                    return;
            }
        }

        private static void plansCommandsHandler(string command)
        {
            string typeOfCommand = command.Split('_')[1];
            switch (typeOfCommand)
            {
                case "000":
                    Thread threadForGet = new Thread(new ThreadStart(() => Plans.getPlans()));
                    threadForGet.Start();
                    return;
                case "001":
                    Thread threadForAdd = new Thread(new ThreadStart(() => Plans.addPlan()));
                    threadForAdd.Start();
                    return;
                case "002":
                    Thread threadForDelete = new Thread(new ThreadStart(() => Plans.deletePlan()));
                    threadForDelete.Start();
                    return;
            }
        }
    }
}
