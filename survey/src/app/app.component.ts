import { Component } from '@angular/core';
import { QuestionApiBase } from './questions/question-api-base';
import { QuestionApiService } from './questions/questionApi.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [QuestionApiService],
})
export class AppComponent {
  questions: QuestionApiBase<string>[] = [];
  loading: boolean = true;

  constructor(private service: QuestionApiService) {}

  ngOnInit() {
    console.log(this.service.getQuestionOptions());
    this.service.getQuestionOptions().then((response) => {
      console.log(response);
      this.questions = response;
      this.loading = false;
    });
  }
}
