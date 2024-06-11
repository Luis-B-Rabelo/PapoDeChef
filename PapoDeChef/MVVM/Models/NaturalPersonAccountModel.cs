#region Internal Libs
#endregion

#region Downloaded Libs
#endregion

#region Project Files
#endregion

using System.Net;

namespace FoodSocialMedia.MVVM.Models
{
    public class NaturalPersonAccountModel : AccountModel, IAccountModel
    {
        #region Properties

        private DateOnly? _birthdate;

        #endregion

        #region Getters & Setters

        public DateOnly? Birthdate
        {
            get => _birthdate;
            set => _birthdate = (DateOnly)value;
        }

        #endregion

        #region Methods

        public override void SetAccountModel(IDictionary<string, object> savedAccount)
        {

            base.SetAccountModel(savedAccount);

            _birthdate = (DateOnly)savedAccount["Birthdate"];
        }


        #endregion

    }
}
