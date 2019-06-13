﻿using System.Collections.Generic;
using System.Linq;

namespace Common
{
    public class TreeHelper
    {
        /// <summary>
        /// 获取树形列表
        /// </summary>
        /// <param name="allAPIAuthorities">所有API权限</param>
        /// <param name="parentID">父级唯一标识</param>
        /// <returns></returns>
        public static List<T1> GetTreeList<T1, T2, T3>(List<T2> allAPIAuthorities, T3? parentID = null) where T1 : ITreeModel<T1, T3>, new() where T2 : ITreeDomain<T3> where T3 : struct
        {
            var result = new List<T1>();
            List<T2> apiAuthorities = allAPIAuthorities.Where(m => m.ParentID.Equals(parentID)).ToList();
            foreach (T2 apiAuthority in apiAuthorities)
            {
                result.Add(new T1
                {
                    ID = apiAuthority.ID,
                    Name = apiAuthority.Name,
                    Child = GetTreeList<T1, T2, T3>(allAPIAuthorities, apiAuthority.ID)
                });
            }
            return result;
        }
    }
}
