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
        List<Captain> captains = new List<Captain>();

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
            if (!TeamExists(teamId))
            {
                throw new TeamNotFoundException();
            }
            players.Add(new Player(id, teamId, name, birthDate, skillLevel, salary));
        }

        public void SetCaptain(long playerId)
        {
            if (!PlayerExists(playerId))
            {
                throw new PlayerNotFoundException();
            }
            long selectedTeamId = players.Where(x => x.Id == playerId).Select(x => x.TeamId).FirstOrDefault();
            Captain existCaptain = captains.FirstOrDefault(x => x.TeamId == selectedTeamId); 
            if (existCaptain != null)
            {
                captains.Remove(existCaptain); 
            }
            captains.Add(new Captain(selectedTeamId, playerId));
        }

        public long GetTeamCaptain(long teamId)
        {
            if (!TeamExists(teamId))
            {
                throw new TeamNotFoundException();
            }
            Captain existCaptain = captains.FirstOrDefault(x => x.TeamId == teamId);
            if (existCaptain == null)
            {
                throw new CaptainNotFoundException();
            }
            return existCaptain.PlayerId;
        }

        public string GetPlayerName(long playerId)
        {
            if (!PlayerExists(playerId))
            {
                throw new PlayerNotFoundException();
            }
            return players.Where(x => x.Id == playerId).Select(x => x.Name).FirstOrDefault().ToString();
        }

        public string GetTeamName(long teamId)
        {
            if (!TeamExists(teamId))
            {
                throw new TeamNotFoundException();
            }
            return teams.Where(x => x.Id == teamId).Select(x => x.Name).FirstOrDefault().ToString();
        }

        public List<long> GetTeamPlayers(long teamId)
        {
            if (!TeamExists(teamId))
            {
                throw new TeamNotFoundException();
            }
            return players.OrderBy(x => x.Id).Where(x => x.TeamId == teamId).Select(x => x.Id).ToList();
        }

        public long GetBestTeamPlayer(long teamId)
        {
            if (!TeamExists(teamId))
            {
                throw new TeamNotFoundException();
            }
            return players.OrderByDescending(x => x.SkillLevel).ThenBy(x => x.Id).Where(x => x.TeamId == teamId).Select(x => x.Id).FirstOrDefault();
        }

        public long GetOlderTeamPlayer(long teamId)
        {
            if (!TeamExists(teamId))
            {
                throw new TeamNotFoundException();
            }
            return players.OrderBy(x => x.BirthDate).ThenBy(x => x.Id).Where(x => x.TeamId == teamId).Select(x => x.Id).FirstOrDefault();
        }

        public List<long> GetTeams()
        {
            return teams.OrderBy(x => x.Id).Select(x => x.Id).ToList();
        }

        public long GetHigherSalaryPlayer(long teamId)
        {
            if (!TeamExists(teamId))
            {
                throw new TeamNotFoundException();
            }
            return players.OrderByDescending(x => x.Salary).ThenBy(x => x.Id).Where(x => x.TeamId == teamId).Select(x => x.Id).FirstOrDefault();
        }

        public decimal GetPlayerSalary(long playerId)
        {
            if (!PlayerExists(playerId))
            {
                throw new PlayerNotFoundException();
            }
            return players.Where(x => x.Id == playerId).Select(x => x.Salary).FirstOrDefault();
        }

        public List<long> GetTopPlayers(int top)
        {
            return players.OrderByDescending(x => x.SkillLevel).ThenBy(x => x.Id).Select(x => x.Id).Take(top).ToList();
        }

        public string GetVisitorShirtColor(long teamId, long visitorTeamId)
        {
            if (!TeamExists(teamId) || !TeamExists(visitorTeamId))
            {
                throw new TeamNotFoundException();
            }
            string homeShirt = teams.Where(x => x.Id == teamId).Select(x => x.MainShirtColor).FirstOrDefault();
            string visitorShirt = teams.Where(x => x.Id == visitorTeamId).Select(x => x.MainShirtColor).FirstOrDefault();
            if(homeShirt == visitorShirt)
            {
                visitorShirt = teams.Where(x => x.Id == visitorTeamId).Select(x => x.SecondaryShirtColor).FirstOrDefault();
            }
            return visitorShirt;
        }
    }
}
