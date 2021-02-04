using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace WebAPICodeFirstSample
{
    public class ResponseWrapper
    {
        [DataMember]
        public bool success { get; set; }

        [DataMember]
        public int status_code { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Meta meta { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public object data { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string message { get; set; }

        public ResponseWrapper(HttpStatusCode status_code, object data = null,
            Meta meta = null, string message = null)
        {
            this.status_code = (int)status_code;
            this.success = this.status_code == 200 ||
                           this.status_code == 201 ||
                           this.status_code == 204;
            this.data = data;
            this.meta = meta;
            this.message = message;
        }

        public ResponseWrapper(HttpStatusCode status_code, string message) :
           this(status_code, null, null, message)
        {
        }
    }
}
