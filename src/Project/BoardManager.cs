   using System;
#nullable disable


public static class BoardManager
{
    
  public static readonly House[,] Houses  = new House[7, 7];
 
    public static void InitializeBoard()
    {
         // y = linha, x = coluna

        // linha 0
        Houses[0,0] = new House("Prison",       "Special",   null, 0,   0, 0, 0,null);
        Houses[0,1] = new House("Green3",       "Property", null, 160, 10, 1, 0, "Green");
        Houses[0,2] = new House("Violet1",      "Property", null, 150, 10, 2, 0, "Violet");
        Houses[0,3] = new House("Train2",       "Train",    null, 150, 25, 3, 0);
        Houses[0,4] = new House("Red3",         "Property", null, 160, 10, 4, 0, "Red");
        Houses[0,5] = new House("White1",       "Property", null, 160, 10, 5, 0, "White");
        Houses[0,6] = new House("BackToStart",  "Special",   null, 0,   0, 6, 0);

        // linha 1
        Houses[1,0] = new House("Blue3",        "Property", null, 170, 10, 0, 1, "Blue");
        Houses[1,1] = new House("Community",    "Special", null, 0,   0, 1, 1);
        Houses[1,2] = new House("Red2",         "Property", null, 130, 10, 2, 1,  "Red");
        Houses[1,3] = new House("Violet2",      "Property", null, 130, 10, 3, 1, "Violet");
        Houses[1,4] = new House("WaterWorks",   "Property",  null, 120, 15, 4, 1);
        Houses[1,5] = new House("Chance",       "Special",   null, 0,   0, 5, 1);
        Houses[1,6] = new House("White2",       "Property", null, 180, 10, 6, 1, "White");

        // linha 2
        Houses[2,0] = new House("Blue2",        "Property", null, 140, 10, 0, 2, "Blue");
        Houses[2,1] = new House("Red1",         "Property", null, 130, 10, 1, 2, "Red");
        Houses[2,2] = new House("Chance",       "Special",   null, 0,   0, 2, 2);
        Houses[2,3] = new House("Brown2",       "Property", null, 120, 10, 3, 2, "Brown");
        Houses[2,4] = new House("Community",    "Special", null, 0,   0, 4, 2);
        Houses[2,5] = new House("Black1",       "Property", null, 110, 10, 5, 2, "Black");
        Houses[2,6] = new House("LuxTax",       "Tax",      null, 0,   0, 6, 2);

        // linha 3
        Houses[3,0] = new House("Train1",       "Train",    null, 150, 25, 0, 3);
        Houses[3,1] = new House("Green2",       "Property", null, 140, 10, 1, 3, "Green");
        Houses[3,2] = new House("Teal1",        "Property", null, 90, 10, 2, 3, "Teal");
        Houses[3,3] = new House("Start",        "Special",   null, 0,   0, 3, 3);
        Houses[3,4] = new House("Teal2",        "Property", null, 130, 10, 4, 3, "Teal");
        Houses[3,5] = new House("Black2",       "Property", null, 120, 10, 5, 3, "Black");
        Houses[3,6] = new House("Train3",       "Train",    null, 150, 25, 6, 3);

        // linha 4
        Houses[4,0] = new House("Blue1",        "Property", null, 140, 10, 0, 4, "Blue");
        Houses[4,1] = new House("Green1",       "Property", null, 120, 10, 1, 4, "Green");
        Houses[4,2] = new House("Community",    "Special", null, 0,   0, 2, 4);
        Houses[4,3] = new House("Brown1",       "Property", null, 100, 10, 3, 4, "Brown");
        Houses[4,4] = new House("Chance",       "Special",   null, 0,   0, 4, 4);
        Houses[4,5] = new House("Black3",       "Property", null, 130, 10, 5, 4, "Black");
        Houses[4,6] = new House("White3",       "Property", null, 190, 10, 6, 4, "White");

        // linha 5
        Houses[5,0] = new House("Pink1",        "Property", null, 160, 10, 0, 5, "Pink");
        Houses[5,1] = new House("Chance",       "Special",   null, 0,   0, 1, 5);
        Houses[5,2] = new House("Orange1",      "Property", null, 120, 10, 2, 5, "Orange");
        Houses[5,3] = new House("Orange2",      "Property", null, 120, 10, 3, 5, "Orange");
        Houses[5,4] = new House("Orange3",      "Property", null, 140, 10, 4, 5, "Orange");
        Houses[5,5] = new House("Community",    "Special", null, 0,   0, 5, 5);
        Houses[5,6] = new House("Yellow3",      "Property", null, 170, 10, 6, 5, "Yellow");

        // linha 6
        Houses[6,0] = new House("FreePark",     "Special",   null, 0,   0, 0, 6);
        Houses[6,1] = new House("Pink2",        "Property", null, 180, 10, 1, 6, "Pink");
        Houses[6,2] = new House("ElectricCompany","Property",null, 120, 15, 2, 6);
        Houses[6,3] = new House("Train4",       "Train",    null, 150, 25, 3, 6);
        Houses[6,4] = new House("Yellow1",      "Property", null, 140, 10, 4, 6, "Yellow");
        Houses[6,5] = new House("Yellow2",      "Property", null, 140, 10, 5, 6, "Yellow");
        Houses[6,6] = new House("Police",       "Special",   null, 0,   0, 6, 6);




    }
    public static House FindHouseByName(string name)
{
    foreach (var h in Houses)
    {
        if (h != null && h.houseName.Equals(name, StringComparison.OrdinalIgnoreCase))
            return h;
    }
    return null;
}
 
  

}