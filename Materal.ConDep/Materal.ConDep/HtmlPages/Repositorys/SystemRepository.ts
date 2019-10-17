namespace Materal.ConDep.Repositories {
    export class SystemRepository {
        GetSystemName(success:Function) {
            Scripts.Common.sendGet("System/GetSystemName", null, success);
        }
        GetSystemVersion(success:Function) {
            Scripts.Common.sendGet("System/GetSystemVersion", null, success);
        }
    }
}