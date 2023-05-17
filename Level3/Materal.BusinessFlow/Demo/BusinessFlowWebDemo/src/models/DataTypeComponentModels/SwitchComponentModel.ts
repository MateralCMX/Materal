import { DataTypeComponentModel } from "./DataTypeComponentModel";

export class SwitchComponentModel extends DataTypeComponentModel {
    public Props = {
        Readonly: false,
        Disabled: false
    };
    constructor(id: string) {
        super(id, "switch");
    }
}