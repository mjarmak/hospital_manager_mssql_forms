using System;
using System.Collections.Generic;
using System.Text;

namespace hospital_manager_exceptions.Exceptions
{
    public class InvalidRoom : Exception
    {

        private string _Message;

        public InvalidRoom(string message)
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
