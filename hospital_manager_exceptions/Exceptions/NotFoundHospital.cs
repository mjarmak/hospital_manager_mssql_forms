using System;

namespace hospital_manager_exceptions.Exceptions
{
    public class NotFoundHospital : Exception
    {
        private string _Message;

        public NotFoundHospital(string message)
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
