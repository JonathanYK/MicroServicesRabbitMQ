using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            // This key will be used to encryption
            String Key = "Very_crazy_password";
            
            // RabbitMQ connection setup:
            var connectRabbitMQ = new ConnectionFactory
            {
                HostName = "127.0.0.1",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };

            using (var connection = connectRabbitMQ.CreateConnection())
            using (var model = connection.CreateModel())

            {
                // Creates users-queue in RabbitMQ if not existed:
                model.QueueDeclare("users-queue", durable: true, exclusive: false, autoDelete: false);

                // Recieving data from the queue:
                var consumer = new EventingBasicConsumer(model);
                consumer.Received += (eventModel, args) =>
                {

                    // Encoding of the recieved data:
                    String usr = Encoding.UTF8.GetString(args.Body);

                    // Decrypting the recieved data:
                    String Decrypted = stringCipher.Decrypt(usr, Key);

                    // Deserializing Json format to the original object:
                    User importedUsr = JsonConvert.DeserializeObject<User>(Decrypted);

                    // Printing the user's fields:
                     Console.WriteLine("-------------------");
                     Console.WriteLine("The name of the imported user is: " + importedUsr.name);
                     Console.WriteLine("The Date is: " + importedUsr.date);
                     Console.WriteLine("And his proffesion is: " + importedUsr.profession);
                     Console.WriteLine("-------------------"); 
                };

                model.BasicConsume("users-queue", true, consumer);
                Console.Read();
            }
        }
    }
}

