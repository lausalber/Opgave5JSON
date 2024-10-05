using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class CalcRequest
    {
        public string Method { get; set; }
        public string Number1 { get; set; }
        public string Number2 { get; set; }
    }
}
