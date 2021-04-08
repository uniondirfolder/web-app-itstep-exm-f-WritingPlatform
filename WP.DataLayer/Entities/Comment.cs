
using System.ComponentModel.DataAnnotations;


namespace WP.DataLayer.Entities
{
    public class Comment
    {
        #region Body
        [Key]
        public int Id { get; set; }

        [StringLength(250)]
        public string Body { get; set; }
        #endregion

        #region Relations
        public int? UserId { get; set; }
        public virtual User Users { get; set; }

        public int? WorkId { get; set; }
        public virtual Work Work { get; set; }
        #endregion
    }
}
