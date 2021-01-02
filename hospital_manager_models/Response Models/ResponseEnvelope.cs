using System;
using System.Collections.Generic;
using System.Text;

namespace hospital_manager_models.Response_Models
{
    public class ResponseEnvelope<T>
    {
        public T data { get; set; }
    }
}
