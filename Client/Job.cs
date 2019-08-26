using System;

namespace Beanstalk.Core.Client {

    public class Job : IJob {

        public ulong Id { get; set; }

        public string Data { get; set; }

    }

}