using PapoDeChef.MVVM.Models;
using System.Windows;
using System.Windows.Controls;

namespace PapoDeChef.MVVM.Views.Templates
{
    public class PostTemplateSelector : DataTemplateSelector
    {
        public DataTemplate NormalPostTemplate { get; set; }
        public DataTemplate RecipePostTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item == null)
            {
                return null;
            }

            FrameworkElement frameworkElement = (FrameworkElement)container;

            IPostModel post = (IPostModel)item;

            if (frameworkElement != null)
            {
                if (!post.IsRecipePost)
                {
                    NormalPostTemplate = (DataTemplate)frameworkElement.FindResource("NormalPostTemplate");
                    return NormalPostTemplate;
                }
                else
                {
                    RecipePostTemplate = (DataTemplate)frameworkElement.FindResource("RecipePostTemplate");
                    return RecipePostTemplate;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
