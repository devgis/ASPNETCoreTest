using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVCWebApp.Services
{
    public class InjectService2 : IInjectService2
    {
        private string id = Guid.NewGuid().ToString();
        public string GetID()
        {
            return "Scoped2:" + id;
        }
    }
}
