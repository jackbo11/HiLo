using Hilo.Server.Models;

namespace Hilo.Server.Helpers
{
    public static class GameKey
    {
        public static string Generate() => Guid.NewGuid().ToString().Substring(0, 6);
    }
}
