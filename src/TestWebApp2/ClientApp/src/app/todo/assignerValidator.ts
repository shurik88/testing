import { AbstractValidator, Severity } from "fluent-ts-validator";

export class AssignerValidator extends AbstractValidator<Assigner>{
  constructor() {
    super();
    this.validateIfString(x => x.name).isNotEmpty().withFailureMessage("укажите имя");
    this.validateIfString(x => x.name).hasMaxLength(50).withFailureMessage("имя может содержать не более 50 символов");
    this.validateIfString(x => x.email).isEmail().withFailureMessage("укажаите корректный email");
  }
}
