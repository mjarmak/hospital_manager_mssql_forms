using System;

namespace hospital_manager_exceptions.Exceptions
{
    public class NotFoundConsultation : Exception
    {
        private string _Message;

        public NotFoundConsultation(string message)
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
