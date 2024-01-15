﻿using Materal.Extensions;

namespace MMB.Demo.Application.Services
{
    /// <summary>
    /// 密码管理器
    /// </summary>
    public static class PasswordManager
    {
        /// <summary>
        /// 加密密码
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static string EncodePassword(string password) => $"Materal{password}Materal".ToMd5_32Encode();
    }
}
