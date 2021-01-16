using System;
using System.Collections.Generic;
using System.Text;

namespace hospital_manager_models.Models
{
    public class ResponseEnvelope<T>
    {
        public T data { get; set; }
    }
}
