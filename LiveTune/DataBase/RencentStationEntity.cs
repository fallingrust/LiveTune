using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Tenray.ZoneTree.Serializers;

namespace LiveTune.DataBase
{

    public class RencentStationEntity
    {       
        public int Id { get; set; }
        public string StationName { get; set; } = string.Empty;
    }


    [JsonSourceGenerationOptions(WriteIndented = true)]
    [JsonSerializable(typeof(RencentStationEntity))]
    [JsonSerializable(typeof(IEnumerable<RencentStationEntity>))]
    public partial class RencentStationEntitySerializerContext : JsonSerializerContext
    {
    }

    public class RencentStationEntitySerializer : ISerializer<RencentStationEntity>
    {
        public RencentStationEntity Deserialize(Memory<byte> bytes)
        {
            var json = Encoding.UTF8.GetString(bytes.ToArray());
            return JsonSerializer.Deserialize(json, RencentStationEntitySerializerContext.Default.RencentStationEntity);
        }

        public Memory<byte> Serialize(in RencentStationEntity entry)
        {
            var json = JsonSerializer.Serialize(entry, RencentStationEntitySerializerContext.Default.RencentStationEntity);
            return Encoding.UTF8.GetBytes(json);
        }
    }
}
