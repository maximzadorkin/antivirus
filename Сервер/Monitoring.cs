using System;
using System.Collections.Generic;
using System.Text;

namespace Сервер
{
    class Monitoring
    {
        public static bool work = false;
        private string path;

        public Monitoring(string command)
        {
            string[] commandParts = command.Split('_');
            this.path = commandParts[2];

            Scanner.work = true;
            this.start();
        }

        private void start()
        {
            while (work)
            {
                // отправлять логи
            }
            // отослать результаты
        }
    }
}
