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

        public void NaturalAccountToLegal(NaturalPersonAccountModel naturalAccount)
        {
            _tag = naturalAccount.Tag;
            _name = naturalAccount.Name;
            _email = naturalAccount.Email;
            _bio = naturalAccount.Bio;
            _qntFollowers = naturalAccount.QntFollowers;
            _followers = naturalAccount.Followers;
            _qntFollowing = naturalAccount.QntFollowing;
            _following = naturalAccount.Following;
            _accessLevel = 1;
            _creationDate = (DateOnly)naturalAccount.CreationDate;

            _foundDate = new DateOnly();
            _address = new string[6];
            _category = "";
            _averageRating = 0.0f;
        }

        #endregion

    }
}
