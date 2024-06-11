
using FoodSocialMedia.MVVM.Models;
using NLog;
using PapoDeChef.Core;
using PapoDeChef.Database;
using System.Collections.ObjectModel;

namespace PapoDeChef.DAO
{
    public class PostDAO
    {
        public static uint CreateNormalPost(uint accountIDFK, string title, string description)
        {
            try
            {
                IDictionary<string, object> normalPost = new Dictionary<string, object>
                {
                    { "ID", DBConn.DB.PostIDCounter++ },
                    { "AccountIDFK", accountIDFK },
                    { "Title", title },
                    { "Description", description },
                    { "WhoLikedID", new List<uint>() },
                    { "LikeCount", (uint)0 },
                    { "IsRecipePost", false },
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

        public static uint CreateRecipePost(uint accountIDFK, string title, string description, string ingredients, string directions)
        {
            try
            {
                IDictionary<string, object> recipePost = new Dictionary<string, object>
                {
                    { "ID", DBConn.DB.PostIDCounter++ },
                    { "AccountIDFK", accountIDFK },
                    { "Title", title },
                    { "Description", description },
                    { "WhoLikedID", new List<uint>() },
                    { "LikeCount", (uint)0 },
                    { "IsRecipePost", true },
                    { "PostDateTime", DateTime.Now },
                    { "Ratings", (uint)0 },
                    { "AverageRating", 0.0f },
                    { "ServingSize", 0 },
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

        public static ObservableCollection<PostModel> GetAccountPosts(uint accountID)
        {
            try
            {
                ObservableCollection<PostModel> accountPosts = new ObservableCollection<PostModel>();

                List<IDictionary<string, object>> accountPostsDic = DBConn.DB.Posts.AsParallel().Where(post => (uint)post["AccountIDFK"] == accountID).ToList();

                foreach (var post in accountPostsDic)
                {
                    PostModel postModel = new PostModel();
                    postModel.SetPostModel(post);
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

                return accountPosts;
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

        public static ObservableCollection<PostModel> GetExplorePosts()
        {
            try
            {
                ObservableCollection<PostModel> explorePosts = new ObservableCollection<PostModel>();

                List<IDictionary<string, object>> explorePostsDic = DBConn.DB.Posts.AsParallel().Take(30).ToList();

                foreach (var post in explorePostsDic)
                {
                    PostModel postModel = new PostModel();
                    postModel.SetPostModel(post);
                    explorePosts.Add(postModel);
                }

                explorePostsDic = null;
#if DEBUG
                GlobalNecessities.Logger.ForDebugEvent()
                    .Message("Pegando posts para o Explorar")
                    .Log();
#endif

                return explorePosts;
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

        public static bool LikePost(uint postID, uint accountID)
        {
            try
            {
                int postIndex = DBConn.DB.Posts.FindIndex(post => (uint)post["ID"] == postID);

                DBConn.DB.Posts[postIndex]["LikeCount"] = (uint)DBConn.DB.Posts[postIndex]["LikeCount"] + 1;
                List<uint> temporaryList = (List<uint>)DBConn.DB.Posts[postIndex]["WhoLikedID"];
                temporaryList.Add(accountID);
                DBConn.DB.Posts[postIndex]["WhoLikedID"] = temporaryList;
                temporaryList = null;

#if DEBUG
                GlobalNecessities.Logger.ForDebugEvent()
                    .Message("Adicionando like ao post")
                    .Property("PostID", postID)
                    .Property("AccountID", accountID)
                    .Log();
#endif

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

        private static IDictionary<string, object> NormalPostModelToDictionary(PostModel normalPostModel)
        {
            try
            {
                IDictionary<string, object> dictionaryPost = new Dictionary<string, object>
                {
                    { "ID", normalPostModel.ID },
                    { "AccountIDFK", normalPostModel.AccountIDFK},
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
                    { "ServingSize", recipePostModel.Servings },
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
        }

    }
}
