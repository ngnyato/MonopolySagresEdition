using System.ComponentModel.Design;
using System.Dynamic;
using System.Linq.Expressions;
using System.Reflection.Metadata;
#nullable disable
namespace MonopolyC;
public class Program
{
    
     
    
    public static void Main()
    {
        
        string[] commandLine; 
        BoardManager.InitializeBoard();
        GameController gc = new GameController();
       
        while (true)
        {
            string line = Console.ReadLine();
            //if (line.Length == 0 || line == null) // proteçao contra input nulo
            //{
              //  Console.Clear();  //
             //   continue;
            //}
            
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

                             if (!gc.IsGameInProgress())
                               {
                                    Console.WriteLine("Não existe um jogo em curso.");
                                    continue;
                               }

                             bool isInGame = gc.playersInGame.Any(p => p.Name == commandLine[1]); // verifica se o jogador faz parte do jogo em curso
                             if (!isInGame)
                               {
                                    Console.WriteLine("Jogador não participa no jogo em curso.");
                                    continue;
                               }

                             if (gc.turnIndex >= gc.playersInGame.Count)
                               {
                                   gc.turnIndex = 0;
                               }

                             if ( commandLine[1] != gc.playersInGame[gc.turnIndex].Name )
                                {
                                    Console.WriteLine("Não é a vez do jogador.");
                                    continue;
                                } 
                            if (commandLine.Length == 4 && gc.CurrentPlayer.hasRolledDices == false)
                              {

                                int moveX = int.Parse(commandLine[2]);
                                int moveY = int.Parse(commandLine[3]);
                                if (gc.CurrentPlayer.isInPrison)
                                {
                                    gc.HandlePrisonTurn(gc.CurrentPlayer, moveX, moveY);
                                    continue;
                                }
                                else 
                                gc.RollDices(moveX,moveY);
                              }
                              else if ( gc.CurrentPlayer.hasRolledDices == false)
                              {
                                if (gc.CurrentPlayer.isInPrison)
                                {
                                    gc.HandlePrisonTurn(gc.CurrentPlayer, null, null);
                                    continue;
                                }
                                else
                                gc.RollDices(null,null);
                              }
            
                            
                             break;
                            
                        
                        case "CE":
                        if (commandLine.Length != 2)
                                {
                                    Console.WriteLine("Instrução inválida.");
                                   continue;
                                }

                             gc.BuySpace();
                        break;

                        case "TT":
                         
                        if (commandLine.Length != 2)
                                {
                                   
                                    Console.WriteLine("Instrução inválida.");
                                   continue;
                                }

                          
                        gc.FinishTurn(commandLine[1]);
                        
                        break;

                         case "PA":
                         
                        if (commandLine.Length != 2)
                                {
                                   
                                    Console.WriteLine("Instrução inválida.");
                                   continue;
                                }


                            gc.PayDueRent(commandLine[1]);

                        
                        break;

                            case "CC":

                        if (commandLine.Length != 3)
                                {
                                   
                                    Console.WriteLine("Instrução inválida.");
                                   continue;
                                }

                            gc.BuildHouse(commandLine[1], commandLine[2]);
                            break;
                           
                            case "DJ":
                               gc.PrintGameDetails();
                              break;

                            case "TC":
                               if (commandLine.Length != 2)
                               {
                                   Console.WriteLine("Instrução inválida.");
                                   continue;
                               }  
                               gc.cardManager.DrawCard(commandLine[1], gc);
                               break;

                            default: 
                               Console.WriteLine("Instrução inválida.");
                            break;
                             




                            




                

            }
            
            // } else if (commands[0] == "IbJ") {
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
    

