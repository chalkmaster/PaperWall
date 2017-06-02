using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using PaperWall.Core.DomainObject;
using PaperWall.Core.Infrastructure;
using PaperWall.Core.Repository;
using Paperwall.Api.Models;

namespace Paperwall.Api.Controllers
{
    public class MessageController : ApiController
    {
        private readonly IMessageRepository _messageRepository;

        public MessageController()
        {
            _messageRepository = IoC.Resolve<IMessageRepository>();
        }

        public string Get()
        {
            return "I'm alive!";
        }

        public IList<MessageDataModel> Get(double latitude, double longitude, string readerIdentification)
        {
            IoC.Resolve<IMessageRepository>().Initialize();
            var result = _messageRepository.GetByLocation(latitude, longitude, 15);
            IoC.Resolve<IMessageRepository>().Finalize();
            return result.Select(m => new MessageDataModel { Message = m.MessageText, PostedAt = m.PostedAt }).ToList();
            
        }
        
        public IList<MessageDataModel> Get(double latitude, double longitude, double precision,string readerIdentification)
        {
            IoC.Resolve<IMessageRepository>().Initialize();
            var result = _messageRepository.GetByLocation(latitude, longitude, precision);
            IoC.Resolve<IMessageRepository>().Finalize();
            return result.Select(m => new MessageDataModel {Message = m.MessageText, PostedAt = m.PostedAt}).ToList();
        }

        public bool Post(ReceivedMessageModel receivedMessage)
        {
            IoC.Resolve<IMessageRepository>().Initialize();
            _messageRepository.Save(new Message
            {
                Longitude = receivedMessage.longitude,
                Latitude = receivedMessage.latitude,
                Precision = receivedMessage.precision,
                MessageText = receivedMessage.messageText,
                Writter = receivedMessage.writerIdentification,
                PostedAt = DateTime.Now
            });
            IoC.Resolve<IMessageRepository>().Finalize();
            return true;

        }

    }
}
