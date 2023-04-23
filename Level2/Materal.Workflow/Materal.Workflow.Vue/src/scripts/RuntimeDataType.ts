export class RuntimeDataType {
    public Properties: RuntimeDataPropertyInfo[] = [];
    public GetJsonString(): string {
        const data: { [key: string]: string | number } = {};
        for (const property of this.Properties) {
            data[property.Name] = property.Type === "String" ? "" : 0;
        }
        return JSON.stringify(data);
    }
}
export class RuntimeDataPropertyInfo {
    public Name: string = "";
    public Type: "String" | "Number" = "String";
    public Description: string = "";
}
export let NowRuntimeDataType: RuntimeDataType = new RuntimeDataType();