using EasyNetQ;
using Messages;
using OpenTelemetry.Context.Propagation;
using OpenTelemetry;
using System.Diagnostics;

namespace Sender;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Starting Sender Service...");
        using (var activity = Monitoring.Monitoring.ActivitySource.StartActivity())
        {
            var bus = RabbitHutch.CreateBus("host=localhost");
            var message = new Message("Hello World");

            var activityContext = activity?.Context ?? Activity.Current?.Context ?? default;
            var propagationContext = new PropagationContext(activityContext, Baggage.Current);
            var propagator = new TraceContextPropagator();
            propagator.Inject(propagationContext, message, (msg, key, value) =>
            {
                msg.Headers[key] = value;
            });

            bus.PubSub.PublishAsync(message);
        }

        Console.WriteLine("[Sender] Queue Setup...");
        Console.WriteLine("[Sender] Waitning 5 seconds");
        Thread.Sleep(5000); // Give Zipkin time to receive the event
        Console.WriteLine("[Sender] Done!");
    }
}