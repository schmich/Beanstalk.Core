using System;

namespace Beanstalk.Core {

    public interface IJob {

        ulong Id { get; set; }

        string Data { get; set; }

    }

}