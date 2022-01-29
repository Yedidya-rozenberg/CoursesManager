using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesManager.Logic.CoursState
{
    class AuthorizationException : Exception
    {
        public AuthorizationException(): base()
        {

        }
        public AuthorizationException(string message) : base(message)
        {
        }
    }
}
