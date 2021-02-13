using System;

namespace hospital_manager_exceptions.Exceptions
{
    public class NotFoundSpeciality : Exception
    {
        private string _Message;

        public NotFoundSpeciality(string message)
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
