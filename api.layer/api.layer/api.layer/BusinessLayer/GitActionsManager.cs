using api.layer.DataAccessLayer;
using api.layer.Entities;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace api.layer.BusinessLayer
{
    public class GitActionsManager : IGitActionsManager
    {
        private readonly IGitActionsDAO _gitActionsDAO;

        public GitActionsManager(IGitActionsDAO gitActionsDAO)
        {
            _gitActionsDAO = gitActionsDAO;
        }

        public async Task<bool> OpenRequestedCreated(GitActions gitActions)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("User-Agent", "TODO-App");
                    using (var response = await httpClient.GetAsync(gitActions.pull_request.url))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        PullRequestEntity pullRequestEntity = JsonConvert.DeserializeObject<PullRequestEntity>(apiResponse);

                        pullRequestEntity.action = gitActions.action;
                        pullRequestEntity.userid = gitActions.sender.id;

                        await _gitActionsDAO.SavePullRequestDetails(pullRequestEntity);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return false;
        }

        public bool PullRequestedCreated(GitActions gitActions)
        {
            return true;
        }

        public async Task<PullRequestSonarDetails> PullRequestSonarDetails(string URL)
        {
            //ToDoConstants.PULL_REQUEST_SONAR_URL + pullRequestDetails.number.ToString()
            using (var httpClient = new HttpClient())
            {
                using (var sonarResponse = await httpClient.GetAsync(URL))
                {
                    string sonarApiResponse = await sonarResponse.Content.ReadAsStringAsync();
                    PullRequestSonarDetails pullRequestSonarDetails = JsonConvert.DeserializeObject<PullRequestSonarDetails>(sonarApiResponse);
                    return pullRequestSonarDetails;
                }
            }
        }

        public async Task<bool> ChecksCompleted(GitActions gitActions)
        {
            await PullRequestSonarDetails(ToDoConstants.PULL_REQUEST_SONAR_URL + gitActions?.check_run.pull_requests?.FirstOrDefault().number.ToString());
            return true;
        }
    }
}
