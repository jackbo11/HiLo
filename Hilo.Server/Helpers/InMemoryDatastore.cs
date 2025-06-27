using HiLo.Core;
using Hilo.Server.Models;

namespace Hilo.Server.Helpers
{
    public static class InMemoryDatastore
    {
        private static readonly Dictionary<string, RemotePlayer> players = new(); // connectionId - players
        private static readonly Dictionary<string, Game> games = new(); // gameKey - games
        private static readonly Dictionary<string, string> activeConnection = new(); // gameKey - connectionid
        private static readonly Dictionary<string, int> gameNumberOfRounds = new(); //gameKey - noOfRounds

        public static bool AddPlayer(RemotePlayer player)
        {
            return players.TryAdd(player.ConnectionId, player);
        }
        public static void RemovePlayer(string connectionId)
        {
            players.Remove(connectionId);
        }
        public static RemotePlayer GetPlayerByConnectionId(string connectionId)
        {
            return players[connectionId];
        }

        public static bool AddGame(string gameKey, Game game)
        {
            return games.TryAdd(gameKey, game);
        }
        public static void RemoveGame(string gameKey)
        {
            games.Remove(gameKey);
            gameNumberOfRounds.Remove(gameKey);
        }
        public static Game GetGame(string gameKey) 
        {
            return games[gameKey];
        }

        private static IEnumerable<KeyValuePair<string, RemotePlayer>> GetPlayersKvpByGameKey(string gameKey)
        {
            return players.Where(p => p.Value.GameKey == gameKey);
        }
        public static IEnumerable<RemotePlayer> GetPlayersByGameKey(string gameKey)
        {
            return GetPlayersKvpByGameKey(gameKey).Select(d => d.Value);
        }
        public static RemotePlayer GetOppositePlayer(string gameKey, string connectionId)
        {
            return GetPlayersKvpByGameKey(gameKey).Single(p => p.Key != connectionId).Value;
        }
        public static IEnumerable<RemotePlayer> GetPlayersInGameByConnectionId(string connectionId)
        {
            return GetPlayersByGameKey(GetPlayerByConnectionId(connectionId).GameKey);
        }

        public static void AddOrUpdateActiveConnection(string gameKey, string connectionId)
        {
            activeConnection[gameKey] = connectionId;
        }
        public static void SwapActiveConnection(string gameKey, string connectionId)
        {
            AddOrUpdateActiveConnection(gameKey, GetOppositePlayer(gameKey, connectionId).ConnectionId);
        }
        public static void RemoveActiveConnection(string gameKey)
        {
            activeConnection.Remove(gameKey);
        }
        public static string GetActiveConnection(string gameKey)
        {
            return activeConnection[gameKey];
        }

        public static void AddOrUpdateGameNumberOfRounds(string gameKey, int numberOfRounds)
        {
            gameNumberOfRounds[gameKey] = numberOfRounds;
        }
        public static void RemoveGameNumberOfRounds(string gameKey)
        {
            gameNumberOfRounds.Remove(gameKey);
        }
        public static (bool success, int numberOfRounds) GetGameNumberOfRounds(string gameKey)
        {
            bool success = gameNumberOfRounds.TryGetValue(gameKey, out int numberOfRounds);
            return (success, numberOfRounds);
        }
    }
}
