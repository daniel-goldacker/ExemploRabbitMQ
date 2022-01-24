using System;
using System.Text;
using RabbitMQ.Client;

namespace Publicador
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "Localhost"
            };

            // Abrimos uma conexão com um nó RabbitMQ
            using (var connection = factory.CreateConnection())
            {
                // Criamos um canal onde vamos definir uma fila, uma mensagem e publicar a mensagem
                using (var channel = connection.CreateModel())
                {
                    // Declaramos a fila apartir da qual vamos consumir as mensagens 
                    channel.QueueDeclare(queue: "meu_primeiro_rabbitmq_1",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    // Criamos a mensagem a ser posta na fila e codificamos a mensagem como um arrayu de bytes 
                    string message = "Meu primeiro RabbitMQ";
                    var body = Encoding.UTF8.GetBytes(message);

                    // Publicamos a mensagem informando a fila e o corpo da mensagem
                    channel.BasicPublish(exchange: "",
                                         routingKey: "meu_primeiro_rabbitmq_1",
                                         basicProperties: null,
                                         body: body);

                    Console.WriteLine("[X] Enviada: {0}", message);
                    Console.ReadLine();
                }
            }
        }
    }
}