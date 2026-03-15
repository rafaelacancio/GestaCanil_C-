using MongoDB.Driver;
using System.Collections.Generic;

namespace GestaoCanil
{
    public class MongoDBService
    {
        private IMongoCollection<Cao> collection;

        public MongoDBService()
        {
            var client = new MongoClient("mongodb+srv://rafacancioiefp_db_user:Rafa2014.@cluster0.61twjrl.mongodb.net/?appName=Cluster0");
            var database = client.GetDatabase("CanilDB");

            collection = database.GetCollection<Cao>("Animais");
        }

        public void GuardarAnimal(Cao animal)
        {
            collection.InsertOne(animal);
        }

        public void GuardarAnimais(List<Cao> animais)
        {
            collection.DeleteMany(Builders<Cao>.Filter.Empty);

            if (animais.Count > 0)
            {
                collection.InsertMany(animais);
            }
        }

        public List<Cao> LerAnimais()
        {
            return collection.Find(_ => true).ToList();
        }
    }
}