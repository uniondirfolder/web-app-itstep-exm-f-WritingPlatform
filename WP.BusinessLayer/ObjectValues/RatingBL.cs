

namespace WP.BusinessLayer.ObjectValues
{
    public class RatingBL
    {
        public int Id { get; set; }
        public int Rank { get; set; }
        public int? WorkId { get; set; }
        public int? UserId { get; set; }
        public bool IsDeleteCheck { get; set; }
    }
}
