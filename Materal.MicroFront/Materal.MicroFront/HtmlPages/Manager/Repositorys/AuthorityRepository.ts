namespace Materal.MicroFront.Repositories {
    export class AuthorityRepository {
        Login(data: any, success: Function) {
            Scripts.Common.sendPost("Authority/Login", data, success);
        }
        Logout(token: string, success: Function) {
            Scripts.Common.sendGet(`Authority/Logout?token=${token}`, null, success);
        }
    }
}