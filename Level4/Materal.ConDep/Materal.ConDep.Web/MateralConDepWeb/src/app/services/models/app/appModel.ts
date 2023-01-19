/**
 * 应用信息
 */
export interface AppModel {
    /**
     * 唯一标识
     */
    ID: string;
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
