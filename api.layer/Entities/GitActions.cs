namespace api.layer
{
    public class GitActions
    {
        public string action { get; set; }

        public int number { get; set; }

        public PullRequestDetails pull_request { get; set; }

        public Sender sender { get; set; }

        public object repository { get; set; }

        public object organization { get; set; }

        public object installation { get; set; }

        public CheckRun check_run { get; set; }
    }

    public class PullRequestDetails
    {
        public string url { get; set; }
    }

    public class Sender
    {
        public string login { get; set; }

        public long id { get; set; }
    }

    public class CheckRun
    {
        public PullRequests[] pull_requests { get; set; }
    }

    public class PullRequests
    {
        public int number { get; set; }
    }
}
