namespace Beanstalk.Core {

    public class BeanstalkFactory {

        private readonly string _host;

        private readonly ushort _port;

        public BeanstalkFactory(string host, ushort port) {
            _host = host;
            _port = port;
        }

        public BeanstalkConnection GetConnection() { return new BeanstalkConnection(_host, _port); }

    }

}