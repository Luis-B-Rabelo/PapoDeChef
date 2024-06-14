using Aspose.Words.Bibliography;
using FoodSocialMedia.MVVM.Models;
using NLog;
using PapoDeChef.Core;
using PapoDeChef.Database;
using PapoDeChef.MVVM.Models;
using System.Collections.ObjectModel;
using System.Security.Cryptography;
using System.Text;

namespace PapoDeChef.DAO
{
    public class AccountDAO
    {
        public static uint CreateNaturalAccount(string tag, string email, string name, string password)
        {
            try
            {
#if DEBUG
                GlobalNecessities.Logger.ForDebugEvent()
                    .Message("Conta a ser criada")
                    .Property("Tag", tag)
                    .Log();
#endif

                IDictionary<string, object> savedAccount = new Dictionary<string, object>
            {
                { "ID", DBConn.DB.AccountIDCounter++ },
                { "Tag", tag },
                { "Name", name },
                { "Email", email },
                { "Bio", string.Empty },
                { "QntFollowers", (uint)0 },
                { "Followers", new List<PreviewAccountModel>() },
                { "QntFollowing", (uint)0 },
                { "Following", new List<PreviewAccountModel>()},
                { "Chats", new ObservableCollection<uint>() },
                { "AccessLevel", (byte)0 },
                { "CreationDate", DateOnly.Parse(DateTime.Today.ToString("d")) },
                { "Birthdate", new DateOnly() }
            };

                if (!DBConn.DB.LoginInfo.ContainsKey(tag))
                {
#if DEBUG
                    GlobalNecessities.Logger.Debug("Criando conta");
#endif

                    string hashedPassword = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(password)));

                    DBConn.DB.LoginInfo.Add(tag, hashedPassword);
                    DBConn.DB.Accounts.Add(savedAccount);

#if DEBUG
                    GlobalNecessities.Logger.ForDebugEvent()
                        .Message("Conta criada")
                        .Property("Conta", savedAccount)
                        .Log();
#endif

                    return DBConn.DB.AccountIDCounter-1;
                }
                else
                {
#if DEBUG
                    GlobalNecessities.Logger.ForDebugEvent()
                        .Message("Erro para cadastrar conta, tag já cadastrada no sistema")
                        .Property("Conta", savedAccount)
                        .Log();
#endif
                    return 0;
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                GlobalNecessities.Logger.ForErrorEvent()
                    .Message("Erro para criar conta")
                    .Exception(ex)
                    .Log();
#endif
                return 0;
            }
        }

        public static uint ConfirmAccount(string tag, string password)
        {
            string hashedPassword = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(password)));

            KeyValuePair<string, string> loginInfo = new KeyValuePair<string, string>(tag, hashedPassword);

            IDictionary<string, object> acc = null;

#if DEBUG
            GlobalNecessities.Logger.ForDebugEvent()
                .Message("Confirmando informações da conta")
                .Property("Info", loginInfo)
                .Log();
#endif

            if (DBConn.DB.LoginInfo.AsParallel().Contains(loginInfo))
            {
                acc = DBConn.DB.Accounts.First(acc => (string)acc["Tag"] == tag);

                return (uint)acc["ID"];
            }
            else
            {
                return 0;
            }
        }

        public static IAccountModel GetAccountByID(uint ID)
        {
            IDictionary<string, object> savedAccount = DBConn.DB.Accounts[(int)ID - 1];

            if (savedAccount != null)
            {
                if ((byte)savedAccount["AccessLevel"] == (byte)0)
                {
                    NaturalPersonAccountModel naturalPersonAccountModel = new NaturalPersonAccountModel();
                    naturalPersonAccountModel.SetAccountModel(savedAccount);
                    return naturalPersonAccountModel;
                }
                else
                {
                    LegalPersonAccountModel legalPersonAccountModel = new LegalPersonAccountModel();
                    legalPersonAccountModel.SetAccountModel(savedAccount);
                    return legalPersonAccountModel;
                }
            }
            else
            {
                return null;
            }
        }


        public static bool ChangeNaturalAccountToLegal(NaturalPersonAccountModel naturalAccount)
        {
            IDictionary<string, object> updatedAccount = NaturalPersonAccountModelToDictionary(naturalAccount, true);

            int index = DBConn.DB.Accounts.FindIndex(acc => (uint)acc["ID"] == naturalAccount.ID);

            DBConn.DB.Accounts[index] = updatedAccount;

            if (updatedAccount != null)
            {
#if DEBUG
                GlobalNecessities.Logger.ForDebugEvent()
                    .Message("Conta mudada para jurídica")
                    .Property("Conta", updatedAccount)
                    .Log();
#endif
                return true;
            }

            return false;

        }

        public static bool ChangeLegalAccountToNatural(LegalPersonAccountModel legalAccount)
        {
            IDictionary<string, object> updatedAccount = LegalPersonAccountModelToDictionary(legalAccount, true);

            int index = DBConn.DB.Accounts.FindIndex(acc => (uint)acc["ID"] == legalAccount.ID);

            DBConn.DB.Accounts[index] = updatedAccount;

            if (updatedAccount != null)
            {
#if DEBUG
                GlobalNecessities.Logger.ForDebugEvent()
                    .Message("Conta mudada para física")
                    .Property("Conta", updatedAccount)
                    .Log();
#endif
                return true;
            }

            return false;

        }

        public static bool UpdateAccount(IAccountModel account)
        {
            try
            {
                IDictionary<string, object> updatedAccount = new Dictionary<string, object>();

                if (account.AccessLevel == 0)
                {
                    updatedAccount = NaturalPersonAccountModelToDictionary((NaturalPersonAccountModel)account, false);
                }
                else
                {
                    updatedAccount = LegalPersonAccountModelToDictionary((LegalPersonAccountModel)account, false);
                }


                int index = DBConn.DB.Accounts.FindIndex(acc => acc["Tag"] == account.Tag);
                DBConn.DB.Accounts[index] = updatedAccount;

#if DEBUG
                GlobalNecessities.Logger.ForDebugEvent()
                    .Message("Conta atualizada")
                    .Property("Conta", updatedAccount)
                    .Log();
#endif

                return true;
            }
            catch (Exception ex)
            {
#if DEBUG
                GlobalNecessities.Logger.ForErrorEvent()
                    .Message("Erro ao atualizar conta")
                    .Exception(ex)
                    .Log();
#endif
                return false;
            }
        }

        public static bool FollowAccount(PreviewAccountModel followAccount, PreviewAccountModel followerAccount)
        {
            try
            {
                int index; 
                List<PreviewAccountModel> follow;

                index = DBConn.DB.Accounts.FindIndex(acc => (uint)acc["ID"] == followAccount.ID);


                DBConn.DB.Accounts[index]["QntFollowers"] = (uint)DBConn.DB.Accounts[index]["QntFollowers"] + 1;

                follow = (List<PreviewAccountModel>)DBConn.DB.Accounts[index]["Followers"];

                follow.Add(followerAccount);

                DBConn.DB.Accounts[index]["Followers"] = follow;

                index = DBConn.DB.Accounts.FindIndex(acc => (uint)acc["ID"] == followerAccount.ID);

                DBConn.DB.Accounts[index]["QntFollowing"] = (uint)DBConn.DB.Accounts[index]["QntFollowing"] + 1;

                follow = (List<PreviewAccountModel>)DBConn.DB.Accounts[index]["Following"];
                follow.Add(followAccount);

                DBConn.DB.Accounts[index]["Following"] = follow;

#if DEBUG
                GlobalNecessities.Logger.ForDebugEvent()
                    .Message("Conta começou a seguir outra")
                    .Property("FollowerID", followerAccount)
                    .Property("FollowingID", followAccount)
                    .Log();
#endif

                return true;
            }
            catch (Exception ex)
            {
#if DEBUG
                GlobalNecessities.Logger.ForErrorEvent()
                    .Message("Conta não conseguiu começar a seguir outra")
                    .Property("FollowerID", followerAccount)
                    .Property("FollowingID", followAccount)
                    .Exception(ex)
                    .Log();
#endif
                return false;
            }
        }

        public static bool UnfollowAccount(PreviewAccountModel followAccount, PreviewAccountModel followerAccount)
        {
            try
            {
                int index;
                List<PreviewAccountModel> follow;

                index = DBConn.DB.Accounts.FindIndex(acc => (uint)acc["ID"] == followAccount.ID);


                DBConn.DB.Accounts[index]["QntFollowers"] = (uint)DBConn.DB.Accounts[index]["QntFollowers"] - 1;

                follow = (List<PreviewAccountModel>)DBConn.DB.Accounts[index]["Followers"];

                follow.Remove(followerAccount);

                DBConn.DB.Accounts[index]["Followers"] = follow;

                index = DBConn.DB.Accounts.FindIndex(acc => (uint)acc["ID"] == followerAccount.ID);

                DBConn.DB.Accounts[index]["QntFollowing"] = (uint)DBConn.DB.Accounts[index]["QntFollowing"] - 1;

                follow = (List<PreviewAccountModel>)DBConn.DB.Accounts[index]["Following"];
                follow.Remove(followAccount);

                DBConn.DB.Accounts[index]["Following"] = follow;

#if DEBUG
                GlobalNecessities.Logger.ForDebugEvent()
                    .Message("Conta parou de seguir outra")
                    .Property("FollowerID", followerAccount)
                    .Property("FollowingID", followAccount)
                    .Log();
#endif

                return true;
            }
            catch (Exception ex)
            {
#if DEBUG
                GlobalNecessities.Logger.ForErrorEvent()
                    .Message("Conta não conseguiu parar de seguir outra")
                    .Property("FollowerID", followerAccount)
                    .Property("FollowingID", followAccount)
                    .Exception(ex)
                    .Log();
#endif
                return false;
            }
        }

        private static IDictionary<string, object> NaturalPersonAccountModelToDictionary(NaturalPersonAccountModel naturalPersonAccountModel, bool converter)
        {
            try
            {
                IDictionary<string, object> dictionaryAccount = new Dictionary<string, object>();

                if (!converter)
                {

                        dictionaryAccount = new Dictionary<string, object>
                        {
                            { "ID", naturalPersonAccountModel.ID },
                            { "Tag", naturalPersonAccountModel.Tag },
                            { "Name", naturalPersonAccountModel.Name },
                            { "Email", naturalPersonAccountModel.Email },
                            { "Bio", naturalPersonAccountModel.Bio },
                            { "Followers", naturalPersonAccountModel.Followers },
                            { "QntFollowers", naturalPersonAccountModel.QntFollowers },
                            { "Following", naturalPersonAccountModel.Following },
                            { "QntFollowing", naturalPersonAccountModel.QntFollowing },
                            { "AccessLevel", naturalPersonAccountModel.AccessLevel },
                            { "CreationDate", naturalPersonAccountModel.CreationDate },
                            { "Birthdate", naturalPersonAccountModel.Birthdate }
                        };
#if DEBUG
                        GlobalNecessities.Logger.ForDebugEvent()
                            .Message("NaturalPersonAccountModel covertido para Dictionary")
                            .Property("NaturalPersonAccountModel", naturalPersonAccountModel)
                            .Property("Dictionary", dictionaryAccount)
                            .Log();
#endif
                }
                else
                {
                    dictionaryAccount = new Dictionary<string, object>
                    {
                        { "ID", naturalPersonAccountModel.ID },
                        { "Tag", naturalPersonAccountModel.Tag },
                        { "Name", naturalPersonAccountModel.Name },
                        { "Email", naturalPersonAccountModel.Email },
                        { "Bio", naturalPersonAccountModel.Bio },
                        { "Followers", naturalPersonAccountModel.Followers },
                        { "QntFollowers", naturalPersonAccountModel.QntFollowers },
                        { "Following", naturalPersonAccountModel.Following },
                        { "QntFollowing", naturalPersonAccountModel.QntFollowing },
                        { "AccessLevel", (byte)1 },
                        { "CreationDate", naturalPersonAccountModel.CreationDate },
                        { "FoundDate", new DateOnly() },
                        { "Address", new string[6]},
                        { "Category", string.Empty },
                        { "AverageRating", 0.0f }
                    };
#if DEBUG
                        GlobalNecessities.Logger.ForDebugEvent()
                            .Message("NaturalPersonAccountModel covertido para LegalPersonAccountModel e para Dictionary")
                            .Property("NaturalPersonAccountModel", naturalPersonAccountModel)
                            .Property("Dictionary", dictionaryAccount)
                            .Log();
#endif
                    }

                return dictionaryAccount;
            }
            catch (Exception ex)
            {
#if DEBUG
#if DEBUG
                GlobalNecessities.Logger.ForErrorEvent()
                    .Message("Erro ao converter NaturalPersonAccountModel para Dictionary")
                    .Exception(ex)
                    .Log();
#endif
#endif
                return null;
            }
        }

        private static IDictionary<string, object> LegalPersonAccountModelToDictionary(LegalPersonAccountModel legalPersonAccountModel, bool converter)
        {
            try
            {
                IDictionary<string, object> dictionaryAccount = new Dictionary<string, object>();

                if (!converter)
                {
                    dictionaryAccount = new Dictionary<string, object>
                    {
                        { "ID", legalPersonAccountModel.ID },
                        { "Tag", legalPersonAccountModel.Tag },
                        { "Name", legalPersonAccountModel.Name },
                        { "Email", legalPersonAccountModel.Email },
                        { "Bio", legalPersonAccountModel.Bio },
                        { "Followers", legalPersonAccountModel.Followers },
                        { "QntFollowers", legalPersonAccountModel.QntFollowers },
                        { "Following", legalPersonAccountModel.Following },
                        { "QntFollowing", legalPersonAccountModel.QntFollowing },
                        { "AccessLevel", legalPersonAccountModel.AccessLevel },
                        { "CreationDate", legalPersonAccountModel.CreationDate },
                        { "FoundDate", legalPersonAccountModel.FoundDate },
                        { "Address", legalPersonAccountModel.Address},
                        { "Category", legalPersonAccountModel.Category },
                        { "AverageRating", legalPersonAccountModel.AverageRating }
                    };
#if DEBUG
                    GlobalNecessities.Logger.ForDebugEvent()
                        .Message("LegalPersonAccountModel covertido para Dictionary")
                        .Property("LegalPersonAccountModel", legalPersonAccountModel)
                        .Property("AccountDictionary", dictionaryAccount)
                        .Log();
#endif
                }
                else
                {
                    dictionaryAccount = new Dictionary<string, object>
                    {
                        { "ID", legalPersonAccountModel.ID },
                        { "Tag", legalPersonAccountModel.Tag },
                        { "Name", legalPersonAccountModel.Name },
                        { "Email", legalPersonAccountModel.Email },
                        { "Bio", legalPersonAccountModel.Bio },
                        { "Followers", legalPersonAccountModel.Followers },
                        { "QntFollowers", legalPersonAccountModel.QntFollowers },
                        { "Following", legalPersonAccountModel.Following },
                        { "QntFollowing", legalPersonAccountModel.QntFollowing },
                        { "AccessLevel", (byte)0 },
                        { "CreationDate", legalPersonAccountModel.CreationDate },
                        { "Birthdate", new DateOnly() }
                    };
#if DEBUG
                    GlobalNecessities.Logger.ForDebugEvent()
                        .Message("LegalPersonAccountModel covertido para NaturalPersonAccountModel e para Dictionary")
                        .Property("LegalPersonAccountModel", legalPersonAccountModel)
                        .Property("AccountDictionary", dictionaryAccount)
                        .Log();
#endif
                    }

                return dictionaryAccount;
            }
            catch (Exception ex)
            {
#if DEBUG
#if DEBUG
                GlobalNecessities.Logger.ForErrorEvent()
                    .Message("Erro ao converter LegalPersonAccountModel para Dictionary")
                    .Exception(ex)
                    .Log();
#endif
#endif
                return null;
            }
        }

        
    }
}
