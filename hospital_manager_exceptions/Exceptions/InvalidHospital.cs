using System;

namespace hospital_manager_exceptions.Exceptions
{
    public class InvalidHospital: Exception
    {
        private string _Message;

        public InvalidHospital(string message)
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
