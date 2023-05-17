import { DataTypeComponentModel } from "./DataTypeComponentModel";

export class SelectComponentModel extends DataTypeComponentModel {
    public Props = {
        Required: false,
        Readonly: false,
        Disabled: false,
        CanNull: false
    };
    constructor(id: string) {
        super(id, "select");
    }
}