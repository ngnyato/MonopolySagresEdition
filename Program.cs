public class Program {
    public static void Main() {
        GameController gc = new GameController();
        while (true) {
            string? line = Console.ReadLine();
            if(line == "" || line == null) {
                break;
            }
            string[] commands = line.Split(" ");
            if (commands[0] == "RJ") {
                string Name = commands[1];
                if (gc.HasPlayer(Name)) {
                    Console.WriteLine("Jogador Existente.");
                } 
                else {
                    gc.RegisterPlayer(Name);
                    Console.WriteLine("Jogador registado com sucesso.");
                }
            } else if (commands[0] == "LJ") {
                if(!gc.HasPlayers()) {
                    Console.WriteLine("Sem jogadores registados.");
                }
            } else if (commands[0] == "IJ") {

            } else if (commands[0] == "LD") {

            } else if (commands[0] == "CE") {

            } else if (commands[0] == "DJ") {

            } else if (commands[0] == "TT") {

            } else if (commands[0] == "PA") {

            } else if (commands[0] == "CC") {

            } else if (commands[0] == "TC") {

            }
        }
    }
}
