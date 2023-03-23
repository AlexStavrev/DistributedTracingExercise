# SendReceiveTracing

Create a trace in a destributed envirornment such as microservices using Zipkin and Opentelemetry.
To run: Have docker up and running (E.g. Docker Desktop on Windows)
Run `docker-compose up` to run the zipkin UI
Start the Reveiver service trough your IDE of cohice,
Start the Sender from your IDE of choice

Sender sends a message trough RabbitMQ (connection string needed in both Program.cs files) and Receiver reads it
The result in zipkin:

![image](https://user-images.githubusercontent.com/40688355/227172054-7292ece4-62c5-46d7-b1ba-e130b74efbb1.png)
