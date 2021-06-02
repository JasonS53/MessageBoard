using MessageBoard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MessageBoard.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [HttpDelete]
        public IActionResult Delete(int id, string message)
        {
            Message message1 = new Message();
            using (MessageBoardDbContext context = new MessageBoardDbContext())
            {
                message1 = context.Message.ToList().Find(m => m.Id == id);
                message1.Message1 = message;
                context.Message.Remove(message1);
                context.SaveChanges();
            };
            return RedirectToAction("MessageBoard");
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edited(int id, string message)
        {
            Message message1 = new Message();
            using (MessageBoardDbContext context = new MessageBoardDbContext())
            {
                message1 = context.Message.ToList().Find(m => m.Id == id);
                message1.Message1 = message;
                message1.Updated = true;
                context.SaveChanges();
            };
            return RedirectToAction("MessageBoard");
        }

        public IActionResult Edit(int id)
        {
            using (MessageBoardDbContext context = new MessageBoardDbContext())
            {
                Message message = new Message();
                message = context.Message.ToList().Find(m => m.Id == id);
                return View(message);
            };
        }

        [Authorize]
        [HttpPost]
        public IActionResult MessageBoard(string message)
        {
            Message message1 = new Message();
            using (MessageBoardDbContext context = new MessageBoardDbContext())
            {
                message1.UserId = User.Identity.Name; 
                message1.PostedTime = DateTime.Now;
                message1.Message1 = message;
                message1.Updated = false;
                context.Message.Add(message1);
                context.SaveChanges();
            };
            return Redirect("MessageBoard");
        }

        [Authorize]
        public IActionResult MessageBoard()
        {
            List<Message> messageList = new List<Message>();
            using (MessageBoardDbContext context = new MessageBoardDbContext())
            {
                messageList = context.Message.ToList();
            }
            return View(messageList);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
