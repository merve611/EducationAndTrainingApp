using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationAndTrainingApp.Business.Types
{
    public class ServiceMessage
    {
        public bool IsSucced { get; set; }
        public string Message { get; set; }
    }
    public class ServiceMessage<T>
    {
        public bool IsSucced { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }
    }

}
