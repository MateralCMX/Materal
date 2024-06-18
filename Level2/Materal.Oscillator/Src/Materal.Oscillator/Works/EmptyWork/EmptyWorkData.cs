﻿using Materal.Oscillator.Abstractions.Works;

namespace Materal.Oscillator.Works.EmptyWork
{
    /// <summary>
    /// 空白任务数据
    /// </summary>
    public class EmptyWorkData() : WorkData<EmptyWork>("新空白任务"), IWorkData
    {
    }
}