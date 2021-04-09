using CoreMVCWebApp.Models;
using CoreMVCWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVCWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IInjectService1 _injectService1;
        private readonly IInjectService2 _injectService21;
        private readonly IInjectService2 _injectService22;
        private readonly IInjectService3 _injectService31;
        private readonly IInjectService3 _injectService32;

        public HomeController(ILogger<HomeController> logger, IInjectService1 injectService1,IInjectService2 injectService21, IInjectService2 injectService22, IInjectService3 injectService31, IInjectService3 injectService32)
        {
            _logger = logger;
            _injectService1 = injectService1;
            _injectService21 = injectService21;
            _injectService22 = injectService22;
            _injectService31 = injectService31;
            _injectService32 = injectService32;
        }

        public IActionResult Index()
        {
            return View(new InjectModel { 
                ID1=_injectService1.GetID(),
                ID21=_injectService21.GetID(),
                ID22 = _injectService22.GetID(),
                ID31 = _injectService31.GetID(),
                ID32 =_injectService32.GetID()});
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            throw new Exception("An custom error!");
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}
