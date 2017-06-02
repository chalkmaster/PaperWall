using System;
using System.Collections.Generic;
using System.Linq;
using PaperWall.Core.DomainObject;
using PaperWall.Core.Repository;

namespace PaperWall.Repository.InMemory
{
    public class InMemoryMessageRepository : IMessageRepository
    {
        private const int Precision = 3;
        private int lastId = 0;
        private List<Message> _inMemoryDataBase;

        private List<Message> InMemoryDataBase { get { return _inMemoryDataBase ?? (_inMemoryDataBase = new List<Message>()); } } 

        public List<Message> GetByLocation(double latitude, double longitude, double precision)
        {
            return InMemoryDataBase.Where(m => Math.Round(m.Latitude, Precision) == Math.Round(latitude, Precision)
                                                && Math.Round(m.Longitude, Precision) == Math.Round(longitude, Precision)).ToList();
        }
        public Message Get(long messageId)
        {
            return InMemoryDataBase.FirstOrDefault(m => m.Id == messageId);            
        }

        public bool Remove(long messageToRemoveId)
        {
            var messageToUpdate = InMemoryDataBase.FirstOrDefault(m => m.Id == messageToRemoveId);
            if (messageToUpdate != null)
                messageToUpdate.Removed = true;
            return true;            
        }
        public Message Remove(Message messageToRemove)
        {
            messageToRemove.Removed = Remove(messageToRemove.Id);
            return messageToRemove;
        }

        public bool Save(Message messageToSave)
        {
            messageToSave.Removed = false;
            
            if (messageToSave.Id == 0)
            {
                messageToSave.Id = ++lastId;
                InMemoryDataBase.Add(messageToSave);
            }

            var messageToUpdate = InMemoryDataBase.FirstOrDefault(m => m.Id == messageToSave.Id);
            if (messageToUpdate != null)
                messageToUpdate.MessageText = messageToSave.MessageText;
            
            return true;
        }

        public void Initialize()
        {
            
        }

        public void Finalize()
        {
            
        }
    }
}
