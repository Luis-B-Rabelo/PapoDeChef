
namespace PapoDeChef.Events
{
    //Classe estática para navegagação entre Views
    static class NavigationEvent
    {
        #region Properties
        //Delegate para o evento "NavigationRequestedHandler" com seu parâmetro de tipo string "viewModelName"
        public delegate void NavigationRequestedHandler(string viewModelName, Dictionary<string, object> parameters);

        //Propriedade estática evento para quandar métodos/funções que serão rodados quando o evento "NavigateTo" for ativado
        public static event NavigationRequestedHandler? NavigationRequested;

        //Propriedade estática string ViewModelName, que temm a funcionalidade de passar qual é o nome do ViewModel que deve ser passado para navegação

        public static Dictionary<string, object> Parameters;

        #endregion

        #region Métodos
        //Método estático que ativa o evento de navegação
        public static void NavigateTo(string viewModelName)
        {
            //Ativando o evento e passando o parâmetro string "ViewModelName" para os métodos que serão chamados apartir desse evento
            NavigationRequested?.Invoke(viewModelName, Parameters);

            //Limpando propriedes para diminuir chance de problemas em navegações futuras
            if (Parameters != null) 
            {
                Parameters = null;
            }
        } 
        #endregion


    }
}
