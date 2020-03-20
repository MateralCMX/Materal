export interface CopyNamespaceModel {
    /**
     * 源服务名称
     */
    SourceConfigServerName: string;
    /**
     * 目标配置服务名称
     */
    TargetConfigServerNames: string[];
    /**
     * 命名空间唯一标识
     */
    NamespaceID: string;
}
