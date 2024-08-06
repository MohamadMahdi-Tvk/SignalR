﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SignalR.Controllers
{
    [Authorize]
    public class SupportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
