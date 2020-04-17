using System;

namespace WebCalendar.DAL.Models.Entities
{
    public class PushSubscription : IEntity
    {
        public Guid Id { get; set; }
        
        public string Endpoint { get; set; }
        public string P256DH { get; set; }
        public string Auth { get; set; }
        public User User { get; set; }

        public DateTime AddedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}