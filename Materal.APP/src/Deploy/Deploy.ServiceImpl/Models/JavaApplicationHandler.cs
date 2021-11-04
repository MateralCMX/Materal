//using Deploy.Enums;
//using System;
//using System.IO;

//namespace Deploy.ServiceImpl.Models
//{
//    public class JavaApplicationHandler : ExeApplicationHandler
//    {
//        public override void StartApplication(ApplicationRuntimeModel applicationRuntime)
//        {
//            string startArgs = GetStartArgs(applicationRuntime);
//            StartApplication(applicationRuntime, "java.exe", startArgs);
//        }

//        public override void StopApplication(ApplicationRuntimeModel applicationRuntime)
//        {
//            StopApplication(applicationRuntime, ApplicationTypeEnum.Java);
//        }

//        #region 私有方法
//        /// <summary>
//        /// 获得启动参数
//        /// </summary>
//        /// <param name="model"></param>
//        /// <returns></returns>
//        private string GetStartArgs(ApplicationRuntimeModel model)
//        {
//            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Application", model.Path, $"{model.MainModule}");
//            string result = string.IsNullOrEmpty(model.RunParams) ? $"{path}" : $"{path} {model.RunParams}";
//            return result;
//        }

//        #endregion
//    }
//}
