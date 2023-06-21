import { Injectable } from '@angular/core';

import { QuestionApiBase } from './question-api-base';
import { RadioQuestion } from './question-radio';
import { SelectQuestion } from './question-select';

import { HttpClient } from '@angular/common/http';

import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';

import { of } from 'rxjs';

interface Question {
  value: string;
  id: number;
  qKey: string;
  qLabel: string;
  created_at: string;
  multiple_choice: boolean;
}
interface Option {
  id: number;
  oKey: string;
  oLabel: string;
  qId: number;
}
interface QuestionsWithOptions {
  value: string;
  key: string;
  label: string;
  options: { key: string; label: string; id: number }[];
  multiple_choice: boolean;
  controlType: string;
}

@Injectable({
  providedIn: 'root',
})
export class QuestionApiService {
  private qUrl: string = 'http://localhost:5175/api/Questions';
  private oUrl: string = 'http://localhost:5175/api/QuestionOptions';

  private questionsArray: Question[] = [];
  private optionsArray: Option[] = [];
  private questionsWithOptions: QuestionsWithOptions[] = [];

  // private questionsArrayTemp: QuestionApiBase<string>[] = [];

  constructor(private http: HttpClient) {}

  getQuestionOptions() {
    var questionsArrayTemp: QuestionApiBase<string>[] = [];

    this.http.get<Question[]>(this.qUrl).subscribe((response: Question[]) => {
      this.questionsArray = response;

      this.http.get<Option[]>(this.oUrl).subscribe((response: Option[]) => {
        this.optionsArray = response;

        console.log('hello');

        this.questionsArray.forEach((question: Question) => {
          let tempArray: Option[] = [];

          this.optionsArray.forEach((option: Option) => {
            if (question.id === option.qId) {
              tempArray.push({
                id: option.id,
                oKey: option.oKey,
                oLabel: option.oLabel,
                qId: question.id,
              });
            }
          });

          this.questionsWithOptions.push({
            value: question.value,
            key: question.qKey,
            label: question.qLabel,
            options: tempArray.map((option) => {
              return { key: option.oKey, label: option.oLabel, id: option.id };
            }),
            multiple_choice: question.multiple_choice,
            controlType: question.multiple_choice ? 'select' : 'radio',
          });

          if (question.multiple_choice) {
            questionsArrayTemp.push(
              new SelectQuestion({
                key: question.qKey,
                label: question.qLabel,
                options: tempArray.map((option) => {
                  return {
                    key: option.oKey,
                    label: option.oLabel,
                    id: option.id,
                  };
                }),
              })
            );
          } else {
            questionsArrayTemp.push(
              new RadioQuestion({
                key: question.qKey,
                label: question.qLabel,
                options: tempArray.map((option) => {
                  return {
                    key: option.oKey,
                    label: option.oLabel,
                    id: option.id,
                  };
                }),
              })
            );
          }
        });
      });
    });

    const questionsFix: QuestionApiBase<string>[] = [
      new RadioQuestion({
        key: 'discovery',
        label: 'How did you find out about this job opportunity?',
        options: [
          { key: 'stackOverflow', label: 'StackOverflow', id: 1 },
          { key: 'indeed', label: 'Indeed', id: 2 },
          { key: 'other', label: 'Other', id: 3 },
        ],
      }),
      new SelectQuestion({
        key: 'location',
        label: 'How do you find the company’s location?',
        options: [
          {
            key: 'publicTransport',
            label: 'Easy to access by public transport',
            id: 4,
          },
          { key: 'car', label: 'Easy to access by car', id: 5 },
          { key: 'pleasant', label: 'In a pleasent area', id: 6 },
          { key: 'noneLocation', label: 'None of the above', id: 7 },
        ],
      }),
      new RadioQuestion({
        key: 'office',
        label:
          'What was your impression of the office where you had the interview?',
        options: [
          { key: 'tidy', label: 'Tidy', id: 8 },
          { key: 'sloppy', label: 'Sloppy', id: 9 },
          { key: 'noneOffice', label: 'Did not notice', id: 10 },
        ],
      }),
      new RadioQuestion({
        key: 'challenging',
        label: 'How technically challenging was the interview?',
        options: [
          { key: 'veryDifficult', label: 'Very difficult', id: 11 },
          { key: 'difficult', label: 'Difficult', id: 12 },
          { key: 'moderate', label: 'Moderate', id: 13 },
          { key: 'easy', label: 'Easy', id: 14 },
        ],
      }),
      new SelectQuestion({
        key: 'manager',
        label: 'How would you describe the manager that interviewed you?',
        options: [
          { key: 'enthusiastic', label: 'Enthusiastic', id: 15 },
          { key: 'polite', label: 'Polite', id: 16 },
          { key: 'organised', label: 'Organised', id: 17 },
          { key: 'noneManager', label: 'Could not tell', id: 18 },
        ],
      }),
    ];

    console.log(questionsArrayTemp);
    console.log(questionsFix);
    return of(questionsArrayTemp);
  }
}
