namespace api.layer
{
    public class GitActions
    {
        public string action { get; set; }

        public int number { get; set; }

        public PullRequest pull_request { get; set; }

        public object sender { get; set; }

        public object repository { get; set; }

        public object organization { get; set; }

        public object installation { get; set; }
    }

    public class PullRequest
    {
        public string url { get; set; } 
    }
}
