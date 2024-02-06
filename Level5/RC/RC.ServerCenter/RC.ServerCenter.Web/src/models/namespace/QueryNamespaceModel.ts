export default interface QueryNamespaceModel {
    /**
     * 名称
     */
    Name: string;
    /**
     * 描述
     */
    Description: string;
    /**
     * 项目唯一标识
     */
    ProjectID: string;
    /**
     * 页码
     */
    PageIndex: number;
    /**
     * 每页显示数量
     */
    PageSize: number;
}