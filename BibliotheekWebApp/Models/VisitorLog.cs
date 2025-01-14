using System;

namespace BibliotheekApp.Models
{
    public class VisitorLog
    {
        public int Id { get; set; }
        public string IpAddress { get; set; }
        public string Url { get; set; }
        public string Method { get; set; }
        public DateTime Timestamp { get; set; }
    }
}

