using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVCWebApp.Services
{
    public class InjectService3 : IInjectService3
    {
        private string id = Guid.NewGuid().ToString();
        public string GetID()
        {
            return "Transient3:" + id;
        }
    }
}
