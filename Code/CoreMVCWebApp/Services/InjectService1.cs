using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVCWebApp.Services
{
    public class InjectService1 : IInjectService1
    {
        private string id = Guid.NewGuid().ToString();
        public string GetID()
        {
            return "Singleton1:" + id;
        }
    }
}
