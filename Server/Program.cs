using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;


namespace Server
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

                // Random user that would be encrypted and transfered to the queue:
                User randUser = new User("Dan", "02/02/2020", 25.5, "Engineer");
                
                // Removing the age of the user before transfering the user to RabbitMQ as requested:
                randUser.age = 0;

                // Serializing the object to Json format:
                String jsonFormatUsr = JsonConvert.SerializeObject(randUser);

                // Encrypting the Json user:
                string Encrypted = stringCipher.Encrypt(jsonFormatUsr, Key);

                // Encoding the encrypted Json user to UTF8 before transfering to the queue:
                var encodedEncryptedJsonUsr = Encoding.UTF8.GetBytes(Encrypted);

                 // Publishing the user to the queue:
                model.BasicPublish(string.Empty, "users-queue", body: encodedEncryptedJsonUsr);

                Console.WriteLine("The user with the name: " + randUser.name + " was exported successfully!");
            }
        }
    }
}
