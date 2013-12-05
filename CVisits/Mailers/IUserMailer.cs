using Mvc.Mailer;
using System;

namespace CVisits.Mailers
{ 
    public interface IUserMailer
    {
        MvcMailMessage VisitScheduled(string clientsmail, string clientsname, string description, string type, string visitorname, DateTime visitsdate);
	}
}