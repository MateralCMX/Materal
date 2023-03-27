export class BuildDataType {
    public Properties: BuildDataPropertyInfo[] = [];
    public GetJsonString(): string {
        return JSON.stringify(this.Properties);
    }
    public ToDictionary(): { [key: string]: string | number; } {
        let result: { [key: string]: string | number; } = {};
        for (let i = 0; i < this.Properties.length; i++) {
            const element = this.Properties[i];
            result[element.Name] = element.Value;
        }
        return result;
    }
    public InitByDictionary(value: { [key: string]: string | number; }) {
        for (const key in value) {
            if (!Object.prototype.hasOwnProperty.call(value, key)) continue;
            const element: string | number = value[key];
            this.Properties.push({ Name: key, Type: typeof element === "string" ? "String" : "Number", Value: element });
        }
    }
}
export class BuildDataPropertyInfo {
    public Name: string = "";
    public Type: "String" | "Number" = "String";
    public Value: string | number = "";
}