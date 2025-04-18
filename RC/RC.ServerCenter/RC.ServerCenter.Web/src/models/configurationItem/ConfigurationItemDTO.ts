export default interface ConfigurationItemDTO {
    /**
     * 唯一标识
     */
    ID: string;
    /**
     * 键
     */
    Key: string;
    /**
     * 值
     */
    Value: string;
    /**
     * 描述
     */
    Description: string;
    /**
     * 项目唯一标识
     */
    ProjectID: string;
    /**
     * 项目名称
     */
    ProjectName: string;
    /**
     * 命名空间唯一标识
     */
    NamespaceID: string;
    /**
     * 命名空间名称
     */
    NamespaceName: string;
}