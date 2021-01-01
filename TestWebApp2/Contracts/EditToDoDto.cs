using System;
using System.ComponentModel.DataAnnotations;

namespace TestWebApp2.Contracts
{
    /// <summary>
    ///     Модель изменения задания.
    /// </summary>
    public class EditToDoDto
    {
        /// <summary>
        /// Текст
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Text { get; set; }

        /// <summary>
        /// Важность
        /// </summary>
        [Required]
        [Range(1, 10)]
        public int Priority { get; set; }

        /// <summary>
        /// Дедлайн
        /// </summary>
        //[CurrentDate(ErrorMessage = "Deadline should greater or equal than current date")]
        public DateTime? Deadline { get; set; }

        /// <summary>
        /// Теги
        /// </summary>
        [MaxLength(3, ErrorMessage = "Max tags count of 3 exceeded")]
        [MinLength(1, ErrorMessage = "Required at least 1 item")]
        public string[] Tags { get; set; }

        /// <summary>
        /// Предполагаемый исполнитель
        /// </summary>
        public AssignerDto AssignedTo { get; set; }
    }
}
