# Beanstalk.Core
Beanstalkd client for .net core

### install
```bash
dotnet add package Beanstalk.Core --version 1.1.0
```

### example
```C#
    class Program {

        static async Task Main(string[] args) {
            await Task.Run(async () => {
                using (var client = new BeanstalkConnection("127.0.0.1", 11300)) {
                    await client.Use("mytube");
                    for (var i = 0; i < 10; i++) {
                        await client.Put($"Mission{i}", TimeSpan.FromSeconds(2 * i), 1024, TimeSpan.FromMinutes(1));
                    }
                }
            });
            using (var client = new BeanstalkConnection("127.0.0.1", 11300)) {
                await client.Watch("mytube");
                while (true) {
                    var job = await client.Reserve(TimeSpan.FromMinutes(5));
                    Console.WriteLine("Reversed job: {0} {1}", job.Id, job.Data);
                    await client.Delete(job.Id);
                }
            }
        }

    }
```
