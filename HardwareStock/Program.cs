using Microsoft.VisualBasic;
using MongoDataAccess.DataAccess;
using MongoDataAccess.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Diagnostics.Metrics;

HardWareDataAccess db = new HardWareDataAccess();

await db.CreateHardWare(new HardWareModel()
{
    Name = "Såg",
    Description = "En såg är ett verktyg för att kapa och klyva material. Den har oftast ett handtag, " +
    "men kan ha flera. Det finns flera typer av sågar med sinsemellan olika användningsområden. " +
    "Vanligast är sågar avsedda för att såga trä, men det finns också sågar avsedda för metall"
});

await db.CreateHardWare(new HardWareModel()
{
    Name = "Hammare",
    Description = "En hammare består av huvud och skaft. Skaftet kan vara av trä, metall eller glasfiber." +
    " Det fästs i det elliptiskt koniska ögat i huvudet och kilas fast med metall- eller träkil i ett sågat spår i skaftet (om detta är av trä). " +
    "Det hela kan förstärkas med epoxylim som fyller ut luckor i ögat mellan skaft och huvud. Ytterligare förstärkning kan förekomma med järnhakar genom ögat." +
    " Hakarna spikas fast mot skaftet.\r\n\r\nEtt träskaft ska vara av hårt träslag som krymper så lite som möjligt vid varierande fuktighet. Bästa svenska träslag är ask, av utländska träslag är hickory bäst."

});

while (true)
{
    int userChoice = db.InputMenuControl();

    switch (userChoice)
    {
        case 1:
            string name = "";
            string description = "";
            
            while (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Input name");
                name = Console.ReadLine();
            }
            while (string.IsNullOrEmpty(description))
            {
                Console.WriteLine("Input discription");
                description = Console.ReadLine();
            }

            var hardWare = new HardWareModel { Name = name, Description = description };
            await db.CreateHardWare(hardWare);
            Console.WriteLine("Tool added\n");
            break;

        case 2:
            int counter = 1;
            var hardWareCollection = await db.GetAllHardWares();
            foreach (var hardWares in hardWareCollection)
            {
                Console.WriteLine($"{counter}. Name:{hardWares.Name}\n");
                                 
                counter++;
            }
            Console.WriteLine("\n");
            break;

        case 3:
            await db.HardWareUpdateControl();
            break;

        case 4:
            await db.HardWareDeleteControl();
            break;

        case 5:
            Environment.Exit(1);
            break;

        default:
            break;
    }

}




