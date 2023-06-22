import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';

import { QuestionBase } from '../questions/question-base';
import { QuestionApiBase } from '../questions/question-api-base';
import { QuestionControlService } from '../questions/question-control.service';

import { AnswersService } from '../answers/answers.service';
import { KeyValue, KeyValuePipe } from '@angular/common';

interface AnswersLayout {
  label: string;
  qId: number;
  keys: { key: string; selections: number }[];
}

@Component({
  selector: 'app-dynamic-form',
  templateUrl: './dynamic-form.component.html',
  providers: [QuestionControlService, AnswersService],
})
export class DynamicFormComponent implements OnInit {
  @Input() questions: QuestionApiBase<string>[] | null = [];
  form!: FormGroup;
  payLoad = '';
  answers: AnswersLayout[] | undefined;
  answersArray: any[] = [];

  constructor(
    private qcs: QuestionControlService,
    private as: AnswersService
  ) {}

  ngOnInit() {
    this.form = this.qcs.toFormGroup(
      this.questions as QuestionApiBase<string>[]
    );
  }

  onSubmit() {
    this.payLoad = JSON.stringify(this.form.getRawValue());
    console.log(this.form.getRawValue());

    this.as.getAnswers().subscribe((response: any) => {
      console.log(response);
      this.answers = response;
    });

    const answersTemp = Object.entries(this.form.getRawValue());
    let answers: any[] = [];
    answersTemp.forEach((element: any) => {
      console.log(typeof element[1] === 'string');
      if (typeof element[1] === 'string') {
        answers.push(element[1]);
      } else {
        element[1].forEach((element2: any) => {
          answers.push(element2);
        });
      }
    });
    console.log(answers);

    this.as.postAnswers(answers);
  }
}
