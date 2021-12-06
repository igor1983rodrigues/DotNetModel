using DotNetModel.DataAccessObject.BaseRepository;
using DotNetModel.DataEntity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetModel.DataRepository.Impl
{
    public class ApplicationDao : ModelBaseDaoRepository<Application>, IApplicationDao
    {
        public ApplicationDao(IConfiguration configuration):base(configuration)
        {
        }
    }
}
