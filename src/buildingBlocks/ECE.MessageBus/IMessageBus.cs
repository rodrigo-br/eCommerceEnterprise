﻿using EasyNetQ;
using EasyNetQ.Internals;
using ECE.Core.Messages.Integration;

namespace ECE.MessageBus
{
    public interface IMessageBus : IDisposable 
    {
        bool IsConnected { get; }
        public IAdvancedBus AdvancedBus { get; }

        void Publish<T>(T message) where T : IntegrationEvent;

        Task PublishAsync<T>(T message) where T : IntegrationEvent;

        void Subscribe<T>(string subscriptionId, Action<T> onMessage) where T : class;

        void SubscribeAsync<T>(string  subscriptionID, Func<T, Task> onMessage) where T : class;

        TResponse Request<TRequest, TResponse>(TRequest request) 
            where TRequest : IntegrationEvent
            where TResponse : ResponseMessage;

        Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest request)
            where TRequest : IntegrationEvent
            where TResponse : ResponseMessage;

        IDisposable Respond<TRequest, TResponse>(Func<TRequest, TResponse> responder)
            where TRequest : IntegrationEvent
            where TResponse : ResponseMessage;

        AwaitableDisposable<IDisposable> RespondAsync<TRequest, TResponse>(Func<TRequest, Task<TResponse>> responder)
            where TRequest : IntegrationEvent
            where TResponse : ResponseMessage;
    }
}