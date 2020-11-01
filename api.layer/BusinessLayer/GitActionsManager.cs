using api.layer.DataAccessLayer;
using api.layer.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
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

        private string[] stringArray = { "new_vulnerabilities", "new_reliability_rating", "new_security_rating", "new_bugs", "new_code_smells", "new_security_hotspots", "new_maintainability_rating" };

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

        public async Task<bool> PRReviewed(GitActions gitActions)
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
            catch(Exception ex)
            {
                throw ex;
            }

            return false;
        }

        public async Task<PullRequestSonarDetails> PullRequestSonarDetails(string URL, int? PRId)
        {
            if (PRId != null)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var sonarResponse = await httpClient.GetAsync(URL))
                    {
                        string sonarApiResponse = await sonarResponse.Content.ReadAsStringAsync();
                        PullRequestSonarDetails pullRequestSonarDetails = JsonConvert.DeserializeObject<PullRequestSonarDetails>(sonarApiResponse);

                        Dictionary<string, dynamic> sonarMetric = new Dictionary<string, dynamic>();
                        if (pullRequestSonarDetails?.Component?.Measures != null)
                        {
                            try
                            {
                                foreach (Measure item in pullRequestSonarDetails.Component.Measures)
                                {
                                    if(item.Periods != null && stringArray.Contains(item.Metric))
                                    {
                                        sonarMetric.Add(item.Metric, item.Periods.FirstOrDefault().Value);
                                    }
                                }
                            }
                            catch(Exception ex)
                            {
                                throw ex;
                            }
                            await _gitActionsDAO.SaveSonarDetails(PRId, sonarMetric);
                        }
                        return pullRequestSonarDetails;
                    }
                }
            }

            return new PullRequestSonarDetails();
        }

        public async Task<bool> ChecksCompleted(GitActions gitActions)
        {
            if(gitActions?.check_run?.pull_requests != null && gitActions?.check_run?.pull_requests.Length > 0)
            {
                await PullRequestSonarDetails(ToDoConstants.PULL_REQUEST_SONAR_URL + gitActions?.check_run?.pull_requests?.FirstOrDefault().number.ToString(), gitActions?.check_run?.pull_requests?.FirstOrDefault().number);
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
