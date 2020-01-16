import { FormGroup } from '@angular/forms';

export class FormGroupCommon {
    static canValid(validateForm: FormGroup): boolean {
        for (const key in validateForm.controls) {
            if (validateForm.controls.hasOwnProperty(key)) {
                const element = validateForm.controls[key];
                element.markAsDirty();
                element.updateValueAndValidity();
            }
        }
        return validateForm.valid;
    }
}
