﻿namespace Materal.WebAPITest.Services
{
    public interface ITestService
    {
        string SayHello();
        Task<string> SayHelloAsync();
    }
}
