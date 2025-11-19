public class GameController {
    private Dictionary<string,Player> players = new Dictionary<string,Player>();
    public bool HasPlayer(string playerName) {
        /* Para listas e arrays: */
        // foreach(Player player in players) {
        //     if(player.Name == playerName) {
        //         return true;
        //     }
        // }
        // return false;
        return players.Keys.Contains(playerName);
    }

    public void RegisterPlayer(string playerName){
        players.Add(playerName, new Player(playerName));
    }
}