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
				//lazy load, only create session if its requested.
				return _ravenSession ?? (_ravenSession = MvcApplication.Store.OpenSession());
			}
			protected set { _ravenSession = value; }
		}

		protected override void OnActionExecuted(ActionExecutedContext filterContext)
		{
			if (filterContext.IsChildAction)
				return;

			//if session was not created, dont do anything...
			if (RavenSession == null)
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
