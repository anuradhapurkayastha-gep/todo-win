using System;

namespace api.layer
{
    public class UserDetails
    {
        public string login { get; set; }
        public int id { get; set; }
        public string url { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}
