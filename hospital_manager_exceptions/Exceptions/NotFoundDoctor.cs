using System;

namespace hospital_manager_exceptions.Exceptions
{
    public class NotFoundDoctor : Exception
    {
        private string _Message;

        public NotFoundDoctor(string message)
        {
            _Message = message;
        }

        public override string Message
        {
            get
            {
                return _Message;
            }
        }

    }
}
