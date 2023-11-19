using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Retry;

namespace ECE.WebApi.Core.Extensions
{
    public static class PollyExtensions
    {
        public static IHttpClientBuilder WaitAndRetry(this IHttpClientBuilder builder)
        {
            builder.AddTransientHttpErrorPolicy(p =>
                    p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(600)))
                .AddTransientHttpErrorPolicy(p =>
                    p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            return builder;
        }
    }
}
