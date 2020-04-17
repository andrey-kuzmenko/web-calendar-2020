namespace WebCalendar.WebApi.Models
{
    public class PushSubscriptionModel
    {
        public string Endpoint { get; set; }
        public string P256DH { get; set; }
        public string Auth { get; set; }
    }
}