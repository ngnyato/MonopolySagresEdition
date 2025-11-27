public class Program
{
    public static void Main()
    {
        GameController gc = new GameController();
        while (true)
        {
            string? line = Console.ReadLine();
            if (line.Length == 0 || line == null)
            {
                Console.Clear();
                continue;
            }
            
            string[] commandLine = line.Split(" ");
            if (commandLine.Length < 2)
            {
                Console.Clear();
                continue;
            }
                 
            string cmd = commandLine[0].ToUpper();
            string playerName = commandLine[1];
            
            switch (cmd)
            {
                   
                case "RJ":
                    
                    if (gc.DoPlayerExists(playerName))
                    {
                        Console.WriteLine("Jogador Existente.");
                    }
                    else
                    {
                        gc.RegisterPlayer(playerName);
                        Console.WriteLine("Jogador registado com sucesso.");
                    }
                    break;

                    case "LJ": 
                        if (!gc.HasPlayers())
                        {
                            Console.WriteLine("Sem jogadores registados.");
                        }

                        break;

            }
            
            // } else if (commands[0] == "IJ") {
            //
            // } else if (commands[0] == "LD") {
            //
            // } else if (commands[0] == "CE") {
            //
            // } else if (commands[0] == "DJ") {
            //
            // } else if (commands[0] == "TT") {
            //
            // } else if (commands[0] == "PA") {
            //
            // } else if (commands[0] == "CC") {
            //
            // } else if (commands[0] == "TC") {

        }
    }
}
    

