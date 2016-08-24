using System.Data;

namespace DN.SampleWithAdoNet.DomainModel
{
    public interface IConnectionFactory
    {
        IDbConnection Create();
    }
}
