using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCase.Dtos
{
    public class UserResponse
    {
        public string Token { get; set; }
        public List<string> Notification { get; set; }
    }
}