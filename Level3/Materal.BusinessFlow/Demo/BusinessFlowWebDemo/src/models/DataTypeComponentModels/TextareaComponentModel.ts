import { DataTypeComponentModel } from "./DataTypeComponentModel";

export class TextareaComponentModel extends DataTypeComponentModel {
    public Props = {
        Required: false,
        Rows: 4
    };
    constructor(id: string) {
        super(id, "textarea");
    }
}