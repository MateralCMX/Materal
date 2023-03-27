export class RuntimeDataType {
    public Properties: RuntimeDataPropertyInfo[] = [];
    public GetJsonString(): string {
        return JSON.stringify(this.Properties);
    }
}
export class RuntimeDataPropertyInfo {
    public Name: string = "";
    public Type: "String" | "Number" = "String";
}
export let NowRuntimeDataType: RuntimeDataType = new RuntimeDataType();