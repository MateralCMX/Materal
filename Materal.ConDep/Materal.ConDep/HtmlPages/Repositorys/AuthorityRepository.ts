namespace Materal.ConDep.Repositorys {
    export class AuthorityRepository {
        Login(data:any,success:Function) {
            Scripts.Common.sendPost("Authority/Login", data, success);
        }
    }
}