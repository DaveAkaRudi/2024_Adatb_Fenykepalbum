namespace PhotoApp.Models
{
    public class AllQueryResultsViewModel
    {
        public List<CategoryRatingViewModel> CategoryRatings { get; set; }
        public List<UserCommentCountViewModel> UserCommentCounts { get; set; }
        public List<CategoryImageCountViewModel> CategoryImageCounts { get; set; }
        public List<UserImageCountViewModel> UserImageCounts { get; set; }

    }
}
