using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PaperWall.Core.DomainObject;
using PaperWall.Core.Infrastructure;
using PaperWall.Core.Repository;

namespace PaperWall.Web.Controllers
{
    public class MessageController : Controller
    {
        private readonly IMessageRepository _messageRepository;

        public MessageController()
        {
            _messageRepository = IoC.Resolve<IMessageRepository>();
        }

        public ActionResult Test()
        {
            return View();
        }
        
        public JsonResult Get(double latitude, double longitude, double precision,string readerIdentification)
        {
            var result = _messageRepository.GetByLocation(latitude, longitude, precision);
            return Json(result.Select(m => new {m.MessageText, PostedAt = m.PostedAt.ToShortDateString()}), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Post(string messageText, double latitude, double longitude, double precision, string writterIdentification)
        {
            _messageRepository.Save(new Message
                                                {
                                                    Longitude = longitude,
                                                    Latitude = latitude,
                                                    Precision = precision,
                                                    MessageText = messageText,
                                                    Writter = writterIdentification,
                                                    PostedAt = DateTime.Now
                                                });
            return View("Test");
        }
    }
}
