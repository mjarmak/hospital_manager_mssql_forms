using System;

namespace hospital_manager_exceptions.Exceptions
{
    public class InvalidUserRequest : Exception
    {
        private string _Message;

        public InvalidUserRequest(string message)
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
