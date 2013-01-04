using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Raven.Client;
using Raven.Contacts.Models;

namespace Raven.Contacts.Controllers
{
	public class ContactController : BaseController
	{

		public ViewResult Index()
		{

			var contacts = RavenSession.Query<Contact>();
			if (contacts != null)
				return View(contacts.AsEnumerable());

			return View(new List<Contact>());
		}

		//
		// GET: /Contact/Details/5

		public ActionResult Details(int id)
		{
			var contact = RavenSession.Load<Contact>(id);
			if (contact == null)
				return HttpNotFound();

			return View(contact);

		}

		//
		// GET: /Contact/Create

		public ActionResult Create()
		{
			return View(new Contact());
		}

		//
		// POST: /Contact/Create

		[HttpPost]
		public ActionResult Create(Contact contact)
		{
			try
			{
				RavenSession.Store(contact);

				//TODO once inserted, how do you select it? (GUID obvious choice)
				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}

		//
		// GET: /Contact/Edit/5

		public ActionResult Edit(int id)
		{
			var contact = RavenSession.Load<Contact>(id);
			if (contact == null)
				return HttpNotFound();

			return View(contact);
		}

		//
		// POST: /Contact/Edit/5

		[HttpPost]
		public ActionResult Edit(Contact contact)
		{
			try
			{
				RavenSession.Store(contact);

				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}

		public ActionResult Delete(string id)
		{
			int theId;
			if (int.TryParse(id, out theId))
			{
				var model = RavenSession.Load<Contact>(theId);
				return View(model);
			}
			return HttpNotFound();
		}

		[HttpPost, ActionName("Delete")]
		public ActionResult DeleteConfirmed(string id)
		{
			RavenSession.Advanced.DocumentStore.DatabaseCommands.Delete("contacts/" + id, null);
			//var contact = RavenSession.Load<Contact>(id);
			//RavenSession.Delete(contact);
			return RedirectToAction("Index");
		}

	}
}
