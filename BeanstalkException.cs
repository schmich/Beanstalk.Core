using System;

namespace Beanstalk.Core {

    public class BeanstalkException : Exception {

        public BeanstalkException(string message) : base(message) { }

    }

}