using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace registerCardApi.Model
{
    public class TokenDTO
    {
        private string _token;

        public string Token { get => _token; set => _token = value; }
    }
}
