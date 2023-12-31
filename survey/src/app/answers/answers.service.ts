import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { environment } from 'src/environments/environment.development';

import { of } from 'rxjs';
import { map, delay } from 'rxjs/operators';

interface Answer {
  oKey: string;
  oLabel: string;
  selections: number;
  qId: number;
}
interface AnswersLayout {
  label: string;
  qId: number;
  keys: { key: string; label: string; selections: number }[];
}

@Injectable()
export class AnswersService {
  // private answer: Answer = {};
  private url: string = 'http://localhost:5175/api/Answers';
  private qUrl: string = 'http://localhost:5175/api/Questions';

  public answers: Answer[] = [];
  public answersLayout: AnswersLayout[] = [];

  public response: any;
  public labels: any;

  constructor(private http: HttpClient) {}

  async fetchLabels() {
    console.log('Fetching labels');
    this.response = await this.http
      .get<any>(this.qUrl)
      .pipe(delay(1000))
      .toPromise();
    return this.response;
  }

  getAnswers() {
    this.fetchLabels().then((response) => {
      this.labels = response;
      this.http.get<Answer[]>(this.url).subscribe((response: Answer[]) => {
        this.answers = response;
        this.answers.forEach((answer) => {
          if (
            JSON.stringify(this.answersLayout).includes(answer.qId.toString())
          ) {
            let i: number = 0;
            this.answersLayout.forEach((layout) => {
              if (layout.qId === answer.qId) {
                this.answersLayout[i].keys.push({
                  key: answer.oKey,
                  label: answer.oLabel,
                  selections: answer.selections,
                });
              }
              i++;
            });
          } else {
            let qLabelTemp: string = '';
            this.labels.forEach((label: any) => {
              if (label.id === answer.qId) {
                qLabelTemp = label.qLabel;
              }
            });
            this.answersLayout.push({
              label: qLabelTemp,
              qId: answer.qId,
              keys: [
                {
                  key: answer.oKey,
                  label: answer.oLabel,
                  selections: answer.selections,
                },
              ],
            });
          }
        });
        this.answersLayout.forEach((layout) => {
          let sum: number = 0;
          layout.keys.forEach((key) => {
            sum += key.selections;
          });
          console.log(sum);
          let i: number = 0;
          layout.keys.forEach((key) => {
            this.answersLayout[this.answersLayout.indexOf(layout)].keys[
              i
            ].selections = (key.selections / sum) * 100;
            i++;
          });
        });
      });
    });

    return of(this.answersLayout);
  }

  postAnswers(formAnswer: any) {
    this.http
      .post<any>(this.url, formAnswer, {
        headers: new HttpHeaders({
          accept: 'text/plain',
          'content-type': 'application/json',
        }),
      })
      .subscribe((response: Answer) => {
        console.log(response);
      });
  }
}
