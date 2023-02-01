/**
 * 应用列表
 */
export interface AppListModel {
    /**
     * 唯一标识
     */
    ID: string;
    /**
     * APP状态
     */
    AppStatus: number;
    /**
     * APP状态文本
     */
    AppStatusText: string;
    /**
     * 名称
     */
    Name: string;
    /**
     * 路径
     */
    AppPath: string;
    /**
     * 主模组名称
     */
    MainModuleName: string;
    /**
     * 运行参数
     */
    Parameters: string;
}
