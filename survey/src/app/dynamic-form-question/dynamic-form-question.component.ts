import { Component, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';

import { QuestionBase } from '../questions/question-base';
import { QuestionApiBase } from '../questions/question-api-base';

@Component({
  selector: 'app-question',
  templateUrl: './dynamic-form-question.component.html',
  styleUrls: ['./dynamic-form-question.component.css'],
})
export class DynamicFormQuestionComponent {
  @Input() question!: QuestionApiBase<string>;
  @Input() form!: FormGroup;

  get isValid() {
    return this.form.controls[this.question.key].valid;
  }
}
