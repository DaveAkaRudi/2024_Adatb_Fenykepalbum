namespace PhotoApp.Models
{
    public class AllQueryResultsViewModel
    {
        public IEnumerable<CategoryRatingViewModel>? CategoryRatings { get; set; }
        public IEnumerable<UserCommentCountViewModel>? UserCommentCounts { get; set; }
        public IEnumerable<TelepulesPhotoCountViewModel>? TelepulesPhotoCounts { get; set; }
        public IEnumerable<CategoryImageCountViewModel>? CategoryImageCounts { get; set; }
        public IEnumerable<UserImageCountViewModel>? UserImageCounts { get; set; }
        public IEnumerable<KategoriakAlbumonkent>? KategoriakAlbumonkent { get; set; }
        public IEnumerable<AtlagosErtekelesFelhasznalonkent>? AtlagosErtekelesFelhasznalonkent { get; set; }
        public IEnumerable<CountryRatingViewModel>? CountryRatingViewModel { get; set; }
        public IEnumerable<AlbumRatingViewModel>? AlbumRatingViewModel { get; set; }
        public IEnumerable<MaxCategoryRatingViewModel>? MaxCategoryRatingViewModel { get; set; }



    }
}
