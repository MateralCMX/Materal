export abstract class DataTypeComponentModel {
    public ID: string = "";
    public Tag: string = "";
    constructor(id: string, tag: string) {
        this.ID = id;
        this.Tag = tag;
    }
}