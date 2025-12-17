using System.Collections;
using System.Runtime.InteropServices.Marshalling;
using System.Runtime.Intrinsics.X86;
using System.Runtime.Serialization.Formatters;
#nullable disable
namespace MonopolyC;
public class GameController
    {
        private Dictionary<string, Player> players = new Dictionary<string, Player>();
        public bool gameInProgress = false;
        public string landedHouse = "";
        public int turnIndex = 0;
        public int freeParkValue = 0;  
        public List<Player> playersInGame = new List<Player>();
        public Player CurrentPlayer => playersInGame[turnIndex];
        public CardManager cardManager = new CardManager();
    
        public GameController()
        {
            cardManager = new CardManager();
        }

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
            return players.Count > 0; // return true se a condiçao implicita for verdadeira !!
        }

        public void ListPlayers() // TODO : melhorar a apresentaçao daqui para baixo
        {
                
                    foreach (var player in players.Values)
                    {
                       Console.WriteLine($"{player.Name} {player.gamesPlayed} {player.wins} {player.ties} {player.losses}"); // é um place older para nao usar name + " " + gamesPlayed + " " + wins + " " + ties + " " + losses
                    }
                
        }

            public void StartGame(string[] playersGameList)
            {
                playersInGame.Clear();
                turnIndex = 0;
                for (int i = 0; i < playersGameList.Length; i++)
                {
                    this.playersInGame.Add(players[playersGameList[i]]);
                }
           
                gameInProgress = true;

            }
        
        public bool IsGameInProgress()
        {
            return gameInProgress;
        }
        
        public void RollDices(int? testMoveX, int? testMoveY)   
    {
        if (turnIndex >= playersInGame.Count)
        turnIndex = 0;

        Player p = playersInGame[turnIndex];
        p.hasRolledDices = true;
        int moveX, moveY;

        if (testMoveX.HasValue && testMoveY.HasValue)
        {
            moveX = testMoveX.Value;
            moveY = testMoveY.Value;
        }
        else
        {
             
         Random rnd = new Random();
         int[] moves = { -3, -2, -1, 1, 2, 3 };

         moveX = moves[rnd.Next(moves.Length)];
         moveY = moves[rnd.Next(moves.Length)];
   // TODO resolver a situacao de sair o 0
        }

        int posX = p.posX + moveX;
        int posY = p.posY + moveY;

         int width  = BoardManager.Houses.GetLength(1); // linhas X e 
         int height = BoardManager.Houses.GetLength(0); // colunas Y

         if (posX < 0)
           posX += width;
         else if (posX >= width)
           posX -= width;
                                             // TODO : documentar isto  
        if (posY < 0)
           posY += height;
         else if (posY >= height)
           posY -= height;
         
        playersInGame[turnIndex].posX = posX;
        playersInGame[turnIndex].posY = posY;   

       House landedHouse = BoardManager.Houses[posY, posX];
       p.CurrentHouse = landedHouse;
        
        if ( landedHouse.houseType == "Property" || landedHouse.houseType == "Train" )
        {

            if (landedHouse.houseOwner == null)
            {
                Console.WriteLine($"Saiu {moveX}/{moveY} – espaço {landedHouse.houseName}. Espaço sem dono.");
            }
            else if (landedHouse.houseOwner == p)
            {
                Console.WriteLine($"Saiu {moveX}/{moveY} - espaço {landedHouse}. Espaço já comprado.");
            }
            else
            {
                Console.WriteLine($"Saiu {moveX}/{moveY} - espaço {landedHouse}. Espaço já comprado por outro jogador. Necessário pagar renda.");
                p.hasDemandingAction = true;
                p.hasToPayRent = true;
            }
        }

//         Free Park  
//         Police  
//         Prison  
//          bacl to start 
//          Chance  
//         Community 
        if (landedHouse.houseType == "Special")
        {
           switch(landedHouse.houseName)
            {
                case "FreePark":
                Console.WriteLine($"Saiu {moveX}/{moveY} - espaço {landedHouse.houseName}. Espaço FreePark. Jogador recebe {freeParkValue}."); // TODO missing implementation 
                p.Receive(freeParkValue);
                freeParkValue = 0;
                    break;

                case "Police":
                Console.WriteLine($"Saiu {moveX}/{moveY} - espaço {landedHouse.houseName}. Espaço Police. Jogador preso.");
                SendPlayerToPrison(p);
                    break;  

                case "Prison":
                Console.WriteLine($"Saiu {moveX}/{moveY} - espaço {landedHouse.houseName}. Espaço Prison. Jogador só de passagem."); 
                    break;

                case "BackToStart":
                Console.WriteLine($"Saiu {moveX}/{moveY} - espaço {landedHouse.houseName}. Espaço BackToStart. Peça colocada no espaço Start."); cardManager.MovePlayerTo(p, "Start");
                p.Receive(200);
                    break;

                case "Chance":
                Console.WriteLine($"Saiu {moveX}/{moveY} - espaço {landedHouse.houseName}. Espaço especial. Tirar carta.");  
                p.hasDemandingAction = true;
                    break;

                case "Community":
                Console.WriteLine($"Saiu {moveX}/{moveY} - espaço {landedHouse.houseName}. Espaço especial. Tirar carta.");
                p.hasDemandingAction = true;
                    break;

                case "LuxTax":
                Console.WriteLine($"Saiu {moveX}/{moveY} - espaço {landedHouse.houseName}. Espaço LuxTax. Jogador paga 75.");
                p.Pay(75);
                freeParkValue += 75;
                    break;


                default:
                    break;
            }
       
        }

        
        
    }

    public void BuySpace ()
    {
        Player p = CurrentPlayer;
        House h = p.CurrentHouse;




        if (h.houseType != "Property" && h.houseType != "Train")
        {
            Console.WriteLine("Este espaço não está para venda.");
            return;
        }

        if (h.houseOwner != null)
         { 
        Console.WriteLine("O espaço já se encontra comprado.");
        return;
         }

         if (p.money < h.housePrice)
         {
        Console.WriteLine("O jogador não tem dinheiro suficiente para adquirir o espaço.");
        return;
            }
    
           p.money = p.money - h.housePrice;
           h.houseOwner = p;
           p.OwnedHouses.Add(h);
    }
    

        public void FinishTurn(string pName)
        {
            Player p = CurrentPlayer;


            if (gameInProgress == false)
            {
                Console.WriteLine("Não existe jogo em curso.");
                return;
            }

            if (pName != p.Name)
            {
                Console.WriteLine("Não é o turno do jogador indicado.");
                return;
            }

            if (p.hasDemandingAction == true)
            {
                Console.WriteLine("O jogador ainda tem ações a fazer.");
                return;
            }



                p.hasRolledDices = false;
                p.hasDemandingAction = false;
                p.hasToPayRent = false;
            
                if (p.isBankrupt)
                {
                    EndTurn();
                }

                else 
                {
                turnIndex++;
                if (turnIndex >= playersInGame.Count) 
                turnIndex = 0;
                Console.WriteLine($"Turno terminado. Novo turno do jogador {playersInGame[turnIndex].Name}.");
               

                
            }
        }



    public void PayDueRent(string pName)
    {
        Player p = CurrentPlayer;
        House h = p.CurrentHouse;

        double antes = h.housePrice * 0.25 + h.housePrice * 0.75 * h.houseNumber;
        int renda = (int)Math.Round(antes);
       


       if (!gameInProgress)
        {
          Console.WriteLine("Não existe um jogo em curso.");
        return;
       }

      if (!playersInGame.Contains(CurrentPlayer))
       {
          Console.WriteLine("Jogador não participa no jogo em curso.");
         return;
       }

      if (pName != CurrentPlayer.Name)
       {
          Console.WriteLine("Não é a vez do jogador.");
          return;
        }

       if (!p.hasToPayRent)
       {
          Console.WriteLine("Não é necessário pagar aluguel.");
          return;
       }

        else 
        {
            p.Pay(renda);
            h.houseOwner.Receive(renda);
            p.hasToPayRent = false;
            p.hasDemandingAction = false;
        }
    }

    public bool OwnsFullColorSet(Player p, string color)
{
    foreach (House h in BoardManager.Houses)
    {
        if (h != null &&
            h.houseType == "Property" &&
            h.color == color &&
            h.houseOwner != p)
        {
            return false;
        }
    }
    return true;
}

    public void EndTurn()
{
    Player p = CurrentPlayer;

    if (p.money < 0)
    {
        p.isBankrupt = true;
    }

    if (p.isBankrupt)
    {
        Console.WriteLine($"{p.Name} faliu e foi eliminado.");
        playersInGame.RemoveAt(turnIndex);

        // se só sobrar um jogador
        if (playersInGame.Count == 1)
        {
            Console.WriteLine($"Jogo terminado. Vencedor: {playersInGame[0].Name}");
            gameInProgress = false;
            playersInGame[0].wins++;
            foreach (var player in playersInGame)
            {
                if (player != playersInGame[0])
                {
                    player.losses++;
                }
            }
            return;
        }

        // NÃO avançar turno aqui
        if (turnIndex >= playersInGame.Count)
            turnIndex = 0;

        return;
    }

   
   
    if (turnIndex >= playersInGame.Count)
        turnIndex = 0;
}

    public void BuildHouse(string pName, string desiredHouse)
    {
        Player p = CurrentPlayer;
        House h = BoardManager.FindHouseByName(desiredHouse);

        if (!gameInProgress)
        {
            Console.WriteLine("Não existe um jogo em curso.");
            return;
        }
        if (!playersInGame.Any(p => pName == p.Name))
        {
            Console.WriteLine("Jogador não participa no jogo em curso.");
            return;
        }
        if (pName != CurrentPlayer.Name)
        {
            Console.WriteLine("Não é a vez do jogador.");
            return;
        }
        if (h == null || p.CurrentHouse != h)
        {
            Console.WriteLine("Não é possível comprar casa no espaço indicado.");
            return;
        }
        if (h.houseType != "Property" )
        {
            Console.WriteLine("Não é possível comprar casa no espaço indicado.");
            return;
        }
        if (h.houseOwner != p)
        {
            Console.WriteLine("Não é possível comprar casa no espaço indicado.");
            return;
        }
        if (!OwnsFullColorSet(p, h.color))
        {
            Console.WriteLine("O jogador não possui todos os espaços da cor.");
            return;
        }
        if (p.money < h.buildingPrice)
        {
            Console.WriteLine("O jogador não possui dinheiro suficiente.");
            return;
        }
        if (h.houseNumber >= 4)
        {
            Console.WriteLine("Não é possível comprar casa no espaço indicado.");
            return;
        }
            p.Pay(h.buildingPrice);
            h.houseNumber++;
            Console.WriteLine("Casa adquirida.");
    }


public void SendPlayerToPrison(Player p)
{
    House prison = BoardManager.FindHouseByName("Prison");

    cardManager.MovePlayerTo(p, "Prison");

    p.isInPrison = true;
    p.prisonTurns = 0;
}

public void HandlePrisonTurn(Player p, int? testMoveX, int? testMoveY)
{
    int d1, d2;

    if (testMoveX.HasValue && testMoveY.HasValue)
    {
        d1 = testMoveX.Value;
        d2 = testMoveY.Value;
    }
    else
    {
        Random rnd = new Random();
        d1 = rnd.Next(1, 7);
        d2 = rnd.Next(1, 7);
    }

    if (d1 == d2)
    {
        Console.WriteLine("Double! Sai da prisão.");
        p.isInPrison = false;
        p.prisonTurns = 0;

        // agora joga normalmente
        RollDices(d1, d2);
        return;
    }

    p.prisonTurns++;

    if (p.prisonTurns >= 3)
    {
        Console.WriteLine("Cumpriu 3 turnos. Sai da prisão.");
        p.isInPrison = false;
        p.prisonTurns = 0;
    }
    else
    {
        Console.WriteLine("Permanece na prisão.");
    }

    EndTurn();
}

public void PrintGameDetails()
{
    if (!gameInProgress)
    {
        Console.WriteLine("Não existe jogo em curso.");
        return;
    }

    int height = BoardManager.Houses.GetLength(0); // linhas (y)
    int width  = BoardManager.Houses.GetLength(1); // colunas (x)

    for (int y = 0; y < height; y++)
    {
        string line = "";

        for (int x = 0; x < width; x++)
        {
            House cell = BoardManager.Houses[y, x];
            if (cell == null)
            {
                line += "".PadRight(22);
                continue;
            }

            // Nome base do espaço
            string cellText = cell.houseName;

            // Dono do espaço (se for propriedade/linha e tiver dono)
            if ((cell.houseType == "Property" || cell.houseType == "Train") && cell.houseOwner != null)
            {
                cellText += $" ({cell.houseOwner.Name}" + (cell.houseNumber > 0 ? $" - {cell.houseNumber}" : "") + ")";
            }

            // Jogadores presos: aparecem como "detentores" de Prison (em vez de ocupantes)
            if (cell.houseName == "Prison")
            {
                var jailed = playersInGame.Where(p => p.isInPrison).Select(p => p.Name).ToList();
                if (jailed.Count > 0)
                    cellText += $" ({string.Join(", ", jailed)})";
            }

            // Ocupantes do espaço (quem está na célula)
            var occupants = playersInGame
                .Where(p => !p.isInPrison && p.posX == x && p.posY == y)
                .Select(p => p.Name)
                .ToList();

            if (occupants.Count > 0)
                cellText += " " + string.Join(" ", occupants);

            // padding para ficar legível em grelha
            line += cellText.PadRight(22);
        }

        Console.WriteLine(line.TrimEnd());
    }

    Console.WriteLine($"{CurrentPlayer.Name} - {CurrentPlayer.money}");
}


    
}