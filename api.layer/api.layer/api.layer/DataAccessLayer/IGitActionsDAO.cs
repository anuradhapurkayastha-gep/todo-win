using api.layer.Entities;
using System.Threading.Tasks;

namespace api.layer.DataAccessLayer
{
    public interface IGitActionsDAO
    {
        public Task<bool> SavePullRequestDetails(PullRequestEntity pullRequestEntity);
    }
}
