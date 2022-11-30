using Azure.Identity;
using Azure.Messaging.ServiceBus;
using MiX.Integrate.Shared.Entities.Messages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unidas.MS.Telemetria.Application.Interfaces.Services.ServiceBus;
using Unidas.MS.Telemetria.Application.ViewModels.ServiceBus;

namespace Unidas.MS.Telemetria.Application.Services.ServiceBus
{
    public class ServiceBusService : IServiceBusService
    {
        private ServiceBusClient _client;
        private ServiceBusSender _sender;
        private ServiceBusReceiver _receiver;
        const int numOfMessages = 3;

        Dictionary<Guid, List<ServiceBusReceivedMessage>> _messages = new Dictionary<Guid, List<ServiceBusReceivedMessage>>();


        public ServiceBusService()
        {
            var clientOptions = new ServiceBusClientOptions
            {
                TransportType = ServiceBusTransportType.AmqpWebSockets
            };
            //TODO: Replace the "<NAMESPACE-NAME>" and "<QUEUE-NAME>" placeholders.

            _client = new ServiceBusClient(Config.GetFromAppSettings("ServiceBus:Golsat:ConnectionString1"), clientOptions);
            _sender = _client.CreateSender(Config.GetFromAppSettings("ServiceBus:Golsat:QueueName"));
            _receiver = _client.CreateReceiver(Config.GetFromAppSettings("ServiceBus:Golsat:QueueName"), new ServiceBusReceiverOptions() { ReceiveMode = ServiceBusReceiveMode.PeekLock });

        }

        public async Task SendAsync(IList<object> jsons)
        {

            // create a batch 
            using ServiceBusMessageBatch messageBatch = await _sender.CreateMessageBatchAsync();


            for (int i = 0; i < jsons.Count(); i++)
            {
                var jsonString = Util.Serialize(jsons[i]);
                if (!messageBatch.TryAddMessage(new ServiceBusMessage(jsonString)))
                {
                    throw new Exception($"The message {i} is too large to fit in the batch.");
                }
            }

            try
            {
                // Use the producer client to send the batch of messages to the Service Bus queue
                await _sender.SendMessagesAsync(messageBatch);
                //Console.WriteLine($"A batch of {numOfMessages} messages has been published to the queue.");
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

                    T obj = Util.Derialize<T>(message.Body.ToString());

                    serviceBusMessages.Add(message);

                    list.Add(obj);
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

        public async Task SendAsync(object json)
        {
            List<object> objects = new List<object>();
            objects.Add(json);

            await this.SendAsync(objects);
        }
    }
}
