using Academico.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Academico.Controllers
{
    public abstract class BaseDAO<T> where T : Professor
    {
        public virtual void Update(T obj)
        {
            ExecutarComando(GetComandoCompleto(GetSqlUpdate(), obj));
        }

        public virtual void Insert(T obj)
        {
            ExecutarComando(GetComandoCompleto(GetSqlInsert(), obj));
        }

        public virtual void Delete(int id)
        {
            ExecutarComando(GetComandoId(GetSqlDelete(), id));
        }

        public virtual T SelectId(int id)
        {
            var cmd = new SqlCommand(GetSqlSelectId(id));
            var tabela = GetDataTable(cmd);

            if (tabela.Rows.Count > 0)
            {
                return GetObjeto(tabela.Rows[0]);
            }

            return null;
        }

        public virtual List<T> SelectNome(string nome)
        {
            var cmd = new SqlCommand(GetSqlSelectNome(nome));
            var tabela = GetDataTable(cmd);

            var lista = new List<T>();

            foreach (DataRow reg in tabela.Rows)
                lista.Add(GetObjeto(reg));

            return lista;
        }
        public virtual List<T> SelectMatricula(string matricula)
        {
            var cmd = new SqlCommand(GetSqlSelectMatricula(matricula));
            var tabela = GetDataTable(cmd);

            var lista = new List<T>();

            foreach (DataRow reg in tabela.Rows)
                lista.Add(GetObjeto(reg));

            return lista;
        }

        public virtual List<T> SelectAll()
        {
            var lista = new List<T>();

            var cmd = new SqlCommand(GetSqlSelect());
            var tabela = GetDataTable(cmd);

            foreach (DataRow reg in tabela.Rows)
                lista.Add(GetObjeto(reg));

            return lista;
        }

        protected abstract T GetObjeto(DataRow reg);
        protected abstract string GetSqlUpdate();
        protected abstract string GetSqlInsert();
        protected abstract string GetSqlDelete();
        protected abstract string GetSqlSelect();
        protected abstract string GetSqlSelectNome(string nome);
        protected abstract string GetSqlSelectMatricula(string matricula);
        protected abstract string GetSqlSelectId(int id);
        protected abstract void AdicionarParametros(SqlCommand cmd, T obj);

        protected void ExecutarComando(SqlCommand cmd)
        {
            using (var conexao = new SqlConnection(GetStringConexao()))
            {
                conexao.Open();

                cmd.Connection = conexao;

                cmd.ExecuteNonQuery();

                conexao.Close();
            }
        }

        protected void ExecutarComandos(IEnumerable<SqlCommand> comandos)
        {
            using (var conexao = new SqlConnection(GetStringConexao()))
            {
                conexao.Open();

                var transacao = conexao.BeginTransaction();

                try
                {
                    foreach (var cmd in comandos)
                    {
                        cmd.Transaction = transacao;
                        cmd.Connection = conexao;
                        cmd.ExecuteNonQuery();
                    }

                    transacao.Commit();
                }
                catch
                {
                    transacao.Rollback();
                    conexao.Close();
                    throw;
                }

                conexao.Close();
            }
        }

        protected SqlCommand GetComandoId(string sql, int id)
        {
            var cmd = new SqlCommand(sql);

            cmd.Parameters.AddWithValue("@ID", id);

            return cmd;
        }

        private DataTable GetDataTable(SqlCommand cmd)
        {
            using (var conexao = new SqlConnection(GetStringConexao()))
            {
                conexao.Open();

                cmd.Connection = conexao;

                var dt = new DataTable();
                var da = new SqlDataAdapter(cmd);

                da.Fill(dt);

                conexao.Close();

                return dt;
            }
        }

        private SqlCommand GetComandoCompleto(string sql, T obj)
        {
            var cmd = GetComandoId(sql, obj.Id);

            AdicionarParametros(cmd, obj);

            return cmd;
        }

        private static string GetStringConexao()
        {
            return @"Integrated Security=SSPI;Persist Security Info=False;" +
                   @"Initial Catalog=academico;Data Source=TR-G27K813";
        }
    }
}
