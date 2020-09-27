
namespace FroniusReader.DataTypes
{
    using OxyPlot;
    using System.Collections.Generic;

    public class Channel
    {
        public Channel(string name, string unit, IReadOnlyList<DataPoint> points)
        {
            Name = name;
            Unit = unit;
            Points = points;
        }

        public string Name { get;}
        public string Unit { get; }
        public IReadOnlyList<DataPoint> Points { get;}
    }
}
