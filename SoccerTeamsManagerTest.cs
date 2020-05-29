using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;
using Codenation.Challenge.Exceptions;

namespace Codenation.Challenge
{
    public class SoccerTeamsManagerTest
    {
        [Fact]
        public void My_Tests()
        {
            var manager = new SoccerTeamsManager();
            // search all Teams
            Assert.Equal(new List<long> { }, manager.GetTeams());
            // search top players
            Assert.Equal(new List<long> { }, manager.GetTopPlayers(2));
            // insert Teams
            manager.AddTeam(1, "Time 1", DateTime.Now, "cor 1", "cor 2");
            manager.AddTeam(2, "Time 2", DateTime.Now, "cor 1", "cor 2");
            manager.AddTeam(3, "Time 3", DateTime.Now, "cor 3", "cor 4");
            Assert.Throws<UniqueIdentifierException>(() =>
                manager.AddTeam(1, "Time 1", DateTime.Now, "cor 1", "cor 2"));
            // insert Players
            manager.AddPlayer(1, 1, "Jogador 1", new DateTime(1983, 03, 01), 10, 10);
            manager.AddPlayer(2, 1, "Jogador 2", new DateTime(1985, 02, 01), 69, 100);
            manager.AddPlayer(3, 1, "Jogador 3", new DateTime(1987, 12, 01), 65, 100);
            manager.AddPlayer(9, 1, "Jogador 9", new DateTime(1989, 07, 01), 31, 10);
            manager.AddPlayer(6, 1, "Jogador 6", new DateTime(1990, 05, 01), 72, 100);
            manager.AddPlayer(4, 1, "Jogador 4", new DateTime(1992, 09, 01), 86, 100);
            manager.AddPlayer(8, 1, "Jogador 8", new DateTime(1984, 04, 01), 10, 10);
            manager.AddPlayer(5, 1, "Jogador 5", new DateTime(1983, 03, 01), 90, 100);
            manager.AddPlayer(7, 1, "Jogador 7", new DateTime(1999, 03, 01), 100, 100);
            manager.AddPlayer(11, 2, "Jogador 1", new DateTime(1995, 03, 01), 10, 10);
            manager.AddPlayer(12, 2, "Jogador 2", new DateTime(1997, 03, 01), 19, 100);
            manager.AddPlayer(13, 2, "Jogador 3", new DateTime(1983, 03, 01), 52, 100);
            Assert.Throws<UniqueIdentifierException>(() =>
                manager.AddPlayer(1, 1, "Jogador 1", DateTime.Today, 0, 0));
            Assert.Throws<TeamNotFoundException>(() =>
                manager.AddPlayer(99, 99, "Jogador 1", DateTime.Today, 0, 0));
            // search player
            Assert.Equal("Jogador 1", manager.GetPlayerName(1));
            Assert.Throws<PlayerNotFoundException>(() =>
                manager.GetPlayerName(99));
            // search team
            Assert.Equal("Time 1", manager.GetTeamName(1));
            Assert.Throws<TeamNotFoundException>(() =>
                manager.GetTeamName(99));
            // search players by team
            Assert.Equal(new List<long> { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, manager.GetTeamPlayers(1));
            Assert.Throws<TeamNotFoundException>(() =>
                manager.GetTeamPlayers(99));
            // search best player on team
            Assert.Equal(7, manager.GetBestTeamPlayer(1));
            Assert.Throws<TeamNotFoundException>(() =>
                manager.GetBestTeamPlayer(99));
            // search oldest player on team
            Assert.Equal(1, manager.GetOlderTeamPlayer(1));
            Assert.Throws<TeamNotFoundException>(() =>
                manager.GetOlderTeamPlayer(99));
            // search all Teams
            Assert.Equal(new List<long> { 1, 2, 3 }, manager.GetTeams());
            // search higher salary by team
            Assert.Equal(2, manager.GetHigherSalaryPlayer(1));
            Assert.Throws<TeamNotFoundException>(() =>
                manager.GetHigherSalaryPlayer(99));
            // search salary from player
            Assert.Equal(10, manager.GetPlayerSalary(1));
            Assert.Throws<PlayerNotFoundException>(() =>
                manager.GetPlayerSalary(99));
            // search top players
            Assert.Equal(new List<long> { 7, 5 }, manager.GetTopPlayers(2));
            // search shirt color from visitor
            Assert.Equal("cor 2", manager.GetVisitorShirtColor(1, 2));
            Assert.Equal("cor 3", manager.GetVisitorShirtColor(1, 3));
            Assert.Throws<TeamNotFoundException>(() =>
                manager.GetVisitorShirtColor(99, 1));
            Assert.Throws<TeamNotFoundException>(() =>
                manager.GetVisitorShirtColor(1, 99));
            Assert.Throws<TeamNotFoundException>(() =>
                manager.GetVisitorShirtColor(99, 99));
        }

        [Fact]
        public void Should_Be_Unique_Ids_For_Teams()
        {
            var manager = new SoccerTeamsManager();
            manager.AddTeam(1, "Time 1", DateTime.Now, "cor 1", "cor 2");
            Assert.Throws<UniqueIdentifierException>(() =>
                manager.AddTeam(1, "Time 1", DateTime.Now, "cor 1", "cor 2"));
        }
 
        [Fact]
        public void Should_Be_Valid_Player_When_Set_Captain()
        {
            var manager = new SoccerTeamsManager();
            manager.AddTeam(1, "Time 1", DateTime.Now, "cor 1", "cor 2");
            manager.AddTeam(2, "Time 2", DateTime.Now, "cor 1", "cor 2");
            manager.AddPlayer(1, 1, "Jogador 1", DateTime.Today, 0, 0);
            manager.AddPlayer(2, 1, "Jogador 2", DateTime.Today, 0, 0);
            manager.SetCaptain(1);
            Assert.Equal(1, manager.GetTeamCaptain(1));
            manager.SetCaptain(2);
            Assert.Equal(2, manager.GetTeamCaptain(1));
            Assert.Throws<PlayerNotFoundException>(() =>
              manager.SetCaptain(99));
            Assert.Throws<CaptainNotFoundException>(() =>
              manager.GetTeamCaptain(2));
        }

        [Fact]
        public void Should_Ensure_Sort_Order_When_Get_Team_Players()
        {
            var manager = new SoccerTeamsManager();
            manager.AddTeam(1, "Time 1", DateTime.Now, "cor 1", "cor 2");

            var playersIds = new List<long>() {15, 2, 33, 4, 13};
            for(int i = 0; i < playersIds.Count(); i++)
                manager.AddPlayer(playersIds[i], 1, $"Jogador {i}", DateTime.Today, 0, 0);

            playersIds.Sort();
            Assert.Equal(playersIds, manager.GetTeamPlayers(1));
        }

        [Theory]
        [InlineData("10,20,300,40,50", 2)]
        [InlineData("50,240,3,1,50", 1)]
        [InlineData("10,22,24,3,24", 2)]
        public void Should_Choose_Best_Team_Player(string skills, int bestPlayerId)
        {
            var manager = new SoccerTeamsManager();
            manager.AddTeam(1, "Time 1", DateTime.Now, "cor 1", "cor 2");

            var skillsLevelList = skills.Split(',').Select(x => int.Parse(x)).ToList();
            for(int i = 0; i < skillsLevelList.Count(); i++)
                manager.AddPlayer(i, 1, $"Jogador {i}", DateTime.Today, skillsLevelList[i], 0);

            Assert.Equal(bestPlayerId, manager.GetBestTeamPlayer(1));
        }

        [Theory]
        [InlineData("Azul;Vermelho", "Azul;Amarelo", "Amarelo")]
        [InlineData("Azul;Vermelho", "Amarelo;Laranja", "Amarelo")]
        [InlineData("Azul;Vermelho", "Azul;Vermelho", "Vermelho")]
        public void Should_Choose_Right_Color_When_Get_Visitor_Shirt_Color(string teamColors, string visitorColors, string visitorMatchColor)
        {
            long teamId = 1;
            long visitorTeamId = 2;
            var teamColorList = teamColors.Split(";");
            var visitorColorList = visitorColors.Split(";");

            var manager = new SoccerTeamsManager();
            manager.AddTeam(teamId, $"Time {teamId}", DateTime.Now, teamColorList[0], teamColorList[1]);
            manager.AddTeam(visitorTeamId, $"Time {visitorTeamId}", DateTime.Now, visitorColorList[0], visitorColorList[1]);

            Assert.Equal(visitorMatchColor, manager.GetVisitorShirtColor(teamId, visitorTeamId));
        }
    }
}
