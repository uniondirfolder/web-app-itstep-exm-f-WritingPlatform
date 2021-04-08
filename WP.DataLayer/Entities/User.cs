
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WP.DataLayer.Entities
{
    public class User
    {
        #region Body
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Login { get; set;}

        [Required]
        [StringLength(250)]
        public string Password { get; set; }

        [Required]
        [StringLength(250)]
        public string Email { get; set; }

        public bool IsDelete { get; set; }
        #endregion

        #region Relations
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<Work> Works { get; set; }
        #endregion

        #region Ctor
        public User()
        {
            Comments = new HashSet<Comment>();
            Ratings = new HashSet<Rating>();
            Works = new HashSet<Work>();
        }
        #endregion
    }
}
