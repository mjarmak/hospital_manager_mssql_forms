using System;

namespace hospital_manager_exceptions.Exceptions
{
    public class InvalidAppointment : Exception
    {
        private string _Message;

        public InvalidAppointment(string message)
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
