using System;
using System.IO;
using Tenray.ZoneTree.Serializers;
using Tenray.ZoneTree;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiveTune.DataBase
{
    public class DbCtx
    {
        public static readonly string CacheDir = AppDomain.CurrentDomain.BaseDirectory;

        public static readonly List<RencentStationEntity> RencentStations = [];
        public static readonly object RencentStationsMutex = new();
        static DbCtx()
        {
            if (!Directory.Exists(CacheDir)) Directory.CreateDirectory(CacheDir);
            Task.Run(() =>
            {
                using var zoneTree = new ZoneTreeFactory<string, RencentStationEntity>()
                               .SetKeySerializer(new Utf8StringSerializer())
                               .SetValueSerializer(new RencentStationEntitySerializer())
                               .SetDataDirectory(CacheDir)
                               .OpenOrCreate();
                var stations = new List<RencentStationEntity>();
                var iter = zoneTree.CreateIterator();
                while (iter.Next())
                {
                    stations.Add(iter.CurrentValue);
                }
                stations = [.. stations.OrderByDescending(p => p.PlayTime)];
                lock (RencentStationsMutex)
                {
                    RencentStations.Clear();
                    RencentStations.AddRange(stations);
                }
            });
        }

        public static void AddOrUpdateRencentStation(RencentStationEntity rencentStationEntity)
        {
            using var zoneTree = new ZoneTreeFactory<string, RencentStationEntity>()
                                .SetKeySerializer(new Utf8StringSerializer())
                                .SetValueSerializer(new RencentStationEntitySerializer())
                                .SetDataDirectory(CacheDir)
                                .OpenOrCreate();
            zoneTree.Upsert(rencentStationEntity.StationId, rencentStationEntity);
            lock (RencentStationsMutex)
            {
                var rencentStation = RencentStations.FirstOrDefault(p => p.StationId == rencentStationEntity.StationId);
                int index = 0;
                if (rencentStation != null)
                {
                    index = RencentStations.IndexOf(rencentStation);
                    RencentStations.RemoveAt(index);
                }
                RencentStations.Insert(index, rencentStationEntity);

                var rencentStations = RencentStations.OrderByDescending(p => p.PlayTime).ToList();
                RencentStations.Clear();
                RencentStations.AddRange(rencentStations);
            }
           
        }

        public static IEnumerable<RencentStationEntity> GetRencentStations(int offset,int count)
        {
            lock (RencentStationsMutex)
            {
                return RencentStations.Skip(offset).Take(count);
            }
        }
    }
}
