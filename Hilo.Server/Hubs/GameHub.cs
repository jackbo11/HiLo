using Hilo.Server.Helpers;
using Hilo.Server.Models;
using HiLo.Core;
using HiLo.DTOs;
using Microsoft.AspNetCore.SignalR;

namespace HiLo.Server.Hubs
{
    public class GameHub : Hub
    {
        public async Task NewGame(NewGameRequest request)
        {
            var gameKey = GameKey.Generate();
            if (InMemoryDatastore.AddPlayer(new RemotePlayer(request.playerName, gameKey, Context.ConnectionId))) {
                await Groups.AddToGroupAsync(Context.ConnectionId, gameKey);
                await Clients.Caller.SendAsync("GameDetails", new GameDetailsResponse(gameKey, [request.playerName], 1));
            }
        }

        public async Task JoinGame(JoinGameRequest request)
        {

            try
            {
                var existingPlayer = InMemoryDatastore.GetPlayersByGameKey(request.gameKey).Single();
                if (existingPlayer.Name != request.playerName && InMemoryDatastore.AddPlayer(new RemotePlayer(request.playerName, request.gameKey, Context.ConnectionId)))
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, request.gameKey);
                    await Clients.Caller.SendAsync("GameJoined", new JoinGameResponse(true));
                    var response = InMemoryDatastore.GetGameNumberOfRounds(request.gameKey);
                    await Clients.Group(request.gameKey).SendAsync("GameDetails", new GameDetailsResponse(request.gameKey, [existingPlayer.Name, request.playerName], response.success ? response.numberOfRounds : 1));
                }
            }
            catch (Exception ex)
            { 
                await Clients.Caller.SendAsync("GameJoined", new JoinGameResponse(false));
            }


        }

        public async Task UpdateRounds(UpdateRoundsRequest request)
        {
            var gameKey = InMemoryDatastore.GetPlayerByConnectionId(Context.ConnectionId).GameKey;
            InMemoryDatastore.AddOrUpdateGameNumberOfRounds(gameKey, request.numberOfRounds);
            var response = InMemoryDatastore.GetGameNumberOfRounds(gameKey);
            if (response.success)
            {
                await Clients.Group(gameKey).SendAsync("RoundsUpdated", new UpdateRoundsResponse(response.numberOfRounds));

            }
        }

        public async Task StartGame()
        {
            
            var players = InMemoryDatastore.GetPlayersInGameByConnectionId(Context.ConnectionId).ToArray();
            var gameKey = players[0].GameKey;
            var response = InMemoryDatastore.GetGameNumberOfRounds(gameKey);
            var game = new Game(new Player(players[0].Name), new Player(players[1].Name), response.success ? response.numberOfRounds : 1);
            var gs = game.StartRound();
            if (InMemoryDatastore.AddGame(gameKey, game))
            {
                var startingPlayer = players.Single(p => p.Name == gs.CurrentPlayer.Name);
                InMemoryDatastore.AddOrUpdateActiveConnection(gameKey, startingPlayer.ConnectionId);
                await Clients.Group(gameKey).SendAsync("GameStarted", new StartGameResponse(gameKey, game.NumberOfRounds));
                await Clients.Group(gameKey).SendAsync("RoundStarted", new StartRoundResponse(game.RoundCount, [game.Player1, game.Player2]));
                await Clients.Group(gameKey).SendAsync("GameProgressed", new ProgressGameResponse(gs));
            }
        }

        public async Task StartRound()
        {
            var players = InMemoryDatastore.GetPlayersInGameByConnectionId(Context.ConnectionId).ToArray();
            var gameKey = players[0].GameKey;
            var game = InMemoryDatastore.GetGame(gameKey);
            var gs = game.StartRound();
            var startingPlayer = players.Single(p => p.Name == gs.CurrentPlayer.Name);
            InMemoryDatastore.AddOrUpdateActiveConnection(gameKey, startingPlayer.ConnectionId);
            await Clients.Group(gameKey).SendAsync("RoundStarted", new StartRoundResponse(game.RoundCount, [game.Player1, game.Player2]));
            await Clients.Group(gameKey).SendAsync("GameProgressed", new ProgressGameResponse(gs));
        }

        public async Task ProgressGame(ProgressGameRequest request)
        {
            
             string gameKey = InMemoryDatastore.GetPlayerByConnectionId(Context.ConnectionId).GameKey;
            if (InMemoryDatastore.GetActiveConnection(gameKey) == Context.ConnectionId) {
                var game = InMemoryDatastore.GetGame(gameKey);
                GameState? gs = new();
                switch (request.action)
                {
                    case Core.Action.Higher:
                        gs = game.Higher();
                        break;
                    case Core.Action.Lower:
                        gs = game.Lower();
                        break;
                    case Core.Action.Swap:
                        gs = game.Swap();
                        break;
                }
                if (gs.HasPlayerSwapped)
                {
                    InMemoryDatastore.SwapActiveConnection(gameKey, Context.ConnectionId);
                }
                
                
                await Clients.Group(gameKey).SendAsync("GameProgressed", new ProgressGameResponse(gs));
                if (gs.IsGameWon && game.GameOver)
                {
                    await Clients.Group(gameKey).SendAsync("GameWon", new GameWonResponse(game.WinningPlayer));
                }
            }
            
        }
    }
}
