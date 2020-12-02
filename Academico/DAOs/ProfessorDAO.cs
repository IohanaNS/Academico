using Academico.Controllers;
using Academico.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Academico.DAOs
{
    public class ProfessorDAO : BaseDAO<Professor>
    {
        protected override string GetSqlDelete() =>
            "DELETE FROM PROFESSORES WHERE ID = @ID";

        protected override string GetSqlInsert() =>
            "INSERT INTO PROFESSORES (NOME, MATRICULA, EMAIL) VALUES (@NOME, @MATRICULA, @EMAIL)";

        protected override string GetSqlSelect() =>
            "SELECT * FROM PROFESSORES ORDER BY NOME";

        protected override string GetSqlSelectId(int id) =>
            "SELECT * FROM PROFESSORES WHERE ID = " + id;

        protected override string GetSqlSelectNome(string nome) =>
           "SELECT * FROM PROFESSORES WHERE NOME LIKE '" + nome + "%'";

        protected override string GetSqlUpdate() =>
            "UPDATE PROFESSORES SET NOME=@NOME, MATRICULA=@MATRICULA, EMAIL=@EMAIL WHERE ID = @ID";
        protected string GetSqlMatricula(string matricula) =>
            "SELECT * FROM PROFESSORES WHERE MATRICULA =" + matricula;

        protected override void AdicionarParametros(SqlCommand cmd, Professor obj)
        {
            cmd.Parameters.AddWithValue("@NOME", obj.Nome);
            cmd.Parameters.AddWithValue("@MATRICULA", obj.Matricula);
            cmd.Parameters.AddWithValue("@EMAIL", obj.Email);
        }

        protected override Professor GetObjeto(DataRow reg)
        {
            var obj = new Professor();

            obj.Id = Convert.ToInt32(reg["ID"]);
            obj.Nome = reg["NOME"].ToString();
            obj.Matricula = reg["MATRICULA"].ToString();
            obj.Email = reg["EMAIL"].ToString();

            return obj;
        }

        protected override string GetSqlSelectMatricula(string matricula)
        {
            throw new NotImplementedException();
        }
    }
}
