using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace api.layer.BusinessLayer
{
    public interface IGitActionsManager
    {
        public Task<JObject> OpenRequestedCreated(GitActions gitActions);

        public bool PullRequestedCreated(GitActions gitActions);
    }
}
