using System.Collections.Generic;

namespace DotNetModel.DataAccessObject.BaseInterface
{
    public interface IBaseDaoInterface<T>
    {
        int Insert(T model, out string mensagem, string strConexao = null);

        void Update(T model, out string mensagem, string strConexao = null);

        void Delete(object obj, out string mensagem, string strConexao = null);

        void DeleteList(object obj, out string mensagem, string strConexao = null);

        T FindById(object parametros, string strConexao = null);

        IEnumerable<T> FindBy(object parametros, string strConexao = null);

        IEnumerable<T> FindAll(string strConexao = null);
    }
}
