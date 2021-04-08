

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WP.DataLayer.Entities
{
    public class Genre
    {
        #region Body
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }
        #endregion

        #region Relations
        public virtual ICollection<Work> Works { get; set; }
        #endregion

        #region Ctor
        public Genre()
        {
            Works = new HashSet<Work>();
        }
        #endregion
    }
}
