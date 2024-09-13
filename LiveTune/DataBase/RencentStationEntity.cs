using LiveTune.Models;
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
        public RencentStationEntity()
        {
            
        }
        public string StationName { get; set; } = string.Empty;
        public string Language { get; set; } = string.Empty;

        public int ClickCount { get; set; }
        public int VoteCount { get; set; }

        public string FaviconUrl { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string StationId { get; set; } = string.Empty;
        public string PlayTime { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        public bool IsLikeStation { get; set; }
        public static RencentStationEntity Parse(StationListItem item)
        {
            return new RencentStationEntity()
            {
                 ClickCount = item.ClickCount,
                 VoteCount = item.VoteCount,
                 StationId = item.StationId,
                 StationName = item.StationName,
                 Language = item.Language,
                 Url = item.Url,
                 Country = item.Country,
                 FaviconUrl = item.FaviconUrl,
                 IsLikeStation = item.IsLike
            };
        }
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
