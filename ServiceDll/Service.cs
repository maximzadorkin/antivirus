using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceDll
{
    public class Service : IService
    {
        public string Method1(string x)
        {
            string s = $"1 You entered: {x} = = = 1";
            return s;
        }

        public string Method2(string x)
        {
            string s = $"2 you entered: {x} = = = 2";
            return s;
        }
    }
}
