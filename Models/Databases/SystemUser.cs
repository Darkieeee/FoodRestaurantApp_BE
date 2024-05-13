using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FoodRestaurantApp_BE.Models.Databases {
    public class SystemUser {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        [JsonIgnore]
        public required string Password { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; } = null!;
        public DateTime? LastLogin { get; set; }
        public ICollection<SystemOrder> SystemOrders { get; set; } = new List<SystemOrder>();
    }
}
