import DeployDTO from "./models/server/DeployDTO";
import EnvironmentServerDTO from "./models/server/EnvironmentServerDTO";
import service from "./services/ServerService";
/**
 * 服务管理器
 */
class ServerManagement {
    public deployList: DeployDTO[];
    public environmentServerList: EnvironmentServerDTO[];
    public selectedDeploy: DeployDTO | null;
    public selectedEnvironmentServer: EnvironmentServerDTO | null;
    constructor() {
        this.deployList = [];
        this.environmentServerList = [];
        this.selectedDeploy = null;
        this.selectedEnvironmentServer = null;
    }
    public async initAsync() {
        const deployResult = await service.GetDeployListAsync();
        if (deployResult && deployResult.length > 0) {
            this.deployList = deployResult;
            this.selectedDeploy = this.deployList[0];
        }
        const environmentServerResult = await service.GetEnvironmentServerListAsync();
        if (environmentServerResult && environmentServerResult.length > 0) {
            this.environmentServerList = environmentServerResult;
            this.selectedEnvironmentServer = this.environmentServerList[0];
        }
    }
    public checkDeploy(deploy?: string) {
        if (!deploy) return;
        for (let index = 0; index < this.deployList.length; index++) {
            const element = this.deployList[index];
            if (element.Service != deploy) continue;
            this.selectedDeploy = element;
            return;
        }
    }
    public checkEnvironmentServer(environmentServer?: string) {
        if (!environmentServer) return;
        for (let index = 0; index < this.environmentServerList.length; index++) {
            const element = this.environmentServerList[index];
            if (element.Service != environmentServer) continue;
            this.selectedEnvironmentServer = element;
            return;
        }
    }
}
const serverManagement = new ServerManagement();
await serverManagement.initAsync();
export default serverManagement;