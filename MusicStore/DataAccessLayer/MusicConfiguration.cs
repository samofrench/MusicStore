using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.SqlServer;
using MusicStore.DataAccessLayer;

namespace MusicStore.DataAccessLayer
{
    public class MusicConfiguration : DbConfiguration
    {
        public MusicConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
            DbInterception.Add(new MusicInterceptorTransientErrors());
            DbInterception.Add(new MusicInterceptorLogging());
        }
    }
}