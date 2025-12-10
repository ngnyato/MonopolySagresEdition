using System.Runtime.InteropServices.Marshalling;
using System.Runtime.Serialization.Formatters;

public class GameController
    {
        private Dictionary<string, Player> players = new Dictionary<string, Player>();
        bool gameInProgress = false;
        int turnIndex = 0;
        private List<Player> playersInGame = new List<Player>();


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
                       Console.WriteLine("{0} {1} {2} {3} {4}",    // Aprender melhor sobre formatação de strings
                       player.Name, player.gamesPlayed, player.wins, player.ties, player.losses); // é um place older para nao usar name + " " + gamesPlayed + " " + wins + " " + ties + " " + losses

                    }
                
        }

            public void StartGame(string[] playersGameList)
            {
                for (int i = 0; i < playersGameList.Length; i++)
                {
                    this.playersInGame.Add(players[playersGameList[i]]);
                }
           
            //TODO implemetar add dinheiro etc etc
                gameInProgress = true;
            }
        
        public bool IsGameInProgress()
        {
            return gameInProgress;
        }
        
        public int RollDices()
    {
        if (turnIndex >= playersInGame.Count)
        {
            turnIndex = 0;
        }

        Random x = new Random();// check if this is needed
        Random y = new Random();// check if this is needed 
        int moveX = x.Next(-3,4); // Min incluido e Max Excluido, gera um numero random 
        int moveY = y.Next(-3,4);

        int posX = posX + moveX;
        int posY = posY + moveY;
        playersInGame[turnIndex].posX = posX;
        playersInGame[turnIndex].posY = posY;

    
        Console.WriteLine("#DEBUG Posicao do jogador" + playersInGame[turnIndex].Name + "X E Y RESPETIVAMENTE" + posX, posY);

        
        
       
       
       
       
       
       
       
        turnIndex++;
    }
    }