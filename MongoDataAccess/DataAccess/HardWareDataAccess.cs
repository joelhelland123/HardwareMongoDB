using MongoDataAccess.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Runtime.CompilerServices;

namespace MongoDataAccess.DataAccess
{
    public class HardWareDataAccess
    {
        public const string ConnectionString = "mongodb+srv://joel123:helland123@cluster0.ym1o2td.mongodb.net/test";
        public const string Database = "HardWareDatabase";
        public const string HardWareCollection = "HardWareCollection";

        private IMongoCollection<T> ConnectToMongo<T>(string collection)
        {
            var client = new MongoClient(ConnectionString);
            var db = client.GetDatabase(Database);
            return db.GetCollection<T>(collection);
        }

        public async Task<HardWareModel> GetOneHardWare(HardWareModel hardWare)
        {
            var hardwareCollection = ConnectToMongo<HardWareModel>(HardWareCollection);
            var results = await hardwareCollection.FindAsync(h => h.Id == hardWare.Id);
            return results.FirstOrDefault();
            
        }
        public async Task<List<HardWareModel>> GetAllHardWares()
        {
            var hardwareCollection = ConnectToMongo<HardWareModel>(HardWareCollection);
            var results = await hardwareCollection.FindAsync(_ => true);
            return results.ToList();
        }

        public Task CreateHardWare(HardWareModel hardWare)
        {
            var hardWareCollection = ConnectToMongo<HardWareModel>(HardWareCollection);
            return hardWareCollection.InsertOneAsync(hardWare);
        }

        public Task UpdateHardWare(HardWareModel hardWare)
        {
            var hardWareCollection = ConnectToMongo<HardWareModel>(HardWareCollection);
            var filter = Builders<HardWareModel>.Filter.Eq("Id", hardWare.Id);
            return hardWareCollection.ReplaceOneAsync(filter, hardWare, new ReplaceOptions { IsUpsert = true });
        }

        public Task DeleteHardWare(HardWareModel hardware)
        {
            var hardWareCollection = ConnectToMongo<HardWareModel>(HardWareCollection);
            return hardWareCollection.DeleteOneAsync(h => h.Id == hardware.Id);
        }

        public int InputMenuControl()
        {
            Console.WriteLine("\n");
            Console.WriteLine("=======================================");
            Console.WriteLine("Choose one of ther alternatives");
            Console.WriteLine("1. Add a new tool");
            Console.WriteLine("2. Verify what tools are available");
            Console.WriteLine("3. Update toolinformation");
            Console.WriteLine("4. Remove a tool");
            Console.WriteLine("5. End program");

            int choice = 0;

            while (!Int32.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 5)
            {
                Console.WriteLine("Input is invalid, try again");
            }
            return choice;
        }
        public int InputModelControl(int valet)
        {
            int choice = 0;
            while (!Int32.TryParse(Console.ReadLine(), out choice) || choice > HardWareCollection.Count() || choice < 1)
            {
                Console.WriteLine("Input is invalid, try again");
            }
            return choice;
        }
        public async Task HardWareUpdateControl()
        {
            string NameChange = "";
            string DescriptionChange = "";
            int choice = 0;
            int counter = 1;
            var hardwares = await GetAllHardWares();
            foreach (var hardWare in hardwares)
            {
                Console.WriteLine(counter + ". " + hardWare.Name);
                counter++;
            }

            Console.WriteLine("Input tool number");
            

            while (!Int32.TryParse(Console.ReadLine(), out choice) || choice > hardwares.Count() || choice < 1)
            {
                Console.WriteLine("Input is invalid, try again");
            }

            while (string.IsNullOrEmpty(NameChange))
            {
                Console.WriteLine("Write a name?");
                NameChange = Console.ReadLine();
                
            }
            hardwares[choice - 1].Name = NameChange;
            while (string.IsNullOrEmpty(DescriptionChange))
            {
                Console.WriteLine("Write a discription?");
                DescriptionChange = Console.ReadLine();  
            }
            hardwares[choice - 1].Description = DescriptionChange;
            await UpdateHardWare(hardwares[choice - 1]);
            Console.WriteLine($"Tool updated\n");
        }
        public async Task HardWareDeleteControl()
        {
            int counter1 = 1;
            int choice;
            var hardWareCollection1 = await GetAllHardWares();
            foreach (var hardWares in hardWareCollection1)
            {
                Console.WriteLine($"{counter1}. Name:{hardWares.Name}\n");
                                  
                counter1++;
            }
            Console.WriteLine("Input the number of what tool would you like to remove");
            //val > HardWareCollection.Count() ||
            while (!Int32.TryParse(Console.ReadLine(), out choice) || choice < 1)
            {
                Console.WriteLine("Input is invalid, try again");
            }
            
            await DeleteHardWare(hardWareCollection1[choice - 1]);
            Console.WriteLine($"Tool Erased\n");
        }
    }
}
