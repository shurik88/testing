using System.ComponentModel.DataAnnotations;

namespace TestWebApp2.Contracts
{
    /// <summary>
    /// Исполнитель
    /// </summary>
    public class AssignerDto
    {
        /// <summary>
        /// ФИО
        /// </summary>
        //[Required]
        //[MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        //[Required]
        //[EmailAddress]
        public string Email { get; set; }
    }
}
