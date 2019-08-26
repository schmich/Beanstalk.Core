using System.Threading.Tasks;

namespace Beanstalk.Core {

    public interface IConnection {

        Task<ulong> Put(uint priority, uint delay, uint ttr, string data);

        Task Use(string tube);

        Task<IJob> Reserve(uint timeout);

        Task Delete(ulong id);

        Task Release(ulong id, uint priority, uint delay);

        Task Bury(ulong id, uint priority);

        Task Touch(ulong id);

        Task<uint> Watch(string tube);

        Task<uint> Ignore(string tube);

        Task<IJob> Peek(ulong id);

        Task<IJob> PeekReady();

        Task<IJob> PeekDelayed();

        Task<IJob> PeekBuried();

        Task<uint> Kick(uint bound);

        Task KickJob(ulong id);

        Task<string> StatsJob(ulong id);

        Task<string> StatsTube(string tube);

        Task<string> Stats();

        Task<string> ListTubes();

        Task<string> ListTubeUsed();

        Task<string> ListTubesWatched();

        Task PauseTube(string tube, uint delay);

        Task Quit();

    }

}