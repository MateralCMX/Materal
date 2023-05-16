import { DataTypeComponentModel } from "./DataTypeComponentModel";

export class StringDataTypeComponentTextareModel extends DataTypeComponentModel {
    public Props = {
        Required: false,
        Rows: 4
    };
    constructor(id: string) {
        super(id, "a-textarea");
    }
}