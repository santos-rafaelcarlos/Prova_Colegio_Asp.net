using Mvc.Mailer;
using SistemaAcademico.WebApp.Mailers.Models;

namespace SistemaAcademico.WebApp.Mailers
{ 
    public interface IPasswordResetMailer
    {
			MvcMailMessage PasswordReset(MailerModel model);
	}
}