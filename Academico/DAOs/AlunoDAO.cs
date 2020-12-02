using Academico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace Academico.DAOs
{
    public class AlunoDAO
    {
        public void Inserir(Aluno obj) => registros.Add(obj);

        public void Excluir(string id)
        {
            var obj = RetornarPorId(id);

            if (obj != null)
                registros.Remove(obj);
        }

        public void Alterar(Aluno obj)
        {
            Excluir(obj.Id);
            Inserir(obj);
        }

        public IEnumerable<Aluno> RetornarTodos() => registros;

        public Aluno RetornarPorId(string id) => registros.Where(x => x.Id == id).FirstOrDefault();

        private static List<Aluno> registros = new List<Aluno>
        {
            new Aluno { Id = "1", Matricula = "123", Nome = "Ana" },
            new Aluno { Id = "2", Matricula = "125", Nome = "Bruno" },
            new Aluno { Id = "3", Matricula = "127", Nome = "Carlos" },
            new Aluno { Id = "4", Matricula = "129", Nome = "Daniela" },
            new Aluno { Id = "5", Matricula = "130", Nome = "Evandro" }
        };
    }
}
