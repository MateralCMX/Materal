import { DataTypeComponentModel } from "./DataTypeComponentModel";

export class InputNumberComponentModel extends DataTypeComponentModel {
    public Props = {
        Required: false,
        Readonly: false,
        Disabled: false,
        Min: 0,
        Max: 100
    };
    constructor(id: string) {
        super(id, "inputNumber");
    }
}