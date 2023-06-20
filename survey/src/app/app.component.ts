import { Component, OnInit } from '@angular/core';

import { QuestionApiBase } from './questions/question-api-base';
import { Observable } from 'rxjs';

import { QuestionApiService } from './questions/questionApi.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [QuestionApiService],
})
export class AppComponent {
  questions$: Observable<QuestionApiBase<any>[]>;

  constructor(service: QuestionApiService) {
    this.questions$ = service.getQuestionOptions();
  }
}
