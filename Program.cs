public class Program
{
    public static void Main()
    {
        string[] commandLine; 
        GameController gc = new GameController();
        while (true)
        {
            string line = Console.ReadLine();
            if (line.Length == 0 || line == null) // proteçao contra input nulo
            {
                Console.Clear();  // TODO O jogo deve acabar com uma linha em branco, isto é apenas para testes!!!
                continue;
            }
            
            commandLine = line.Split(" ", StringSplitOptions.RemoveEmptyEntries); // divide o input entre o comando e o jogador 
            
           
            string cmd = commandLine[0].ToUpper(); // formataçao para permitir melhor user interaction
            
            
            
            switch (cmd)
            {
                   
                case "RJ":
                    if (commandLine.Length < 2)
                    {
                        Console.WriteLine("Instrução inválida.");
                        break;
                    }
                    string playerName = commandLine[1];
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
                        else
                        {
                                gc.ListPlayers();
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
    

