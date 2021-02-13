using System;

namespace hospital_manager_exceptions.Exceptions
{
    public class NotFoundUser : Exception
    {
        private string _Message;

        public NotFoundUser(string message)
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
