using MongoDB.Driver; // Biblioteca para trabalhar com MongoDB
using System.Collections.Generic; // Permite usar listas (List<T>)

namespace GestaoCanil // Namespace do projeto
{
    public class MongoDBService // Classe responsável pela comunicação com a base de dados
    {
        // Representa a coleção (equivalente a uma tabela) onde os cães serão armazenados
        private IMongoCollection<Cao> collection;

        public MongoDBService()
        {
            // Cria um cliente para ligar ao servidor MongoDB usando a connection string
            var client = new MongoClient("mongodb+srv://rafacancioiefp_db_user:Rafa2014.@cluster0.61twjrl.mongodb.net/?appName=Cluster0");

            // Seleciona a base de dados chamada "CanilDB"
            var database = client.GetDatabase("CanilDB");

            // Seleciona a coleção "Animais" onde os objetos Cao serão guardados
            collection = database.GetCollection<Cao>("Animais");
        }

        public void GuardarAnimal(Cao animal)
        {
            // Insere um único objeto Cao na coleção (base de dados)
            collection.InsertOne(animal);
        }

        public void GuardarAnimais(List<Cao> animais)
        {
            // Remove todos os documentos da coleção (limpa a base de dados)
            collection.DeleteMany(Builders<Cao>.Filter.Empty);

            // Verifica se existem elementos na lista antes de inserir
            if (animais.Count > 0)
            {
                // Insere vários documentos de uma só vez
                collection.InsertMany(animais);
            }
        }

        public List<Cao> LerAnimais()
        {
            // Procura todos os documentos da coleção (_ => true significa "traz todos")
            // Converte o resultado para uma lista de objetos Cao
            return collection.Find(_ => true).ToList();
        }
    }
}