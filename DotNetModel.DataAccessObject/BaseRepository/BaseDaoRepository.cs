using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using DotNetModel.DataAccessObject.BaseInterface;

namespace DotNetModel.DataAccessObject.BaseRepository
{
    public class BaseDaoRepository<T> : IBaseDaoInterface<T>
    {
        private readonly string strConnection;

        public IConfiguration Configuration { get; }

        public BaseDaoRepository(IConfiguration configuration, string strConnection)
        {
            this.Configuration = configuration;
            this.strConnection = strConnection;
        }

        protected DbConnection GetConnection(string strConnection)
        {
            //         string conString = Microsoft
            //.Extensions
            //.Configuration
            //.ConfigurationExtensions
            //.GetConnectionString(this.Configuration, "DefaultConnection");
            string strConnectionString = Configuration.GetConnectionString(strConnection ?? this.strConnection);
            //SimpleCRUD.SetDialect(SimpleCRUD.Dialect.SQLServer);
            return new SqlConnection(strConnectionString);
        }

        public virtual void Update(T model, out string message, string strConnection = null)
        {
            using (var conn = GetConnection(strConnection))
            {
                try
                {
                    conn.Open();
                    int id = conn.Update(model);
                    if (id == 0) throw new Exception("Erro ao atualizar os dados. Entre em contato com o administrador.");
                    message = null;
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public virtual void Delete(object obj, out string message, string strConnection = null)
        {
            using (var conn = GetConnection(strConnection))
            {
                try
                {
                    conn.Open();
                    int id = conn.Delete<T>(obj);
                    if (id == 0) throw new Exception("Erro ao excluir os dados. Entre em contato com o administrador.");
                    message = null;
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public virtual void DeleteList(object obj, out string message, string strConnection = null)
        {
            using (var conn = GetConnection(strConnection))
            {
                try
                {
                    conn.Open();
                    int id = conn.DeleteList<T>(obj);
                    if (id == 0) throw new Exception("Erro ao excluir os dados. Entre em contato com o administrador.");
                    message = null;
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public virtual int Insert(T model, out string message, string strConnection = null)
        {
            using (var conn = GetConnection(strConnection))
            {
                try
                {
                    conn.Open();
                    message = null;
                    int id = conn.Insert(model) ?? 0;
                    if (id == 0) throw new Exception("Erro ao inserir os dados. Entre em contato com o administrador.");
                    return id;
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                    return 0;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public virtual IEnumerable<T> FindBy(object parametros, string strConnection = null)
        {
            using (var conn = GetConnection(strConnection))
            {
                try
                {
                    conn.Open();
                    return conn.GetList<T>(parametros);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public virtual T FindById(object parametros, string strConnection = null)
        {
            using (var conn = GetConnection(strConnection))
            {
                try
                {
                    conn.Open();
                    return conn.Get<T>(parametros);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public virtual IEnumerable<T> FindAll(string strConnection = null)
        {
            using (var conn = GetConnection(strConnection))
            {
                try
                {
                    conn.Open();
                    return conn.GetList<T>();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public virtual void ExecuteCommand(string sqlString, object objectParams, out string message, string strConnection = null)
        {
            using (var conn = GetConnection(strConnection))
            {
                //IDbTransaction transaction = null;
                try
                {
                    conn.Open();
                    //transaction = conn.BeginTransaction();

                    conn.Execute(sqlString, objectParams, commandType: CommandType.Text);
                    //transaction.Commit();

                    message = null;
                }
                catch (Exception ex)
                {
                    //transaction.Rollback();
                    message = ex.Message;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public virtual IEnumerable<TCampo> ExecuteQuery<TCampo>(string sqlString, object objectParams, CommandType type, string strConnection = null)
        {
            using (var conn = GetConnection(strConnection))
            {
                try
                {
                    return conn.Query<TCampo>(sqlString, objectParams, commandType: type);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public virtual IEnumerable<T> ExecuteQuery(string sqlString, object objectParams, CommandType type, string strConnection = null)
        {
            using (var conn = GetConnection(strConnection))
            {
                try
                {
                    return conn.Query<T>(sqlString, objectParams, commandType: type);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public virtual IEnumerable<T> ExecuteQuery(object objectParams, string strConnection = null)
        {
            using (var conn = GetConnection(strConnection))
            {
                try
                {
                    return conn.GetList<T>(objectParams);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public virtual IEnumerable<TReturn> ExecuteQuery<T1, T2, TReturn>(string sqlString, object objectParams, Func<T1, T2, TReturn> map, string[] splitOn, CommandType type = CommandType.Text, string strConnection = null)
        {
            using (var conn = GetConnection(strConnection))
            {
                try
                {
                    return conn.Query<T1, T2, TReturn>(sqlString, map, objectParams, null, true, string.Join(", ", splitOn), null, type);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public virtual IEnumerable<TReturn> ExecuteQuery<T1, T2, T3, TReturn>(string sqlString, object objectParams, Func<T1, T2, T3, TReturn> map, string[] splitOn, CommandType type = CommandType.Text, string strConnection = null)
        {
            using (var conn = GetConnection(strConnection))
            {
                try
                {
                    return conn.Query(sqlString, map, objectParams, null, true, string.Join(", ", splitOn), null, type);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public virtual IEnumerable<TReturn> ExecuteQuery<T1, T2, T3, T4, TReturn>(string sqlString, object objectParams, Func<T1, T2, T3, T4, TReturn> map, string[] splitOn, CommandType type = CommandType.Text, string strConnection = null)
        {
            using (var conn = GetConnection(strConnection))
            {
                try
                {
                    return conn.Query(sqlString, map, objectParams, null, true, string.Join(", ", splitOn), null, type);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public virtual IEnumerable<TReturn> ExecuteQuery<T1, T2, T3, T4, T5, TReturn>(string sqlString, object objectParams, Func<T1, T2, T3, T4, T5, TReturn> map, string[] splitOn, CommandType type = CommandType.Text, string strConnection = null)
        {
            using (var conn = GetConnection(strConnection))
            {
                try
                {
                    return conn.Query(sqlString, map, objectParams, null, true, string.Join(", ", splitOn), null, type);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public virtual IEnumerable<TReturn> ExecuteQuery<T1, T2, T3, T4, T5, T6, TReturn>(string sqlString, object objectParams, Func<T1, T2, T3, T4, T5, T6, TReturn> map, string[] splitOn, CommandType type = CommandType.Text, string strConnection = null)
        {
            using (var conn = GetConnection(strConnection))
            {
                try
                {
                    return conn.Query(sqlString, map, objectParams, null, true, string.Join(", ", splitOn), null, type);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public virtual IEnumerable<TReturn> ExecuteQuery<T1, T2, T3, T4, T5, T6, T7, TReturn>(string sqlString, object objectParams, Func<T1, T2, T3, T4, T5, T6, T7, TReturn> map, string[] splitOn, CommandType type = CommandType.Text, string strConnection = null)
        {
            using (var conn = GetConnection(strConnection))
            {
                try
                {
                    return conn.Query(sqlString, map, objectParams, null, true, string.Join(", ", splitOn), null, type);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
