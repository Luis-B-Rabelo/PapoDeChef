#region Internal Libs
#endregion

#region Downloaded Libs
#endregion

#region Project Files
#endregion

namespace FoodSocialMedia.MVVM.Models
{
    public class LegalPersonAccountModel : AccountModel, IAccountModel
    {
        #region Properties

        private DateOnly? _foundDate;

        private string? _category;

        private string[]? _address;

        private uint? _ratings;

        private float? _averageRating;

        #endregion

        #region Getters & Setters

        public DateOnly? FoundDate
        {
            get => _foundDate;
            set => _foundDate = (DateOnly)value;
        }

        public string? Category
        {
            get => _category;
            set => _category = value;
        }

        public string[]? Address
        {
            get => _address;
            set => _address = value;
        }

        public uint? Ratings
        {
            get => _ratings;
            set => _ratings = value;
        }

        public float? AverageRating
        {
            get => _averageRating;
            set => _averageRating = value;
        }

        #endregion

        #region Methods

        public override void SetAccountModel(IDictionary<string, object> savedAccount)
        {

            base.SetAccountModel(savedAccount);

            _foundDate = (DateOnly)savedAccount["FoundDate"];
            _category = (string)savedAccount["Category"];
            _address = (string[])savedAccount["Address"];
            _averageRating = (float)savedAccount["AverageRating"];
        }

        #endregion

    }
}
