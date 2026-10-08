using Azure.Core;
using Azure.Identity;
using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System.Text;

namespace DotNet8.WebApi.Factory.Utilities
{
    public class sendEmail
    {
        public static int sndEmail(string foldername, string subjectline, string mailto, string tenderid, string username, IConfiguration configuration)
        {
            StreamWriter sw = new StreamWriter("emailError.txt");
            sw.WriteLine("Email Log notes");

            int retVal = 0;

            try
            {
                var tenantId =      "6ee3e1ee-cb93-4e95-9027-d45c9dd3677e";
                var clientId =      "a350a4b3-a88d-4b8d-a7c3-0aac0054f706";
                var clientSecret =  "WuV8Q~nZbzxMdNV~tB5pf0ykFI1gU5sUolBa2cBN";
                var credential =    new ClientSecretCredential(tenantId, clientId, clientSecret);
                var message =       new MimeMessage();

                var token =         credential.GetToken(new TokenRequestContext(new[] { "https://outlook.office365.com/.default" }));

                message.From.Add(MailboxAddress.Parse("fpp.autotenders@eic.co.uk"));

                message.To.Add(MailboxAddress.Parse(mailto));

                message.Subject = subjectline;

                StringBuilder sb = new StringBuilder();

                using (StreamReader sr = new StreamReader(foldername + @"\email.txt"))
                {
                    while (!sr.EndOfStream)
                    {
                        sb.AppendLine(sr.ReadLine());
                    }
                }

                sw.WriteLine($"{foldername}\\email.txt file created");

                var builder = new BodyBuilder
                {
                    HtmlBody = sb.ToString(),

                    TextBody = "Please see the attached files."
                };

                //builder.Attachments.Add(@".\Uploads\Swaythling Mop Up tender NHH MON-001878.xlsx");

                message.Body = builder.ToMessageBody();

                using var protocolLog = new ProtocolLogger("smtp.log");

                //using var smtp = new SmtpClient(protocolLog);

                var smtp = new SmtpClient(protocolLog) { Timeout = 60000 };

                sw.WriteLine($"smtp object created");

                const int maxAttempts = 3;
                
                for (int attempt = 1; attempt <= maxAttempts; attempt++)
                {
                    try
                    {
                        sw.WriteLine($"Attempt {attempt}");

                        //await Task.Delay(TimeSpan.FromSeconds(30));

                        //await smtp.ConnectAsync("smtp.office365.com", 587, SecureSocketOptions.StartTls);
                        smtp.Connect("smtp.office365.com", 587, SecureSocketOptions.StartTls);

                        sw.WriteLine($"SMTP connected Connected");

                        var oauth2 = new SaslMechanismOAuth2("fpp.autotenders@eic.co.uk", token.Token);

                        //await smtp.AuthenticateAsync(oauth2);
                        //await smtp.SendAsync(message);
                        //await smtp.DisconnectAsync(true);
                        smtp.Authenticate(oauth2);
                        smtp.Send(message);
                        smtp.Disconnect(true);

                        retVal = 1;

                        double rval = updateSql.updateTenderSent(tenderid, username, configuration);

                        sw.WriteLine("Mail sent ");

                        break;
                    }
                    catch (TimeoutException ex)
                    {
                        if (attempt == maxAttempts)
                            throw;

                        //await Task.Delay(TimeSpan.FromSeconds(5));
                    }
                    catch (Exception ex)
                    {
                        sw.WriteLine(ex.Message.ToString());
                    }
                }
                
            }
            catch (SmtpCommandException ex)
            {
                sw.WriteLine(ex.Message.ToString());
                sw.WriteLine(ex.StatusCode.ToString());
            }
            catch (SmtpProtocolException ex)
            {
                sw.WriteLine(ex.Message.ToString());
            }
            catch (Exception ex)
            {
                sw.WriteLine(ex.Message.ToString());
            }
            finally
            {
            }

            sw.WriteLine("Log Ended");
            sw.Close();

            return retVal;
        }


    }
}
