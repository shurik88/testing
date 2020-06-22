using System;
using System.ComponentModel.DataAnnotations;

namespace TestWebApp2.Model
{
    /// <summary>
    /// Дело
    /// </summary>
    public class ToDoDto
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid? Id { get; set; }

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
    }
}
