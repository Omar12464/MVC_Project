using MVC_Project.DAL.Models;
using System.Net;
using System.Net.Mail;

namespace MVC_Project.PL.Helper
{
	public class EmailSettingd
	{
		public static void SendEmail(Email email)
		{
			var client = new SmtpClient("smtp.gmail.com",587);
			client.EnableSsl = false;
			client.Credentials = new NetworkCredential("oe04008@gmail.com", "Omar@159357");
			client.Send("oe04008@gmail.com",email.Recipiant,email.Subject,email.Body);
		}
    }
}
