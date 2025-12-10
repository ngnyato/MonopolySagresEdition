using System.ComponentModel.Design;

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

                    case "IJ": //TODO : Please clean this mess of a case
                            if (commandLine.Length < 3 || commandLine.Length > 6) // minimo 2 jogadores maximo 5 jogadores
                         {
                              Console.WriteLine("Instrução inválida.");
                              continue;
                         }
                             string[] plrsInGame = new string[commandLine.Length - 1];

                             for (int i = 1; i < commandLine.Length; i++)    // separamos os nomes dos jogadores do comando
                            {plrsInGame[i - 1 ]= commandLine[i];}

                            bool allExist = true;
                            for (int j = 0; j < plrsInGame.Length; j++)   // verificamos se os jogadores existem
                            {
                                if (!gc.DoPlayerExists(plrsInGame[j]))
                                {
                                   allExist = false;
                                }
                            }
                            if (!allExist)
                            {
                                Console.WriteLine("Jogador inexistente.");
                                continue;
                            }
                         if (gc.IsGameInProgress())   // verifica se ja existe um jogo em curso
                         {
                             Console.WriteLine("Existe um jogo em curso.");
                             continue;
                         }

                         gc.StartGame(plrsInGame); // inicia o jogo
                         Console.WriteLine("Jogo iniciado com sucesso.");
                    
                         break;



                         case "LD":
                         
                                if (commandLine.Length < 2)
                                {
                                    Console.WriteLine("Instrução inválida.");
                                   continue;
                                }


                           bool isInGame = gc.playersInGame.Any(p => p.Name == commandLine[1]);

                             if (!gc.IsGameInProgress())
                               {
                                    Console.WriteLine("Não existe um jogo em curso.");
                                    continue;
                               }
                             if (!isInGame)
                               {
                                    Console.WriteLine("Jogador não participa no jogo em curso.");
                                    continue;
                               }
                             else if ( commandLine[1] != gc.playersInGame[gc.turnIndex].Name )
                                {
                                    Console.WriteLine("Não é a vez do jogador.");
                                    continue;
                                } 
                              else if  (commandLine[1] == gc.playersInGame[gc.turnIndex].Name)
                                {
                                    gc.RollDices();
                                }
                                // TODO : implementar o resto do  tabuleiro e dos comandos!!
                            
                             
                                  
                                
                             
                             
                             break;
                            


                        default:
                            Console.WriteLine("Instrução inválida.");
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
    

