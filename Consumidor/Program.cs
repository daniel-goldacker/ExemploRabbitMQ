using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Consumidor
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
                 channel.QueueDeclare(queue: "saudacao_1",
                                      durable: false,
                                      exclusive: false,
                                      autoDelete: false,
                                      arguments: null);

                    // Solicita a entrega das mensagens de forma assíncrona e fornece um retorno de chamada 
                    var consumer = new EventingBasicConsumer(channel);

    
                    // Recebe a mensagem da fila converte para string e imprime no console a mensagem 
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine("[X] Recebida: {0}", message);
                    };

                    // Indicamos o consumo da mensagem
                    channel.BasicConsume(queue: "saudacao_1",
                                         autoAck: true,
                                         consumer: consumer);

                    Console.ReadLine();

                }
            }
        }
    }
}
