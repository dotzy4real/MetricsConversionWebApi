using MetricsConversion.Domain.Caching;
using System;

namespace MetricsConversion.Domain.Common
{

    public abstract partial class BaseEntityExt
    {
        public virtual int Id { get; set; }
    }



    /// <summary>
    /// Base class for entities
    /// </summary>
    public abstract partial class BaseEntity
    {
        /// <summary>
        /// Gets or sets the entity identifier
        /// </summary>
        public virtual int Id { get; set; }

    }


    public abstract class Entity : BaseEntity, IEntity
    {
        /// <summary>
        /// Get key for caching the entity
        /// </summary>
        public string EntityCacheKey => GetEntityCacheKey(GetType(), Id);

        /// <summary>
        /// Get key for caching the entity
        /// </summary>
        /// <param name="entityType">Entity type</param>
        /// <param name="id">Entity id</param>
        /// <returns>Key for caching the entity</returns>
        public static string GetEntityCacheKey(Type entityType, object id)
        {
            return string.Format(CachingDefaults.DefaultEntityCacheKey, entityType.Name.ToLower(), id);
        }
    }
}
