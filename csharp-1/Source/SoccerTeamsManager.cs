using System;
using System.Collections.Generic;
using Codenation.Challenge.Exceptions;
using System.Linq;

namespace Codenation.Challenge
{
    public class SoccerTeamsManager : IManageSoccerTeams
    {
        List<Team> teams = new List<Team>();
        List<Player> players = new List<Player>();
        public SoccerTeamsManager()
        {
        }
        public bool TeamExists(long teamId)
        {
            return teams.Any(x => x.Id == teamId);
        }

        public bool PlayerExists(long playerId)
        {
            return players.Any(x => x.Id == playerId);
        }

        public void AddTeam(long id, string name, DateTime createDate, string mainShirtColor, string secondaryShirtColor)
        {
            if (TeamExists(id))
            {
                throw new UniqueIdentifierException();
            }
            teams.Add(new Team(id, name, createDate, mainShirtColor, secondaryShirtColor));
        }

        public void AddPlayer(long id, long teamId, string name, DateTime birthDate, int skillLevel, decimal salary)
        {
            if (PlayerExists(id))
            {
                throw new UniqueIdentifierException();
            }
            players.Add(new Player(id, teamId, name, birthDate, skillLevel, salary));
        }

        public void SetCaptain(long playerId)
        {
            throw new NotImplementedException();
        }

        public long GetTeamCaptain(long teamId)
        {
            throw new NotImplementedException();
        }

        public string GetPlayerName(long playerId)
        {
            throw new NotImplementedException();
        }

        public string GetTeamName(long teamId)
        {
            throw new NotImplementedException();
        }

        public List<long> GetTeamPlayers(long teamId)
        {
            throw new NotImplementedException();
        }

        public long GetBestTeamPlayer(long teamId)
        {
            throw new NotImplementedException();
        }

        public long GetOlderTeamPlayer(long teamId)
        {
            throw new NotImplementedException();
        }

        public List<long> GetTeams()
        {
            throw new NotImplementedException();
        }

        public long GetHigherSalaryPlayer(long teamId)
        {
            throw new NotImplementedException();
        }

        public decimal GetPlayerSalary(long playerId)
        {
            throw new NotImplementedException();
        }

        public List<long> GetTopPlayers(int top)
        {
            throw new NotImplementedException();
        }

        public string GetVisitorShirtColor(long teamId, long visitorTeamId)
        {
            throw new NotImplementedException();
        }

    }
}
