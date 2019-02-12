using FireOnWheels.Web.Messages;
using FireOnWheels.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FireOnWheels.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult RegisterOrder()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterOrder(OrderViewModel model)
        {
            RegisterOrderCommand registerOrderCommand = new RegisterOrderCommand(model);

            RabbitMQManager rabbitMQManager = new RabbitMQManager();
            rabbitMQManager.SendRegisterOrderCommand(registerOrderCommand);

            return View("Thanks");
        }
    }
        
}
