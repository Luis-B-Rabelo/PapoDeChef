
using FoodSocialMedia.MVVM.Models;
using NLog;
using PapoDeChef.Core;
using PapoDeChef.Database;
using PapoDeChef.MVVM.Models;
using System.Collections.ObjectModel;

namespace PapoDeChef.DAO
{
    public class PostDAO
    {
        public static uint CreateNormalPost(PreviewAccountModel account, string title, string description)
        {
            try
            {
                IDictionary<string, object> normalPost = new Dictionary<string, object>
                {
                    { "ID", DBConn.DB.PostIDCounter++ },
                    { "Account", account },
                    { "Title", title },
                    { "Description", description },
                    { "WhoLikedID", new List<uint>() },
                    { "LikeCount", (uint)0 },
                    { "IsRecipePost", false },
                    { "Comments", new ObservableCollection<CommentModel>()},
                    { "PostDateTime", DateTime.Now }
                };

                DBConn.DB.Posts.Add(normalPost);

#if DEBUG
                GlobalNecessities.Logger.ForDebugEvent()
                    .Message("Salvando NormalPost")
                    .Property("NormalPost", normalPost)
                    .Log();
#endif

                return DBConn.DB.PostIDCounter-1;
            }
            catch (Exception ex) 
            {
#if DEBUG
                GlobalNecessities.Logger.ForErrorEvent()
                    .Message("Erro para salvar post")
                    .Exception(ex)
                    .Log();
#endif
                return 0;
            }
        }

        public static uint CreateRecipePost(PreviewAccountModel account, string title, string description, string ingredients, string directions)
        {
            try
            {
                IDictionary<string, object> recipePost = new Dictionary<string, object>
                {
                    { "ID", DBConn.DB.PostIDCounter++ },
                    { "Account", account },
                    { "Title", title },
                    { "Description", description },
                    { "WhoLikedID", new List<uint>() },
                    { "LikeCount", (uint)0 },
                    { "IsRecipePost", true },
                    { "Comments", new ObservableCollection<CommentModel>() },
                    { "PostDateTime", DateTime.Now },
                    { "SumOfRatings", (uint)0 },
                    { "Ratings", (uint)0 },
                    { "AverageRating", 0.0f },
                    { "Ingredients", ingredients },
                    { "Directions", directions },
                };

                DBConn.DB.Posts.Add(recipePost);

#if DEBUG
                GlobalNecessities.Logger.ForDebugEvent()
                    .Message("Salvando RecipePost")
                    .Property("RecipePost", recipePost)
                    .Log();
#endif

                return DBConn.DB.PostIDCounter-1;
            }
            catch (Exception ex)
            {
#if DEBUG
                GlobalNecessities.Logger.ForErrorEvent()
                    .Message("Erro para salvar post")
                    .Exception(ex)
                    .Log();
#endif
                return 0;
            }
        }

        public static ObservableCollection<IPostModel> GetAccountPosts(uint accountID)
        {
            try
            {
                ObservableCollection<IPostModel> accountPosts = new ObservableCollection<IPostModel>();

                List<IDictionary<string, object>> accountPostsDic = DBConn.DB.Posts.AsParallel().Where(post => ((PreviewAccountModel)post["Account"]).ID == accountID).ToList();

                foreach (var post in accountPostsDic)
                {
                    IPostModel postModel;

                    if (!(bool)post["IsRecipePost"])
                    {
                        postModel = new PostModel();
                        postModel.SetPostModel(post);
                    }
                    else
                    {
                        postModel = new RecipePostModel();
                        postModel.SetPostModel(post);
                    }
                    accountPosts.Add(postModel);
                }

                accountPostsDic = null;
#if DEBUG
                GlobalNecessities.Logger.ForDebugEvent()
                    .Message("Pegando posts da conta")
                    .Property("AccountID", accountID)
                    .Property("Posts", accountPosts)
                    .Log();
#endif

                return new ObservableCollection<IPostModel>(accountPosts.Reverse().ToList());
            }
            catch (Exception ex)
            {
#if DEBUG
                GlobalNecessities.Logger.ForErrorEvent()
                    .Message("Erro para pegar posts da conta")
                    .Property("AccountID", accountID)
                    .Exception(ex)
                    .Log();
#endif

                return null;
            }
        }

        public static ObservableCollection<IPostModel> GetExplorePosts()
        {
            try
            {
                ObservableCollection<IPostModel> explorePosts = new ObservableCollection<IPostModel>();

                List<IDictionary<string, object>> explorePostsDic = DBConn.DB.Posts.AsParallel().Take(30).ToList();

                foreach (var post in explorePostsDic)
                {
                    IPostModel postModel;

                    if (!(bool)post["IsRecipePost"])
                    {
                        postModel = new PostModel();
                        postModel.SetPostModel(post);
                    }
                    else
                    {
                        postModel = new RecipePostModel();
                        postModel.SetPostModel(post);
                    }

                    explorePosts.Add(postModel);
                }

                explorePostsDic = null;
#if DEBUG
                GlobalNecessities.Logger.ForDebugEvent()
                    .Message("Pegando posts para o Explorar")
                    .Log();
#endif

                return new ObservableCollection<IPostModel>(explorePosts.Reverse().ToList());
            }
            catch (Exception ex)
            {
#if DEBUG
                GlobalNecessities.Logger.ForErrorEvent()
                    .Message("Erro para pegar posts para o Explorar")
                    .Exception(ex)
                    .Log();
#endif

                return null;
            }
        }

        public static ObservableCollection<IPostModel> GetHomePosts(List<PreviewAccountModel> following)
        {
            try
            {
                List<uint> followingIDs = new List<uint>();

                IDictionary<string, object> homePostDic;

                ObservableCollection<IPostModel> homePosts = new ObservableCollection<IPostModel>();

                foreach (PreviewAccountModel account in following) 
                {
                    homePostDic = DBConn.DB.Posts.AsParallel().FirstOrDefault(post => ((PreviewAccountModel)post["Account"]).ID == account.ID, null);

                    if (homePostDic != null)
                    {
                        IPostModel postModel;

                        if (!(bool)homePostDic["IsRecipePost"])
                        {
                            postModel = new PostModel();
                            postModel.SetPostModel(homePostDic);
                        }
                        else
                        {
                            postModel = new RecipePostModel();
                            postModel.SetPostModel(homePostDic);
                        }

#if DEBUG
                        GlobalNecessities.Logger.Debug("Adicionando Post para mostrar no Home");
#endif

                        homePosts.Add(postModel);
                    }

                }

#if DEBUG
                GlobalNecessities.Logger.ForDebugEvent()
                    .Message("Pegando posts para o Home")
                    .Property("Posts", homePosts)
                    .Log();
#endif

                return new ObservableCollection<IPostModel>(homePosts.Reverse().ToList());
            }
            catch (Exception ex)
            {
#if DEBUG
                GlobalNecessities.Logger.ForErrorEvent()
                    .Message("Erro para pegar posts para o Home")
                    .Exception(ex)
                    .Log();
#endif

                return null;
            }
        }

        public static bool LikePost(uint postID, uint accountID)
        {
            try
            {
                int postIndex = DBConn.DB.Posts.FindIndex(post => (uint)post["ID"] == postID);

                List<uint> temporaryList = (List<uint>)DBConn.DB.Posts[postIndex]["WhoLikedID"];

                if(!temporaryList.Contains(accountID) || temporaryList.Count == 0) 
                {
                    DBConn.DB.Posts[postIndex]["LikeCount"] = (uint)DBConn.DB.Posts[postIndex]["LikeCount"] + 1;
                    temporaryList.Add(accountID);
#if DEBUG
                    GlobalNecessities.Logger.ForDebugEvent()
                        .Message("Adicionando like do post")
                        .Property("PostID", postID)
                        .Property("AccountID", accountID)
                        .Log();
#endif
                }
                else
                {
                    DBConn.DB.Posts[postIndex]["LikeCount"] = (uint)DBConn.DB.Posts[postIndex]["LikeCount"] - 1;
                    temporaryList.Remove(accountID);
#if DEBUG
                    GlobalNecessities.Logger.ForDebugEvent()
                        .Message("Removendo like do post")
                        .Property("PostID", postID)
                        .Property("AccountID", accountID)
                        .Log();
#endif
                }

                DBConn.DB.Posts[postIndex]["WhoLikedID"] = temporaryList;
                temporaryList = null;

                return true;
            }
            catch (Exception ex)
            {
#if DEBUG
                GlobalNecessities.Logger.ForErrorEvent()
                    .Message("Erro para adicionar like ao post")
                    .Property("PostID", postID)
                    .Property("AccountID", accountID)
                    .Exception(ex)
                    .Log();
#endif
                return false;
            }
        }

        public static bool CommentOnNormalPost(uint postID, PreviewAccountModel account, string comment) 
        {
            try
            {
                int postIndex = DBConn.DB.Posts.FindIndex(post => (uint)post["ID"] == postID);

                ObservableCollection<CommentModel> temporaryList = (ObservableCollection<CommentModel>)DBConn.DB.Posts[postIndex]["Comments"];
                CommentModel commentModel = new CommentModel();
                commentModel.SetNormalComment(account, comment);

                temporaryList.Add(commentModel);


                DBConn.DB.Posts[postIndex]["Comments"] = temporaryList;
                temporaryList = null;

#if DEBUG
                GlobalNecessities.Logger.ForDebugEvent()
                    .Message("Adicionando comentário ao post")
                    .Property("PostID", postID)
                    .Property("Account", account)
                    .Property("Comentário", comment)
                    .Log();
#endif

                return true;
            }
            catch (Exception ex)
            {
#if DEBUG
                GlobalNecessities.Logger.ForErrorEvent()
                    .Message("Erro para adicionar comentário ao post")
                    .Property("PostID", postID)
                    .Property("AccountTag", account)
                    .Property("Comentário", comment)
                    .Exception(ex)
                    .Log();
#endif
                return false;
            }
        }

        public static bool CommentOnRecipePost(uint postID, PreviewAccountModel account, string comment, byte rating)
        {
            try
            {
                int postIndex = DBConn.DB.Posts.FindIndex(post => (uint)post["ID"] == postID);

                ObservableCollection<CommentModel> temporaryList = (ObservableCollection<CommentModel>)DBConn.DB.Posts[postIndex]["Comments"];
                CommentModel commentModel = new CommentModel();
                commentModel.SetRecipeComment(account, comment, rating);

                temporaryList.Add(commentModel);

                DBConn.DB.Posts[postIndex]["Comments"] = temporaryList;
                DBConn.DB.Posts[postIndex]["SumOfRatings"] = (uint)DBConn.DB.Posts[postIndex]["SumOfRatings"] + rating;
                DBConn.DB.Posts[postIndex]["Ratings"] = (uint)DBConn.DB.Posts[postIndex]["Ratings"] + 1;
                DBConn.DB.Posts[postIndex]["AverageRating"] = (float)(uint)DBConn.DB.Posts[postIndex]["SumOfRatings"] / (uint)DBConn.DB.Posts[postIndex]["Ratings"];

                temporaryList = null;

#if DEBUG
                GlobalNecessities.Logger.ForDebugEvent()
                    .Message("Adicionando comentário ao post")
                    .Property("PostID", postID)
                    .Property("AccountTag", account)
                    .Property("Comentário", comment)
                    .Log();
#endif

                return true;
            }
            catch (Exception ex)
            {
#if DEBUG
                GlobalNecessities.Logger.ForErrorEvent()
                    .Message("Erro para adicionar comentário ao post")
                    .Property("PostID", postID)
                    .Property("AccountTag", account)
                    .Property("Comentário", comment)
                    .Exception(ex)
                    .Log();
#endif
                return false;
            }
        }

        /*
        private static IDictionary<string, object> NormalPostModelToDictionary(PostModel normalPostModel)
        {
            try
            {
                IDictionary<string, object> dictionaryPost = new Dictionary<string, object>
                {
                    { "ID", normalPostModel.ID },
                    { "Account", normalPostModel.Account},
                    { "Title", normalPostModel.Title },
                    { "Description", normalPostModel.Description },
                    { "WhoLikedID", normalPostModel.WhoLikedID },
                    { "LikeCount", normalPostModel.LikeCount },
                    { "IsRecipePost", normalPostModel.IsRecipePost },
                    { "PostDateTime", normalPostModel.PostDateTime }
                };
#if DEBUG
                    GlobalNecessities.Logger.ForDebugEvent()
                        .Message("PostModel covertido para Dictionary")
                        .Property("PostModel", normalPostModel)
                        .Property("Dictionary", dictionaryPost)
                        .Log();
#endif

                return dictionaryPost;
            }
            catch (Exception ex)
            {
#if DEBUG
                GlobalNecessities.Logger.ForErrorEvent()
                    .Message("Erro ao converter NaturalPersonAccountModel para Dictionary")
                    .Exception(ex)
                    .Log();
#endif

                return null;
            }
            }

        private static IDictionary<string, object> RecipePostModelToDictionary(RecipePostModel recipePostModel)
        {
            try
            {
                IDictionary<string, object> dictionaryPost = new Dictionary<string, object>();



                dictionaryPost = new Dictionary<string, object>
                {
                    { "ID", recipePostModel.ID },
                    { "AccountIDFK", recipePostModel.AccountIDFK },
                    { "Title", recipePostModel.Title },
                    { "Description", recipePostModel.Description },
                    { "WhoLikedID", recipePostModel.WhoLikedID },
                    { "LikeCount", recipePostModel.LikeCount },
                    { "IsRecipePost", recipePostModel.IsRecipePost },
                    { "PostDateTime", recipePostModel.PostDateTime },
                    { "Ratings", recipePostModel.Ratings },
                    { "AverageRating", recipePostModel.AverageRating },
                    { "Ingredients", recipePostModel.Ingredients },
                    { "Diretions", recipePostModel.Directions },
                };
#if DEBUG
                GlobalNecessities.Logger.ForDebugEvent()
                    .Message("RecipePostModel covertido para Dictionary")
                    .Property("RecipePostModel", recipePostModel)
                    .Property("Dictionary", dictionaryPost)
                    .Log();
#endif

                return dictionaryPost;
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
        }*/

    }
}
