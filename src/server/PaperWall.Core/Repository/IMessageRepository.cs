using System.Collections.Generic;
using PaperWall.Core.DomainObject;

namespace PaperWall.Core.Repository
{
    public interface IMessageRepository
    {
        List<Message> GetByLocation(double latitude, double longitude, double precision);
        Message Get(long messageId);
        bool Remove(long messageToRemoveId);
        Message Remove(Message messageToRemove);
        bool Save(Message messageToSave);
        void Initialize();
        void Finalize();
    }
}