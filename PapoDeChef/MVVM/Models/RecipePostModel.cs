#region Internal Libs
#endregion

#region Downloaded Libs
#endregion

#region Project Files
#endregion


namespace FoodSocialMedia.MVVM.Models
{
    class RecipePostModel : PostModel
    {
        #region Properties
        private uint _ratings;

        private float _averageRating;

        private byte _servings;

        private string _ingredients;

        private string _directions;

        #endregion

        #region Getters & Setters

        public uint Ratings
        {
            get => _ratings;
        }

        public float AverageRating
        { 
            get => _averageRating;
        }

        public byte Servings
        {
            get => _servings;
        }

        public string Ingredients
        {
            get => _ingredients;
        }

        public string Directions
        {
            get => _directions;
        }

        #endregion

        #region Methods

        public override void SetPostModel(IDictionary<string, object> savedPost)
        {
            base.SetPostModel(savedPost);

            _ratings = (uint)savedPost["Ratings"];
            _averageRating = (float)savedPost["AverageRating"];
            //_servings = (byte)savedPost["Servings"];
            _ingredients = (string)savedPost["Ingredients"];
            _directions = (string)savedPost["Directions"];
        }

        #endregion
    }
}
