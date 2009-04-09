public partial class _Default : System.Web.UI.Page
{
    public void Page_Load(object sender, System.EventArgs e)
    {
        Mubble.Tools.Http.RedirectPermanently(Mubble.Models.Controller.RootContent.Url);
    }
}
