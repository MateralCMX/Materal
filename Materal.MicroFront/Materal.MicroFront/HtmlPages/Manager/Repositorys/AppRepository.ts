namespace Materal.MicroFront.Repositories {
    export class AppRepository {
        GetAppList(success: Function) {
            Scripts.Common.sendGet("App/GetAppList", null, success);
        }
        StartAllApp(success: Function) {
            Scripts.Common.sendGet("App/StartAllApp", null, success);
        }
        RestartAllApp(success: Function) {
            Scripts.Common.sendGet("App/RestartAllApp", null, success);
        }
        StopAllApp(success: Function) {
            Scripts.Common.sendGet("App/StopAllApp", null, success);
        }
        AddApp(data: any, success: Function) {
            Scripts.Common.sendPost("App/AddApp", data, success);
        }
        EditApp(data: any, success: Function) {
            Scripts.Common.sendPost("App/EditApp", data, success);
        }
        DeleteApp(data: string, success: Function) {
            Scripts.Common.sendGet(`App/DeleteApp?id=${data}`, null, success);
        }
        GetAppInfo(data: string, success: Function) {
            Scripts.Common.sendGet(`App/GetAppInfo?id=${data}`, null, success);
        }
        StartApp(data: string, success: Function) {
            Scripts.Common.sendGet(`App/StartApp?id=${data}`, null, success);
        }
        RestartApp(data: string, success: Function) {
            Scripts.Common.sendGet(`App/RestartApp?id=${data}`, null, success);
        }
        StopApp(data: string, success: Function) {
            Scripts.Common.sendGet(`App/StopApp?id=${data}`, null, success);
        }
        GetConsoleList(data: string, success: Function, fail: Function) {
            Scripts.Common.sendGet(`App/GetConsoleList?id=${data}`, null, success, fail);
        }
    }
}