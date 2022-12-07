using Azure.Identity;
using Azure.Messaging.ServiceBus;
using MiX.Integrate.Shared.Entities.Messages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unidas.MS.Telemetria.Application.Exceptions;
using Unidas.MS.Telemetria.Application.Interfaces.Services.ServiceBus;
using Unidas.MS.Telemetria.Application.ViewModels.ServiceBus;

namespace Unidas.MS.Telemetria.Application.Services.Queue
{
    public class ServiceBusService : IServiceBusService
    {
        private ServiceBusClient _client;
        private ServiceBusSender _sender;
        private ServiceBusReceiver _receiver;
        const int numOfMessages = 3;

        Dictionary<Guid, List<ServiceBusReceivedMessage>> _messages = new Dictionary<Guid, List<ServiceBusReceivedMessage>>();


        public ServiceBusService(string connectionString, string queueName)
        {
            var clientOptions = new ServiceBusClientOptions
            {
                TransportType = ServiceBusTransportType.AmqpWebSockets
            };
            //TODO: Replace the "<NAMESPACE-NAME>" and "<QUEUE-NAME>" placeholders.

            _client = new ServiceBusClient(connectionString, clientOptions);
            _sender = _client.CreateSender(queueName);
            _receiver = _client.CreateReceiver(queueName, new ServiceBusReceiverOptions() { ReceiveMode = ServiceBusReceiveMode.PeekLock });

        }

        public async Task SendAsync<T>(T json)
        {
            List<T> objects = new List<T>();
            objects.Add(json);

            await this.SendListAsync<T>(objects);
        }

        public async Task SendListAsync<T>(IList<T> jsons)
        {

            List<ServiceBusMessageBatch> batchMessages = new List<ServiceBusMessageBatch>();
            // create a batch 
            ServiceBusMessageBatch tempMessageBatch = await _sender.CreateMessageBatchAsync();



            for (int i = 0; i < jsons.Count(); i++)
            {
                var jsonString = Util.Serialize(jsons[i]);
                if (!tempMessageBatch.TryAddMessage(new ServiceBusMessage(jsonString)))
                {
                    throw new Exception($"The message {i} is too large to fit in the batch.");
                }

                if (i > 0 && i % 100 == 0 || i == jsons.Count - 1)
                {
                    batchMessages.Add(tempMessageBatch);
                    tempMessageBatch = await _sender.CreateMessageBatchAsync();
                }


            }

            try
            {
                foreach (var batchMesage in batchMessages)
                {
                    // Use the producer client to send the batch of messages to the Service Bus queue
                    _sender.SendMessagesAsync(batchMesage).Wait();
                    
                    
                    //Console.WriteLine($"A batch of {numOfMessages} messages has been published to the queue.");
                }
            }
            finally
            {
                //await _sender.DisposeAsync();
                //await _client.DisposeAsync();
            }

        }

        public async Task<ServiceBusVM<T>> ReadAsync<T>()
        {
            try
            {
                var guid = Guid.NewGuid();

                List<T> list = new List<T>();
                List<ServiceBusReceivedMessage> serviceBusMessages = new List<ServiceBusReceivedMessage>();

                
                var messages = await _receiver.ReceiveMessagesAsync(1000, TimeSpan.FromSeconds(30)); // You have read one message


                //if (message == null) // Continue till you receive no message from the Queue
                //    break;


                foreach (var message in messages)
                {
                    try
                    {
                        var json = message.Body.ToString();
                        T obj = Util.Deserialize<T>(json);

                        serviceBusMessages.Add(message);

                        list.Add(obj);
                    }
                    catch (JsonNotValidException ex)
                    {
                        //logar o erro
                    }
                }


                if (list.Count > 0)
                    _messages.Add(guid, serviceBusMessages);

                var serviceBusVM = new ServiceBusVM<T>()
                {
                    Guid = guid,
                    Messages = list
                };


                return serviceBusVM;

            }
            finally
            {
                //await _processor.DisposeAsync();
                //await _client.DisposeAsync();
            }
        }

        public async Task DeleteAsync(Guid guid)
        {
            var messages = _messages[guid];

            if (messages == null)
                throw new Exception();

            foreach (var message in messages)
            {
                await _receiver.CompleteMessageAsync(message);
            }

            _messages.Remove(guid);
        }


    }
}
