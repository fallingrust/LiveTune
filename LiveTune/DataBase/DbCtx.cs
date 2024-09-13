using System;
using System.IO;
using Tenray.ZoneTree.Serializers;
using Tenray.ZoneTree;
using System.Collections.Generic;
using System.Linq;

namespace LiveTune.DataBase
{
    public class DbCtx
    {
        public static readonly string CacheDir = AppDomain.CurrentDomain.BaseDirectory;
        public static readonly string RencentCacheDir = AppDomain.CurrentDomain.BaseDirectory;
        public static readonly string LikeCacheDir = AppDomain.CurrentDomain.BaseDirectory;
        public static readonly List<RencentStationEntity> RencentStations = [];
        public static readonly List<LikeStationEntity> LikeStations = [];
        public static readonly object RencentStationsMutex = new();
        public static readonly object LikeStationsMutex = new();
        private static readonly IZoneTree<string, RencentStationEntity> RencentCache;
        private static readonly IZoneTree<string, LikeStationEntity> LikeCache;
        static DbCtx()
        {
            RencentCacheDir = Path.Combine(CacheDir, "Cache", "Rencent");
            LikeCacheDir = Path.Combine(CacheDir, "Cache", "Like");
            if (!Directory.Exists(RencentCacheDir)) Directory.CreateDirectory(RencentCacheDir);
            if (!Directory.Exists(LikeCacheDir)) Directory.CreateDirectory(LikeCacheDir);

            RencentCache = new ZoneTreeFactory<string, RencentStationEntity>()
                              .SetKeySerializer(new Utf8StringSerializer())
                              .SetValueSerializer(new RencentStationEntitySerializer())
                              .SetDataDirectory(RencentCacheDir)
                              .OpenOrCreate();
            LikeCache = new ZoneTreeFactory<string, LikeStationEntity>()
                             .SetKeySerializer(new Utf8StringSerializer())
                             .SetValueSerializer(new LikeStationEntitySerializer())
                             .SetDataDirectory(LikeCacheDir)
                             .OpenOrCreate();

            {
                
                var stations = new List<RencentStationEntity>();
                var iter = RencentCache.CreateIterator();
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
            }

            {
               
                var stations = new List<LikeStationEntity>();
                var iter = LikeCache.CreateIterator();
                while (iter.Next())
                {
                    stations.Add(iter.CurrentValue);
                }
                lock (LikeStationsMutex)
                {
                    LikeStations.Clear();
                    LikeStations.AddRange(stations);
                }
            }
           
        }

        public static void AddOrUpdateRencentStation(RencentStationEntity rencentStationEntity)
        {
           
            RencentCache.Upsert(rencentStationEntity.StationId, rencentStationEntity);
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


        public static void AddOrRemoveLikeStation(LikeStationEntity likeStationEntity)
        {
           
            if (LikeCache.ContainsKey(likeStationEntity.StationId))
            {
                LikeCache.ForceDelete(likeStationEntity.StationId);
                lock (LikeStationsMutex)
                {
                    var station = LikeStations.FirstOrDefault(p => p.StationId == likeStationEntity.StationId);
                    if (station != null)
                        LikeStations.Remove(station);
                }
            }
            else
            {
                LikeCache.Upsert(likeStationEntity.StationId, likeStationEntity);
                lock (LikeStationsMutex)
                {
                    LikeStations.Add(likeStationEntity);
                }
            }
        }

        public static IEnumerable<LikeStationEntity> GetLikeStations(int offset, int count)
        {
            lock (LikeStationsMutex)
            {
                return LikeStations.Skip(offset).Take(count);
            }
        }

        public static bool IsLikeStation(string stationId)
        {
            return LikeCache.ContainsKey(stationId);
        }
    }
}
