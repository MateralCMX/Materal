using System;
using System.Collections.Generic;
using System.Text;
using Materal.TTA.Common;

namespace Materal.TTA.MySqlRepository
{
    public class MySqlRepositoryImpl<T, TPrimaryKeyType>: EntityFrameworkRepositoryImpl<T,TPrimaryKeyType>, IMySqlRepository<T,TPrimaryKeyType>
    {
    }
}
