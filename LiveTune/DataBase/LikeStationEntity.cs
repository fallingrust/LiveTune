using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text;
using System;
using Tenray.ZoneTree.Serializers;
using LiveTune.Models;

namespace LiveTune.DataBase
{
    public class LikeStationEntity
    {
        public LikeStationEntity()
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
        public bool IsLikeStation { get; set; }
        public static LikeStationEntity Parse(StationListItem item)
        {
            return new LikeStationEntity()
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
    [JsonSerializable(typeof(LikeStationEntity))]
    [JsonSerializable(typeof(IEnumerable<LikeStationEntity>))]
    public partial class LikeStationEntitySerializerContext : JsonSerializerContext
    {
    }

    public class LikeStationEntitySerializer : ISerializer<LikeStationEntity>
    {
        public LikeStationEntity Deserialize(Memory<byte> bytes)
        {
            var json = Encoding.UTF8.GetString(bytes.ToArray());
            return JsonSerializer.Deserialize(json, LikeStationEntitySerializerContext.Default.LikeStationEntity);
        }

        public Memory<byte> Serialize(in LikeStationEntity entry)
        {
            var json = JsonSerializer.Serialize(entry, LikeStationEntitySerializerContext.Default.LikeStationEntity);
            return Encoding.UTF8.GetBytes(json);
        }
    }
}
