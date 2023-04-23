export class WorkflowDataType {
    public Name: string;
    public Value: "String" | "Number";;
}
export const AllWorkflowDataTypes: Array<WorkflowDataType> = [{
    Name: "字符串",
    Value: "String"
}, {
    Name: "数字",
    Value: "Number"
}];