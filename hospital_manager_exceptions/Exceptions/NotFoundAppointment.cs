using System;

namespace hospital_manager_exceptions.Exceptions
{
    public class NotFoundAppointment : Exception
    {
        private string _Message;

        public NotFoundAppointment(string message)
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
