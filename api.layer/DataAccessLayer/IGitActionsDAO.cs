using api.layer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api.layer.DataAccessLayer
{
    public interface IGitActionsDAO
    {
        public Task<bool> SavePullRequestDetails(PullRequestEntity pullRequestEntity);

        public Task<bool> SaveSonarDetails(int? PRId, Dictionary<string, dynamic> SonarMetic);

        public Task<List<RatingEntity>> FetchRaitingReport();
    }
}
