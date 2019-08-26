using System;

namespace Beanstalk.Core.Client {

    public class BeanstalkException : Exception {

        public BeanstalkException(string message) : base(message) { }

    }

}