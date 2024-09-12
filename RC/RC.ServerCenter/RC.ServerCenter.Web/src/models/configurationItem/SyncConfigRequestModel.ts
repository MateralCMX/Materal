export default interface SyncConfigRequestModel {
    /**
     * 项目ID
     */
    ProjectID: string;
    /**
     * 配置项ID
     */
    NamespaceIDs:string[];
    /**
     * 模式
     */
    Mode: number;
    /**
     * 目标环境组
     */
    TargetEnvironments: string[];
}