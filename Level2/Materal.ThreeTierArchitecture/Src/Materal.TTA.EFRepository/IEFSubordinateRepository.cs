﻿using Materal.TTA.Common;

namespace Materal.TTA.EFRepository
{
    /// <summary>
    /// EF读写分离仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TPrimaryKeyType"></typeparam>
    public interface IEFSubordinateRepository<T, in TPrimaryKeyType> : ISubordinateRepository<T, TPrimaryKeyType>
        where T : class, IEntity<TPrimaryKeyType>
        where TPrimaryKeyType : struct
    {
    }
}
