

using System.ComponentModel.DataAnnotations;

namespace WP.DataLayer.Entities
{
    public class Rating
    {
        #region Body
        [Key]
        public int Id { get; set; }

        public int? Rank { get; set; }
        #endregion

        #region Relations
        public int? WorkId { get; set; }
        public virtual Work Works { get; set; }

        public int? UserId { get; set; }
        public virtual User Users { get; set; }
        #endregion

    }
}
