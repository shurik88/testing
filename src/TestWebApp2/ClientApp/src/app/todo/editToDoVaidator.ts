import { AbstractValidator } from "fluent-ts-validator";
import { AssignerValidator } from "./assignerValidator";

export class EditToDoValidator extends AbstractValidator<EditToDo>{
  constructor() {
    super();
    this.validateIfString(x => x.text).isNotEmpty().withFailureMessage("укажите текст");
    this.validateIfString(x => x.text).hasMaxLength(50).withFailureMessage("длинна текста не должна превышать 50 символов");
    this.validateIfNumber(x => x.priority).isGreaterThanOrEqual(1).isLessThanOrEqual(10)
      .withFailureMessage("значение приоритета должна быть в диапозаоне от 1 до 10 включительно");

    this.validateIfIterable(x => x.tags)
      .hasNumberOfElementsBetween(1, 3)
      .whenNotNull()
      .withFailureMessage("количество указанных тегов не может быть больше 3");

    this.validateIf(x => x.assignedTo).fulfills(new AssignerValidator()).whenNotNull();
  }
}
