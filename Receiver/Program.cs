using EasyNetQ;
using Messages;
using OpenTelemetry;
using OpenTelemetry.Context.Propagation;
using System.Diagnostics;

namespace Receiver;

public class Program
{
    private static readonly TextMapPropagator Propagator = new TraceContextPropagator();

    public static void Main(string[] args)
    {
        Console.WriteLine("Starting Receiver Service...\n");
        var bus = RabbitHutch.CreateBus("host=localhost");
        
        bus.PubSub.SubscribeAsync<Message>("DTE", msg =>
        {
            var parentContext = Propagator.Extract(default, msg, (m, key) =>
            {
                if (msg.Headers.TryGetValue(key, out var value))
                {
                    return new[] { m.Headers.ContainsKey(key) ? m.Headers[key].ToString() : string.Empty };
                }

                return Enumerable.Empty<string>();
            });
            Baggage.Current = parentContext.Baggage;
            using var activity = Monitoring.Monitoring.ActivitySource.StartActivity("Message Received", ActivityKind.Consumer, parentContext.ActivityContext);

            Console.WriteLine(msg.Text);
        });

        Console.WriteLine("\n[Receiver] Connected to Queue");
        Console.WriteLine("[Receiver] Locking Listener...");
        while (true)
        {
            Thread.Sleep(5000);
        }
    }
}