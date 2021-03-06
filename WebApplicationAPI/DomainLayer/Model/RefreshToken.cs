using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Model
{
    public class RefreshToken
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string refreshToken { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public DateTime Expires { get; set; }
        public User User { get; set; }
    }
}
