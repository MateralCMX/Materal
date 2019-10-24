namespace Materal.MicroFront.Repositories {
    export class ServiceRepository {
        GetAppList(success: Function) {
            Scripts.Common.sendPost("Service/GetServiceModel", null, success);
        }
    }
}