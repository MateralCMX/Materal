/**
 * 节点信息
 */
export class StepInfoModel {
    Name: string;
    Style: string;
    Component: any;
    constructor(name: string, style: string, component: any) {
        this.Name = name;
        this.Style = style;
        this.Component = component;
    }
}