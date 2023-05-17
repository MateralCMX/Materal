import { DataTypeComponentModel } from "./DataTypeComponentModel";

export class DatePickerComponentModel extends DataTypeComponentModel {
    public Props = {
        Required: false,
        Readonly: false,
        Disabled: false,
        Type: 'Date'
    };
    constructor(id: string, type: "Date" | "Time" | "DateTime") {
        super(id, "datePicker");
        this.Props.Type = type;
    }
}