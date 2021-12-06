using DotNetModel.DataEntity;
using DotNetModel.DataRepository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;

namespace DotNetModel.Business.Impl
{
    public class ApplicationBusiness : IApplicationBusiness
    {
        private readonly IApplicationDao iApplicationDao;
        public ApplicationBusiness(IApplicationDao iApplicationDao)
        {
            this.iApplicationDao = iApplicationDao;
        }

        public Application Add(Application body)
        {
            Verity(body);

            bool existsItem = body.Id > 0 && GetById(body.Id) != null;
            if (existsItem)
            {
                throw new Exception("There is already this item!");
            }
            
            int id = iApplicationDao.Insert(body, out string message);
            if (!string.IsNullOrEmpty(message))
            {
                throw new Exception(message);
            }

            return GetById(id);
        }

        private void Verity(Application body)
        {
            if (body == null)
            {
                throw new ArgumentNullException("No one valid item!");
            }

            if (string.IsNullOrEmpty(body.PathLocal))
            {
                throw new Exception("No local path informed!");
            }

            if (string.IsNullOrEmpty(body.Url))
            {
                throw new Exception("No local url informed!");
            }

        }

        public Application Alter(int id, Application body)
        {
            Verity(body);

            ExistsItem(id);

            body.Id = id;

            iApplicationDao.Update(body, out string message);

            if (!string.IsNullOrEmpty(message))
            {
                throw new Exception(message);
            }

            return GetById(id);
        }

        private void ExistsItem(int id)
        {
            bool existsItem = id > 0 && GetById(id) != null;
            if (!existsItem)
            {
                throw new Exception("The item does not exists!");
            }
        }

        public void Delete(int id)
        {
            ExistsItem(id);
            iApplicationDao.Delete(id, out string message);
            if (!string.IsNullOrEmpty(message))
            {
                throw new Exception(message);
            }
        }

        public IEnumerable<Application> Find(string url, string pathLocal, bool? debuggingMode)
        {

            if (string.IsNullOrEmpty(url) && string.IsNullOrEmpty(pathLocal) && !debuggingMode.HasValue)
            {
                return iApplicationDao.FindAll();
            }
            else if (string.IsNullOrEmpty(pathLocal) && !debuggingMode.HasValue)
            {
                return iApplicationDao.FindBy (new { Url = url });
            }
            else if (string.IsNullOrEmpty(url) && !debuggingMode.HasValue)
            {
                return iApplicationDao.FindBy( new { PathLocal = pathLocal });
            }
            else if (string.IsNullOrEmpty(url) && string.IsNullOrEmpty(pathLocal))
            {
                return iApplicationDao.FindBy (new { DebuggingMode = debuggingMode.Value });
            }
            else if (!debuggingMode.HasValue)
            {
                return iApplicationDao.FindBy (new { Url = url, PathLocal = pathLocal });
            }
            else if (string.IsNullOrEmpty(url))
            {
                return iApplicationDao.FindBy( new { PathLocal = pathLocal, DebuggingMode = debuggingMode });
            }
            else if (string.IsNullOrEmpty(pathLocal))
            {
                return iApplicationDao.FindBy (new { Url = url, DebuggingMode = debuggingMode.Value });
            }

            return iApplicationDao.FindBy(new { Url = url, PathLocal = pathLocal, DebuggingMode = debuggingMode.Value });
        }

        public Application GetById(int id) => iApplicationDao.FindById(id);
    }
}
