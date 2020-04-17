namespace WebCalendar.Services.PushNotification.Models
{
    public class PushSubscriptionServiceModel
    {
        public string Endpoint { get; set; }
        public string P256DH { get; set; }
        public string Auth { get; set; }
    }
}