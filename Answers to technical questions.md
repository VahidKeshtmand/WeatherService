# Technical Responses – Vahid Keshtmand

- **How much time did you spend on this task?**  
**If you had more time, what improvements or additions would you make?**  
For 4 hours. I would use caching to improve performance.
Since we are working with an external service, it would be good to use Circuit Breaker and Retry Pattern to increase the software's resilience.

---

- **Most useful feature recently added to your favorite programming language:**  
We can define fields, properties, and events with default values in an interface.

```csharp
public interface ILogger
{
    void Log(string message) => Console.WriteLine(message);
    string Name { get; set; } = "DefaultLogger";
    event EventHandler? OnLog
    {
        add => Console.WriteLine("Subscribed to OnLog");
        remove => Console.WriteLine("Unsubscribed from OnLog");
    }
}
```
- **How do you identify and diagnose a performance issue in a production environment? Have you done this before?**  
We need to use monitoring tools like Prometheus.
It is a tool that helps to measure various metrics, such as:

* CPU usage
* RAM usage
* Disk status
* GC activity
* Number of requests, how many failed, how many were accepted

I have used Prometheus and Grafana for a test service.


---

- **What’s the last technical book you read or technical conference you attended? What did you learn from it?**  
I attended a conference on System Design where I became familiar with concepts such as **Reliability, Security, Testability, Scalability, Performance** and more.
---

- **What’s your opinion about this technical test?**  
In my opinion, it was a good test to assess individuals' technical skills, and I enjoyed this challenge.

---

- **Please describe yourself using JSON format.**  
```json 
{
  "name": "Vahid Keshtmand",
  "role": ".NET Developer",
  "experience": "4 years",
  "summary": "Passionate .NET Developer with experience across the full software development lifecycle, including development, maintenance, and support of internal organizational and financial systems. Skilled in ASP.NET Core, Clean Architecture, EF Core, and front-end development with Vue.js. Experienced in building scalable APIs, real-time applications with SignalR, and integrating with external services.",
  "keySkills": [
    "C#",
    "ASP.NET Core",
    "EF Core",
    "Vue.js",
    "SQL Server",
    "RESTful APIs",
    "gRPC",
    "SignalR",
    "RabbitMQ",
    "xUnit",
    "NUnit",
    "Moq"
  ],
  "workExperience": [
    {
      "company": "Apna",
      "location": "Qom, Iran",
      "period": "Sep 2022 – Present",
      "description": "Worked on internal organizational applications using ASP.NET Core, Clean Architecture, and EF Core. Built scalable RESTful APIs and real-time features using SignalR. Developed front-end interfaces with Vue.js."
    },
    {
      "company": "Amnshabakegostar",
      "location": "Qom, Iran",
      "period": "Sep 2021 – Jun 2022",
      "description": "Contributed to a mobile accounting application using .NET Core and EF Core. Started as an intern and promoted to junior developer."
    }
  ],
  "education": {
    "degree": "B.Sc. in Computer Engineering",
    "university": "Shahab Danesh University",
    "location": "Qom, Iran",
    "year": "2018-2022"
  }
}
```