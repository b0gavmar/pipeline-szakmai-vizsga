using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeLine.Shared.Responses
{
    public class ChargingProgressResponse : Response
    {
        public bool IsFinished { get; set; } = false;
    }
}
