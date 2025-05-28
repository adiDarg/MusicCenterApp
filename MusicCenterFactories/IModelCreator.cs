using System.Data;

namespace MusicCenterFactories;

public interface IModelCreator<T>
{
    T CreateModel(IDataReader src);
}