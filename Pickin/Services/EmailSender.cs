using Pickin.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;
//using SendGrid;
//using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace Pickin.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;
        private readonly IHostingEnvironment _env;

        public EmailSender(
            IOptions<EmailSettings> emailSettings,
            IHostingEnvironment env)
        {
            _emailSettings = emailSettings.Value;
            _env = env;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                //var mimeMessage = new MimeMessage();

                //mimeMessage.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.Sender));

                //mimeMessage.To.Add(new MailboxAddress(email));

                //mimeMessage.Subject = subject;

                //mimeMessage.Body = new TextPart("html")
                //{
                //    Text = message
                //};

                //using (var client = new SmtpClient())
                //{
                //    // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                //    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                //    if (_env.IsDevelopment())
                //    {
                //        // The third parameter is useSSL (true if the client should make an SSL-wrapped
                //        // connection to the server; otherwise, false).
                //        await client.ConnectAsync(_emailSettings.MailServer, _emailSettings.MailPort, true);
                //    }
                //    else
                //    {
                //        await client.ConnectAsync(_emailSettings.MailServer);
                //    }

                //    // Note: only needed if the SMTP server requires authentication
                //    await client.AuthenticateAsync(_emailSettings.Sender, _emailSettings.Password);

                //    await client.SendAsync(mimeMessage);

                //    await client.DisconnectAsync(true);
                //}

                var client = new SmtpClient(_emailSettings.MailServer, _emailSettings.MailPort)
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(_emailSettings.Sender, _emailSettings.Password),
                    EnableSsl = Convert.ToBoolean(_emailSettings.EnableSSL)
                };

                MailMessage mailMessage = new MailMessage();
                mailMessage.IsBodyHtml = true;
                mailMessage.From = new MailAddress(_emailSettings.Sender);
                mailMessage.To.Add(email);

                mailMessage.Subject = subject;
                mailMessage.Body = message;

                client.Send(mailMessage);
            }
            catch (Exception ex)
            {
                // TODO: handle exception
                throw new InvalidOperationException(ex.Message);
            }
        }

        //public void GenerarMail(TipoEnvioLink id, FormularioContraseniaDTO formulario, string email)
        //{
        //    DefaultLogger.Log("Armo mail a enviar...", null);
        //    //Logger.Info("Armo mail a enviar...");

        //    try
        //    {
        //        string smtp = ((System.Collections.Specialized.NameValueCollection)(ConfigurationManager.GetSection("Seguridad.config"))).Get("SMTP");
        //        int port = Convert.ToInt32(((System.Collections.Specialized.NameValueCollection)(ConfigurationManager.GetSection("Seguridad.config"))).Get("PORT"));
        //        int enableSSL = Convert.ToInt32(((System.Collections.Specialized.NameValueCollection)(ConfigurationManager.GetSection("Seguridad.config"))).Get("ENABLE_SSL"));

        //        string user = ((System.Collections.Specialized.NameValueCollection)(ConfigurationManager.GetSection("Seguridad.config"))).Get("USER_CREDENCIAL");
        //        string pass = ((System.Collections.Specialized.NameValueCollection)(ConfigurationManager.GetSection("Seguridad.config"))).Get("PASS_CREDENCIAL");

        //        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12; // .NET 4.5 

        //        var client = new SmtpClient(smtp, port)
        //        {
        //            UseDefaultCredentials = false,
        //            Credentials = new NetworkCredential(user, pass),
        //            EnableSsl = Convert.ToBoolean(enableSSL)
        //        };

        //        string sender = ((System.Collections.Specialized.NameValueCollection)(ConfigurationManager.GetSection("Seguridad.config"))).Get("SENDER");

        //        MailMessage message = new MailMessage();
        //        message.IsBodyHtml = true;
        //        message.From = new MailAddress(sender);
        //        message.To.Add(email);

        //        int token;
        //        using (RNGCryptoServiceProvider rg = new RNGCryptoServiceProvider())
        //        {
        //            byte[] rno = new byte[16];
        //            rg.GetBytes(rno);
        //            token = BitConverter.ToInt32(rno, 0);
        //        }
        //        var tokenEncriptado = Md5Encryption(Convert.ToString(token));

        //        string html = string.Empty;
        //        switch (id)
        //        {
        //            case TipoEnvioLink.Registrar:
        //                {
        //                    html = File.ReadAllText(HttpContext.Current.Server.MapPath("~/EmailTemplates/Registrarse.html"));
        //                    html = html.Replace("{cliente}", formulario.Nombre);
        //                    html = html.Replace("{tipo_documento}", formulario.CUIT.Contains("-") ? "RUC" : "Cédula de Identidad Paraguaya");
        //                    html = html.Replace("{documento}", formulario.CUIT);
        //                    html = html.Replace("{fecha_nacimiento}", formulario.FechaNacimiento.Value.ToString("dd/MM/yyyy"));
        //                    html = html.Replace("{celular}", formulario.Celular);
        //                    html = html.Replace("{email}", formulario.Email);
        //                    message.Subject = Global.Registrarse;
        //                    break;
        //                }
        //            case TipoEnvioLink.Recuperar:
        //                {
        //                    html = File.ReadAllText(HttpContext.Current.Server.MapPath("~/EmailTemplates/Recuperar.html"));
        //                    message.Subject = Global.Recuperar;
        //                    break;
        //                }
        //        }

        //        html = html.Replace("{link}", GenerarLink(formulario.CUIT, tokenEncriptado, id));

        //        string header = HttpContext.Current.Server.MapPath(@"~/Images/mailing/header.png");
        //        LinkedResource headerResource = new LinkedResource(header);
        //        headerResource.ContentId = "{header}";
        //        headerResource.TransferEncoding = TransferEncoding.Base64;
        //        headerResource.ContentType.MediaType = MediaTypeNames.Image.Jpeg;

        //        string footer = HttpContext.Current.Server.MapPath(@"~/Images/mailing/footer.png");
        //        LinkedResource footerResource = new LinkedResource(footer);
        //        footerResource.ContentId = "{footer}";
        //        footerResource.TransferEncoding = TransferEncoding.Base64;
        //        footerResource.ContentType.MediaType = MediaTypeNames.Image.Jpeg;

        //        //html = html.Replace("{header}", HttpContext.Current.Server.MapPath(@"~/Images/mailing/header.png"));
        //        //html = html.Replace("{footer}", HttpContext.Current.Server.MapPath(@"~/Images/mailing/footer.png"));

        //        AlternateView htmlView = AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html);
        //        htmlView.LinkedResources.Add(headerResource);
        //        htmlView.LinkedResources.Add(footerResource);
        //        message.AlternateViews.Add(htmlView);
        //        message.IsBodyHtml = true;

        //        client.Send(message);
        //        new LoginBusiness().AltaPortalPass(formulario.CUIT, tokenEncriptado, (int)id);

        //        DefaultLogger.Log("Mail enviado correctamente...", null);
        //        //Logger.Info("Mail enviado correctamente...");
        //    }
        //    catch (Exception ex)
        //    {
        //        DefaultLogger.Log("Se produjo un error al enviar Mail: ", ex);
        //        //Logger.Error(ex, "Se produjo un error al enviar Mail: ");
        //    }
        //}
    }
}
