/* 
 * Este modelo é apenas exemplo, não tem utilidade na Aplicação.
 * Dentro da class, criamos os campos que desejamos ter na base de dados.
 * Neste caso, temos 'Id', 'Name' e 'DisplayOrder'.
*/

using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PhunnyShop.Models
{
    public class Category
    {
        [Key]  // Data Annotation. Definimos que a propriedade abaixo será a Primary Key. Neste caso, 'Id'.
        public int CategoryId { get; set; }
        [Required]
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
    }
}
