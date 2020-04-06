This project made in order to demonstrate microservices connection with a QueueManager on RabbitMQ platform.

There is 2 standalone projects: CLIENT and SERVER:


Server:

The Program.cs class of the server generates connection to RabbitMQ platform, then creates "users-queue" queue in RabbitMQ if not existed.
User object will be genereted and the age of this user will be set to 0 as requested, then it will be serializing to a JSON format.
The JSON object will encrypted using stringCipher.cs class that implements AES256 encryption.
The encrypted string will encoded to UTF8 and transfered to the "users-queue" in RabbitMQ.


Client:

The Program.cs class of the client generates connection to the same RabbitMQ session, and creates "users-queue" queue if not existed.
Then, it will listening to the messages from the RabbitMQ session, and while gets a new message, it will encoding it and decrypt the string
using stringCipher.cs class. Finally, it will deserializing the JSON object back to the original user object, and will print all the
object's properties.



