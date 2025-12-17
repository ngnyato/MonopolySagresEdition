using System;
namespace MonopolyC;
public class CardManager
{
   
    public void DrawCard(string pName, GameController gc)
{
 
    if (!gc.gameInProgress)
    {
        Console.WriteLine("Não existe um jogo em curso.");
        return;
    }

    if (!gc.playersInGame.Any(pl => pl.Name == pName))
    {
        Console.WriteLine("Jogador não participa no jogo em curso.");
        return;
    }

    if (gc.CurrentPlayer.Name != pName)
    {
        Console.WriteLine("Não é a vez do jogador.");
        return;
    }

    Player p = gc.CurrentPlayer;
    House h = p.CurrentHouse;

   
    if (h.houseName != "Chance" && h.houseName != "Community")
    {
        Console.WriteLine("Não é possível tirar carta neste espaço.");
        return;     
    }


    if (p.cardDrawnThisTurn)
    {
        Console.WriteLine("A carta já foi tirada.");
        return;
    }



    int roll = new Random().Next(1, 101); // 1..100
    p.cardDrawnThisTurn = true;

    if (h.houseName == "Chance")
    {
        // 20 / 10 / 10 / 20 / 20 / 20  (total 100)
        if (roll <= 20)
        {  
            p.Receive(150); 
            Console.WriteLine("O jogador recebe 150.");
        }
        else if (roll <= 30)
        {
            p.Receive(200);
            Console.WriteLine("O jogador recebe 200.");
        }
        else if (roll <= 40)
        {   
            p.Pay(70);
            Console.WriteLine("O jogador tem de pagar 70.");
        }
        else if (roll <= 60)
        {
            MovePlayerTo(p, "Start");
            Console.WriteLine("O jogador move-se para a casa Start."); //TODO Missing Implementation
        }
        else if (roll <= 80)
        {
            MovePlayerTo(p, "Police");
            Console.WriteLine("O jogador move-se para a casa Police."); //TODO Missing Implementation
        }
        else
        {
            MovePlayerTo(p, "FreePark");
            Console.WriteLine("O jogador move-se para a casa FreePark."); //TODO Missing Implementation
        }
    }
    else // Community
    {
        // 10 / 10 / 20 / 20 / 10 / 10 / 10 / 10  (total 100)
        if (roll <= 10)
        {
            int totalHouses = p.OwnedHouses.Sum(x => x.houseNumber);
            int pay = 20 * totalHouses;
            p.Pay(pay);
            Console.WriteLine($"  O jogador paga 20 por cada casa nos seus espaços. Total: {pay}.");
        }
        else if (roll <= 20)
        {
            int received = 0;
            foreach (var other in gc.playersInGame)
            {
                if (other == p) continue;
                other.money -= 10;
                received += 10;
            }
            p.Receive(received);    
            Console.WriteLine($"  O jogador recebe 10 de cada outro jogador.");
        }
        else if (roll <= 40)
        {
           p.Receive(100);
            Console.WriteLine("O jogador recebe 100.");
        }
        else if (roll <= 60)
        {
            p.Receive(170);
            Console.WriteLine("O jogador recebe 170.");
        }
        else if (roll <= 70)
        {
            p.Pay(40);
            Console.WriteLine("O jogador tem de pagar 40.");
        }
        else if (roll <= 80)
        {
            MovePlayerTo(p, "Pink1");
            Console.WriteLine("O jogador move-se para Pink1.");
        }
        else if (roll <= 90)
        {
            MovePlayerTo(p, "Teal2");
            Console.WriteLine(" O jogador move-se para Teal2.");
        }
        else
        {
            MovePlayerTo(p, "White2");
            Console.WriteLine("O jogador move-se para White2.");
        }
    }

    // se TC era obrigatório, podes “libertar” a ação pendente
    p.hasDemandingAction = false;
}   
public void MovePlayerTo(Player p, string houseName)
{
    House target = BoardManager.FindHouseByName(houseName);
    if (target == null) return;

    p.posX = target.x;
    p.posY = target.y;
    p.CurrentHouse = target;
}

}