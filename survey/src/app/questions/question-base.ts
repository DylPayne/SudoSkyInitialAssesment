export class QuestionBase<T> {
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
      controlType?: string;
      type?: string;
      options?: { key: string; label: string }[];
      multiple_choice?: boolean;
    } = {}
  ) {
    this.value = options.value;
    this.key = options.key || '';
    this.label = options.label || '';
    this.controlType = options.controlType || '';
    this.type = options.type || '';
    this.options = options.options || [];
    this.multiple_choice = options.multiple_choice || false;
  }
}
