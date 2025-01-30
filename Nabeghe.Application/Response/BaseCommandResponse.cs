using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nabeghe.Application.Response
{
    public class BaseCommandResponse
    {
        public int Id { get; set; }
        public List<string> ErroresList { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }
}
