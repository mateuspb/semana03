using System;

namespace Codenation.Challenge
{
    class Captain
    {
        public Captain(long teamId, long playerId)
        {
            this.TeamId = teamId;
            this.PlayerId = playerId;
        }
        public long TeamId { get; set; }
        public long PlayerId { get; set; }
    }
}