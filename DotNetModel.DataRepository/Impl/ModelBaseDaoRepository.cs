using DotNetModel.DataAccessObject.BaseRepository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetModel.DataRepository.Impl
{
    public abstract class ModelBaseDaoRepository<T> : BaseDaoRepository<T>
    {
        public ModelBaseDaoRepository(IConfiguration Configuration) :base(Configuration, "DBConecction")
        {

        }
    }
}
