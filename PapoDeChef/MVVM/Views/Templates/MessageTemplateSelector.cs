using FoodSocialMedia.MVVM.Models;
using System.Windows;
using System.Windows.Controls;

namespace PapoDeChef.MVVM.Views.Templates
{
    public class MessageTemplateSelector : DataTemplateSelector
    {
        public DataTemplate Message1Template { get; set; }
        public DataTemplate Message2Template { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item == null)
            {
                return null;
            }

            FrameworkElement frameworkElement = (FrameworkElement)container;

            MessageModel chat = (MessageModel)item;

            if (frameworkElement != null)
            {
                if (chat.SentByAccountID != Session.AccountSession.ID)
                {
                    Message1Template = (DataTemplate)frameworkElement.FindResource("Message1Template");
                    return Message1Template;
                }
                else
                {
                    Message2Template = (DataTemplate)frameworkElement.FindResource("Message2Template");
                    return Message2Template;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
