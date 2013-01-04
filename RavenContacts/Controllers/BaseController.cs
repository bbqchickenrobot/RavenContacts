using System.Web.Mvc;
using Raven.Client;

namespace Raven.Contacts.Controllers
{
    public abstract class BaseController : Controller
    {
	    private IDocumentSession _ravenSession;

		public IDocumentSession RavenSession { 
			get
			{
				return _ravenSession ?? (_ravenSession = MvcApplication.Store.OpenSession());
			}
			protected set { _ravenSession = value; }
		}

		protected override void OnActionExecuted(ActionExecutedContext filterContext)
		{
			if (filterContext.IsChildAction)
				return;

			using (RavenSession)
			{
				if (filterContext.Exception != null)
					return;

				if (RavenSession != null)
					RavenSession.SaveChanges();
			}
		}
    }
}
