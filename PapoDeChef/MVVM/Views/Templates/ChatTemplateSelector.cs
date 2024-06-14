using FoodSocialMedia.MVVM.Models;
using PapoDeChef.MVVM.Models;
using System.Windows;
using System.Windows.Controls;

namespace PapoDeChef.MVVM.Views.Templates
{
    public class ChatTemplateSelector : DataTemplateSelector
    {
        public DataTemplate Account1Template { get; set; }
        public DataTemplate Account2Template { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item == null)
            {
                return null;
            }

            FrameworkElement frameworkElement = (FrameworkElement)container;

            ChatModel chat = (ChatModel)item;

            if (frameworkElement != null)
            {
                if (chat.Account1.ID != Session.AccountSession.ID)
                {
                    Account1Template = (DataTemplate)frameworkElement.FindResource("Account1Template");
                    return Account1Template;
                }
                else
                {
                    Account2Template = (DataTemplate)frameworkElement.FindResource("Account2Template");
                    return Account2Template;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
