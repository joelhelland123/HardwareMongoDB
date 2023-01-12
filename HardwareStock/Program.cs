using Microsoft.VisualBasic;
using MongoDataAccess.DataAccess;
using MongoDataAccess.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Diagnostics.Metrics;

while (true)
{
    HardWareDataAccess db = new HardWareDataAccess();
    int userChoice = db.InputMenuControl();

    //await db.CreateHardWare(new HardWareModel()
    //{
    //    Namn = "Såg",
    //    Beskrivning = "Kan såga"
    //});

    //await db.CreateHardWare(new HardWareModel()
    //{
    //    Namn = "Hammare",
    //    Beskrivning = "Kan Hamra"

    //});


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
                Console.WriteLine($"{counter}. Name:{hardWares.Name}\n" +
                                  $"   discription: {hardWares.Description}\n");
                counter++;
            }
            Console.WriteLine("\n");

            break;


        case 3:
            await db.HardWareUpdateControl();
            //var hardwares = await db.GetAllHardWares();
            //int räknare = 1;
            //foreach(var hardware in hardwares)
            //{
            //    Console.WriteLine(räknare + hardware.Namn);
            //    räknare++;
            //}
            //Console.WriteLine("Välj ett av verktygen");
            //int val = int.Parse(Console.ReadLine());
            //HardWareModel valet = hardwares[val-1];
            //Console.WriteLine("Skriv in ett namn");
            //string namnet = Console.ReadLine();
            //valet.Namn = namnet;
            //var hardwares = new HardWareModel();
            //await db.UpdateHardWare(db.HardWareControl(hardwares());
            //var filter = Builders<BsonDocument>.Filter.Eq("Namn", "Hammare");
            //var update = Builders<BsonDocument>.Update.Set("Namn", filter);
            //hardWareCollection = await db.GetAllHardWares();

            break;
        case 4:
            await db.HardWareDeleteControl();
            //int counter1 = 1;
            //int val;
            //var hardWareCollection1 = await db.GetAllHardWares();
            //foreach (var hardWares in hardWareCollection1)
            //{
            //    Console.WriteLine($"{counter1}. Namn:{hardWares.Namn}\n Beskrivning: {hardWares.Beskrivning}");
            //    counter1++;
            //}
            //Console.WriteLine("Skriv den in siffran på verktyget du önskar");
            ////val > HardWareCollection.Count() ||
            //while (!Int32.TryParse(Console.ReadLine(), out val) ||  val < 1)
            //{
            //    Console.WriteLine("Du behöver skriva en siffra, välj något av alternativen");
            //}
            //await db.DeleteHardWare(hardWareCollection1[val-1]);
            break;
        case 5:
            Environment.Exit(1);
            break;
        default:
            break;
    }

}




