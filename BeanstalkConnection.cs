using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Beanstalk.Core {

    public class BeanstalkConnection : IDisposable {

        private static readonly TimeSpan DefaultDelay = TimeSpan.Zero;

        private static readonly TimeSpan DefaultTtr = TimeSpan.FromMinutes(1);

        private static readonly uint DefaultPriority = 1024;

        private readonly string _host;

        private readonly ushort _port;

        private TcpClient _client;

        public BeanstalkConnection(string host, ushort port) {
            _host = host;
            _port = port;
        }

        private async Task<NetworkStream> GetStream() {
            if (_client != null && _client.Connected) return _client.GetStream();
            _client = new TcpClient();
            await _client.ConnectAsync(_host, _port);
            return _client.GetStream();
        }

        public void Dispose() { _client?.Dispose(); }

        public async Task<ulong> Put(string data, TimeSpan delay, uint priority, TimeSpan ttr) {
            return await new Command(await GetStream())
                .Put(priority, (uint) delay.TotalSeconds, (uint) ttr.TotalSeconds, data);
        }

        public async Task<ulong> Put(string data) {
            return await new Command(await GetStream())
                .Put(DefaultPriority, (uint) DefaultDelay.TotalSeconds, (uint) DefaultTtr.TotalSeconds, data);
        }

        public async Task<ulong> Put(string data, uint priority) {
            return await new Command(await GetStream())
                .Put(priority, (uint) DefaultDelay.TotalSeconds, (uint) DefaultTtr.TotalSeconds, data);
        }

        public async Task<ulong> Put(string data, TimeSpan delay) {
            return await new Command(await GetStream())
                .Put(DefaultPriority, (uint) delay.TotalSeconds, (uint) DefaultTtr.TotalSeconds, data);
        }

        public async Task Use(string tube) { await new Command(await GetStream()).Use(tube); }

        public async Task<IJob> Reserve() {
            return await new Command(await GetStream()).Reserve((uint) DefaultTtr.TotalSeconds);
        }

        public async Task<IJob> Reserve(TimeSpan timeout) {
            return await new Command(await GetStream()).Reserve((uint) timeout.TotalSeconds);
        }

        public async Task Delete(ulong id) { await new Command(await GetStream()).Delete(id); }

        public async Task Release(ulong id) {
            await new Command(await GetStream()).Release(id, DefaultPriority, (uint) DefaultDelay.TotalSeconds);
        }

        public async Task Release(ulong id, uint priority) {
            await new Command(await GetStream()).Release(id, priority, (uint) DefaultDelay.TotalSeconds);
        }

        public async Task Release(ulong id, TimeSpan delay) {
            await new Command(await GetStream()).Release(id, DefaultPriority, (uint) delay.TotalSeconds);
        }

        public async Task Release(ulong id, uint priority, TimeSpan delay) {
            await new Command(await GetStream()).Release(id, priority, (uint) delay.TotalSeconds);
        }

        public async Task Bury(ulong id) { await new Command(await GetStream()).Bury(id, DefaultPriority); }

        public async Task Bury(ulong id, uint priority) { await new Command(await GetStream()).Bury(id, priority); }

        public async Task Touch(ulong id) { await new Command(await GetStream()).Touch(id); }

        public async Task Watch(string tube) { await new Command(await GetStream()).Watch(tube); }

        public async Task Ignore(string tube) { await new Command(await GetStream()).Ignore(tube); }

        public async Task<IJob> Peek(ulong id) { return await new Command(await GetStream()).Peek(id); }

        public async Task<IJob> PeekReady() { return await new Command(await GetStream()).PeekReady(); }

        public async Task<IJob> PeekDelayed() { return await new Command(await GetStream()).PeekDelayed(); }

        public async Task<IJob> PeekBuried() { return await new Command(await GetStream()).PeekDelayed(); }

        public async Task<uint> Kick(uint bound) { return await new Command(await GetStream()).Kick(bound); }

        public async Task KickJob(ulong id) { await new Command(await GetStream()).KickJob(id); }

        public async Task<string> StatsJob(ulong id) { return await new Command(await GetStream()).StatsJob(id); }

        public async Task<string> StatsTube(string tube) {
            return await new Command(await GetStream()).StatsTube(tube);
        }

        public async Task<string> Stats() { return await new Command(await GetStream()).Stats(); }

        public async Task<string> ListTubes() { return await new Command(await GetStream()).ListTubes(); }

        public async Task<string> ListTubeUsed() { return await new Command(await GetStream()).ListTubeUsed(); }

        public async Task<string> ListTubesWatched() { return await new Command(await GetStream()).ListTubesWatched(); }

        public async Task Pause(string tube) {
            await new Command(await GetStream()).PauseTube(tube, (uint) DefaultDelay.TotalSeconds);
        }

        public async Task Pause(string tube, TimeSpan delay) {
            await new Command(await GetStream()).PauseTube(tube, (uint) delay.TotalSeconds);
        }

        public async Task Quit() { await new Command(await GetStream()).Quit(); }

    }

}