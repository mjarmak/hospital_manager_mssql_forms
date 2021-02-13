using System;

namespace hospital_manager_exceptions.Exceptions
{
    public class NotFoundRoom : Exception
    {
        private string _Message;

        public NotFoundRoom(string message)
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
