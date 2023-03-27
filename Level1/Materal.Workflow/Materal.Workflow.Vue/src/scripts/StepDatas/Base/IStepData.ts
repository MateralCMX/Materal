import { BuildDataType } from "../../BuildDataType";

export interface IStepData {
    /**
     * 节点数据类型
     */
    StepDataType: string;
    /**
     * 名称
     */
    Name: string;
    /**
     * 描述
     */
    Description?: string;
    /**
     * 构建数据
     */
    BuildDatas: BuildDataType;
}