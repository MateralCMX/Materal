import { DataTypeComponentModel } from "./DataTypeComponentModel";

export class StringDataTypeComponentModel extends DataTypeComponentModel {
    public Props = {
        Required: false
    };
    constructor(id: string) {
        super(id, "a-input");
    }
}