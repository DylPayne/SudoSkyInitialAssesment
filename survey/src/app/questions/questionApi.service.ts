import { Injectable } from '@angular/core';

import { QuestionApiBase } from './question-api-base';
import { RadioQuestion } from './question-radio';
import { SelectQuestion } from './question-select';

import { HttpClient } from '@angular/common/http';

import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class QuestionApiService {
  constructor(private http: HttpClient) {}

  async getQuestionOptions() {
    console.log(environment);
    const temp = await this.http.get<any>(environment.apiOUrl).toPromise();
    const layout = this.createQuestionForm(temp);
    console.log(layout);
    return layout;
  }

  createQuestionForm(questions: QuestionApiBase<string>[]) {
    var questionsArray: any = [];
    questions.forEach((question: any) => {
      if (question.multiple_choice) {
        questionsArray.push(
          new SelectQuestion({
            value: '',
            key: question.key,
            label: question.label,
            options: question.options.map((option: any) => {
              return {
                key: option.key,
                label: option.label,
                id: option.id,
              };
            }),
            multiple_choice: question.multiple_choice,
            controlType: 'select',
          })
        );
      } else {
        questionsArray.push(
          new RadioQuestion({
            value: '',
            key: question.key,
            label: question.label,
            options: question.options.map((option: any) => {
              return {
                key: option.key,
                label: option.label,
                id: option.id,
              };
            }),
            multiple_choice: question.multiple_choice,
            controlType: 'radio',
          })
        );
      }
    });
    return questionsArray;
  }
}
