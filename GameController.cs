    public class GameController
    {
        private Dictionary<string, Player> players = new Dictionary<string, Player>();

        public bool DoPlayerExists(string playerName)
        {
            return players.Keys.Contains(playerName);
        }

        public void RegisterPlayer(string playerName)
        {
            players.Add(playerName, new Player(playerName));
        }

        public bool HasPlayers()
        {
            return players.Count > 0;
        }

        public void ListPlayers()
        {
                
                    foreach (var player in players.Values)
                    {
                        Console.WriteLine(player.Name);
                    }
                
        }
    }