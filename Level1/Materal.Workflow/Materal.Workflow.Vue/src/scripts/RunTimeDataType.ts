export class RunTimeDataType {
    public Items: RunTimeDataTypeItem[] = [];
    public GetJsonString(): string {
        return JSON.stringify(this.Items);
    }
}
export class RunTimeDataTypeItem {
    public Name: string = "";
    public Value: "String" | "Number" = "String";
}