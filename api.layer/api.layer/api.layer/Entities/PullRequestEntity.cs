using System;

namespace api.layer.Entities
{
    public class PullRequestEntity
    {
        public string action { get; set; }

        public long? userid { get; set; }

        public int? number { get; set; }

        public DateTime created_at { get; set; }

        public DateTime updated_at { get; set; }

        public DateTime? closed_at { get; set; }

        public DateTime? merged_at { get; set; }

        public string commits_url { get; set; }

        public string review_comments_url { get; set; }

        public DateTime? pushed_at { get; set; }
    }
}
