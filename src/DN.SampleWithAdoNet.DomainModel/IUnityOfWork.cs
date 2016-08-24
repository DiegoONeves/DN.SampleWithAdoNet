using System;

namespace DN.SampleWithAdoNet.DomainModel
{
    public interface IUnityOfWork: IDisposable
    {
        void SaveChanges();
    }
}
