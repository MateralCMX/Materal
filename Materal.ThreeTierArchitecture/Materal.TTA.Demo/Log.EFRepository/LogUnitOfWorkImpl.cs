using Materal.TTA.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Log.EFRepository
{
    public class LogUnitOfWorkImpl : EFUnitOfWorkImpl<LogDbContext>, ILogUnitOfWork
    {
        public LogUnitOfWorkImpl(LogDbContext context) : base(context)
        {

        }
    }
}
