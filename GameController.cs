public class GameController {
    private Dictionary<string,Player> players = new Dictionary<string,Player>();
    public bool DoPlayerExists(string playerName) {
        return players.Keys.Contains(playerName);
    }

    public void RegisterPlayer(string playerName){
        players.Add(playerName, new Player(playerName));
    }

    public bool HasPlayers() {
        return false; // TODO: missing implementation.
    }
}