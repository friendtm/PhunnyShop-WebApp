using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace PhunnyShop.Models
{
	public class Subscription
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]  // Auto-Increment.
		public int SubscriptionId { get; set; }
		[DisplayName("Nome")]
		[Required]
		public string SubscriptionName { get; set; }
		[DisplayName("Descrição")]
		public string Description { get; set; }
	}
}
