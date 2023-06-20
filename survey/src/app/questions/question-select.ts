import { QuestionApiBase } from './question-api-base';

export class SelectQuestion extends QuestionApiBase<string> {
  override controlType = 'select';
}
