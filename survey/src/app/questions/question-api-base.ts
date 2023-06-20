export class QuestionApiBase<T> {
  value: T | undefined;
  key: string;
  label: string;
  type: string;
  options: { key: string; label: string }[];
  multiple_choice: boolean;
  controlType: string;
  constructor(
    options: {
      value?: T;
      key?: string;
      label?: string;
      type?: string;
      options?: { key: string; label: string; id: number }[];
      multiple_choice?: boolean;
      controlType?: string;
    } = {}
  ) {
    this.value = options.value;
    this.key = options.key || '';
    this.label = options.label || '';
    this.type = options.type || '';
    this.options = options.options || [];
    this.multiple_choice = options.multiple_choice || false;
    this.controlType = options.controlType || '';
  }
}
