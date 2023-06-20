import { Component, Input } from '@angular/core';

import { AnswersService } from './answers.service';

import { QuestionApiBase } from '../questions/question-api-base';
import { KeyValue } from '@angular/common';

@Component({
  selector: 'app-answers',
  templateUrl: './answers.component.html',
  styleUrls: ['./answers.component.css'],
  providers: [AnswersService],
})
export class AnswersComponent {
  @Input() answers: any;
  test = 'hello';

  constructor(private as: AnswersService) {}

  ngOnInit() {
    console.log(this.answers);
  }
}
