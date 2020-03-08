import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Injectable({
    providedIn: 'root'
})
export class FormGroupCommon {
    public canValid(validateForm: FormGroup): boolean {
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
