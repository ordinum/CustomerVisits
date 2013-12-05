using Mvc.Mailer;
using System;

namespace CVisits.Mailers
{ 
    public class UserMailer : MailerBase, IUserMailer 	
	{
		public UserMailer()
		{
			MasterName="_Layout";
		}
		

        public virtual MvcMailMessage VisitScheduled(string clientsmail, string clientsname, string description, string type, string visitorname, DateTime visitsdate)
        {
            //ViewBag.Data = someObject;            
            ViewBag.ClientName = clientsname;
            ViewBag.VisitDescription = description;
            ViewBag.VisitType = type;
            ViewBag.VisitorName = visitorname;
            ViewBag.VisitDate = visitsdate;
                       
            return Populate(x =>
            {
                x.Subject = "A Customer Visit has been scheduled for you by a Metso Sales Engineer";
                x.ViewName = "VisitScheduled";
                x.To.Add(clientsmail);
            });
        }
 	}
}