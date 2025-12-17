public class Player {
    public string Name;
    public int gamesPlayed;
    public int wins;
    public int losses;
    public int ties;
    public double money = 1200; // TODO: check if this is needed.
    public int posX = 3, posY = 3;

    public bool hasDemandingAction = false, hasToPayRent = false, cardDrawnThisTurn = false;
    public House CurrentHouse{get; set;}

    public bool hasRolledDices = false;
    public bool isBankrupt = false, isInPrison = false;
    public int prisonTurns = 0; 
    public List<House> OwnedHouses {get; } = new(); // TODO pesquisar o que este comando faz


    


    // FIXME: some fields are missing;

    public Player(string Name) {
        this.Name = Name;
    }

    public void Pay(double amount)
{
    money -= amount;

    if (money < 0)
    {
        isBankrupt = true;
        Console.WriteLine($"{Name} ficou sem dinheiro e foi eliminado.");
    }

}

public void Receive(double amount)
{
    money += amount;
}
}
    