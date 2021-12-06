using DotNetModel.DataEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetModel.Business
{
    public interface IApplicationBusiness
    {
        IEnumerable<Application> Find(string url, string pathLocal, bool? debuggingMode);
        Application GetById(int id);
        Application Add(Application body);
        Application Alter(int id, Application body);
        void Delete(int id);
    }
}
