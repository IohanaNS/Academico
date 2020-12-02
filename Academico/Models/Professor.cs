using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Academico.Models
{
    public class Professor
    {
        public int Id { get; set; }

        public string Matricula { get; set; }

        public string Nome { get; set; }
        public string Email { get; set; }
    }
}
