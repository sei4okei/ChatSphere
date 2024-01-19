using ChatSphere.Hubs;
using ChatSphere.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Runtime.CompilerServices;

namespace ChatSphere.Controllers
{
    public class ChatController : Controller
    {
        private IHubContext<ChatHub> hubContext;

        public ChatController(IHubContext<ChatHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        //Плохое решение - обновляет страницу и все сообщения пропадают :/
        //Думаю решаемо с помощью Local Storage'а но суть работы SignalR - усвоена.
        //Также в wwwroot/js/chat.js более приятное решение без обновления страницы
        //и с сохранением сообщений.
        [HttpPost]
        public async Task<IActionResult> SendMessage([FromForm] MessageModel messages)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index", messages);

            await hubContext.Clients.All.SendAsync("ReceiveMessage", messages.User, messages.Message);

            return RedirectToAction("Index");
        }
    }
}
