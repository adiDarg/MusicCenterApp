using System.Data;
using MusicCenterModels;

namespace MusicCenterFactories;

public class InstrumentCreator: IModelCreator<Instrument>
{
    public Instrument CreateModel(IDataReader src)
    {
        Instrument model = new Instrument()
        {
            Id = Convert.ToString(src["InstrumentID"]),
            Name = Convert.ToString(src["InstrumentName"]),
        };
        return model;
    }
}