import { QuestionBase } from './question-base';
import { QuestionApiBase } from './question-api-base';

export class CheckboxQuestion extends QuestionApiBase<string> {
  override controlType = 'checkbox';
}
