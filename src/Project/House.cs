public class House 
{   
     // #FUNNY/ if i put a doctor here in this class it becomes DOCTOR HOUSE ?!?!
     // https://i.redd.it/0iqk8cd2yh9e1.jpeg

    public string houseName { get; set;}
    public string houseType { get; set;}  //Property, Train, Tax, Chance, Community, Special
    public Player houseOwner { get; set;} 
    public int housePrice { get; set;}
    public int houseRent { get; set;}
    public int houseNumber  = 0;
    public int buildingPrice => (int)Math.Round(housePrice * 0.6);
    public string color { get; set; }

    public int x { get; set;}
    public int y { get; set;}

    public House( string houseName, string houseType, Player houseOwner, int housePrice, int houseRent, int x, int y, string color = null) 
    {
        this.houseName = houseName;
        this.houseType = houseType;
        this.houseOwner = houseOwner;
        this.housePrice = housePrice;
        this.houseRent = houseRent;
        this.x = x;
        this.y = y;
    }



}