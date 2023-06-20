import { QuestionApiBase } from './question-api-base';
import { QuestionBase } from './question-base';

export class RadioQuestion extends QuestionApiBase<string> {
  override controlType = 'radio';
}
