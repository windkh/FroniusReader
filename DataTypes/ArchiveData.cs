
namespace FroniusReader.DataTypes
{
    using System.Collections.Generic;

    public class ArchiveData
    {
        public ArchiveData(IReadOnlyCollection<Channel> channels)
        {
            Channels = channels;
        }

        public IReadOnlyCollection<Channel> Channels { get; }
    }
}
