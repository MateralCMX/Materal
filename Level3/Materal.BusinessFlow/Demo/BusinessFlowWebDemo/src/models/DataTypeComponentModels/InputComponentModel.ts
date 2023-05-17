import { DataTypeComponentModel } from "./DataTypeComponentModel";

export class InputComponentModel extends DataTypeComponentModel {
    public Props = {
        Required: false,
        Readonly: false
    };
    constructor(id: string) {
        super(id, "input");
    }
}