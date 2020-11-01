﻿using System.Threading.Tasks;

namespace api.layer.BusinessLayer
{
    public interface IGitActionsManager
    {
        public Task<bool> OpenRequestedCreated(GitActions gitActions);

        public bool PullRequestedCreated(GitActions gitActions);

        public void PRReviewed(GitActions gitActions);

        public Task<bool> ChecksCompleted(GitActions gitActions);
    }
}