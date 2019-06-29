using MongoDB.Bson;
using MongoDB.Driver;
using System;

namespace MongoSharpTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                MongoClient dbClient = new MongoClient("mongodb://127.0.0.1:27017");

                //Database List  
                var dbList = dbClient.ListDatabases().ToList();

                Console.WriteLine("The list of databases are :");
                foreach (var item in dbList)
                {
                    Console.WriteLine(item);
                }

                Console.WriteLine("\n\n");

                //Get Database and Collection  
                IMongoDatabase db = dbClient.GetDatabase("test");
                var collList = db.ListCollections().ToList();
                Console.WriteLine("The list of collections are :");
                foreach (var item in collList)
                {
                    Console.WriteLine(item);
                }

                var things = db.GetCollection<BsonDocument>("things");

                //CREATE  
                BsonElement personFirstNameElement = new BsonElement("PersonFirstName", "Sankhojjal");

                BsonDocument personDoc = new BsonDocument();
                personDoc.Add(personFirstNameElement);
                personDoc.Add(new BsonElement("PersonAge", 23));

                things.InsertOne(personDoc);

                //UPDATE  
                BsonElement updatePersonFirstNameElement = new BsonElement("PersonFirstName", "Souvik");

                BsonDocument updatePersonDoc = new BsonDocument();
                updatePersonDoc.Add(updatePersonFirstNameElement);
                updatePersonDoc.Add(new BsonElement("PersonAge", 24));

                BsonDocument findPersonDoc = new BsonDocument(new BsonElement("PersonFirstName", "Sankhojjal"));

                var updateDoc = things.FindOneAndReplace(findPersonDoc, updatePersonDoc);

                Console.WriteLine(updateDoc);

                //DELETE  
                BsonDocument findAnotherPersonDoc = new BsonDocument(new BsonElement("PersonFirstName", "Souvik"));

                things.FindOneAndDelete(findAnotherPersonDoc);

                //READ  
                var resultDoc = things.Find(new BsonDocument()).ToList();
                foreach (var item in resultDoc)
                {
                    Console.WriteLine(item.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
}