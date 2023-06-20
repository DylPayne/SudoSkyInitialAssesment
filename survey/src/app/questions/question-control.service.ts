import { Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

import { QuestionBase } from './question-base';
import { QuestionApiBase } from './question-api-base';

@Injectable()
export class QuestionControlService {
  toFormGroup(questions: QuestionApiBase<string>[]) {
    const group: any = {};

    questions.forEach((question) => {
      group[question.key] = new FormControl(question.key || '');
    });
    return new FormGroup(group);
  }
}
