using ACT.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;


namespace ACT.Plugins.Communication
{
    public class ACT_Emails : ACT.Plugins.ACT_Core, ACT.Core.Interfaces.Communication.I_Emails
    {
        public Guid StartBatch() { return Guid.NewGuid(); }
        public bool SendBatch() { return false; }
        public Core.Interfaces.Common.I_TestResult SendEmail(List<string> To, List<string> CC, List<string> BCC, string ReplyTo, string Subject, string Body)
        {
            var _TmpReturn = ACT.Core.CurrentCore<Core.Interfaces.Common.I_TestResult>.GetCurrent();

            try
            {
                string _Host = ACT.Core.SystemSettings.GetSettingByName("SMTP_HOST").Value;
                string _Port = ACT.Core.SystemSettings.GetSettingByName("SMTP_PORT").Value;
                string _UserName = ACT.Core.SystemSettings.GetSettingByName("SMTP_USERNAME").Value;
                string _Password = ACT.Core.SystemSettings.GetSettingByName("SMTP_PASSWORD").Value;

                var creds = new NetworkCredential(_UserName, _Password);
                var auth = creds.GetCredential(_Host, _Port.ToInt(), "Basic");

                using (var msg = new MailMessage())
                {
                    using (var smtp = new SmtpClient())
                    {
                        smtp.Host = _Host;
                        smtp.Port = _Port.ToInt();
                        smtp.Credentials = auth;
                        // ** can be set in config **

                        msg.From = new MailAddress(ReplyTo);

                        foreach (var t in To)
                        {
                            msg.To.Add(t);
                        }

                        foreach (var c in CC)
                        {
                            msg.CC.Add(c);
                        }

                        foreach (var b in BCC)
                        {
                            msg.Bcc.Add(b);
                        }
                        msg.Subject = Subject;
                        msg.Body = Body;
                        msg.IsBodyHtml = true;

                        smtp.Send(msg);
                    }
                }

                _TmpReturn.Success = true;
            }
            catch (Exception ex)
            {
                _TmpReturn.Success = false;
                _TmpReturn.Exceptions.Add(ex);
                LogError("ACT.Plugins.ACTEmails.SendEmail", "Error Sending email", ex, "", Core.Enums.ErrorLevel.Warning);
            }

            return _TmpReturn;
        }



        public Core.Interfaces.Common.I_TestResult SendEmail(string To, string CC, string BCC, string ReplyTo, string Subject, string Body, string FileName)
        {
            var _TmpReturn = ACT.Core.CurrentCore<Core.Interfaces.Common.I_TestResult>.GetCurrent();

            try
            {
                string _Host = ACT.Core.SystemSettings.GetSettingByName("SMTP_HOST").Value;
                string _Port = ACT.Core.SystemSettings.GetSettingByName("SMTP_PORT").Value;
                string _UserName = ACT.Core.SystemSettings.GetSettingByName("SMTP_USERNAME").Value;
                string _Password = ACT.Core.SystemSettings.GetSettingByName("SMTP_PASSWORD").Value;

                var creds = new NetworkCredential(_UserName, _Password);
                var auth = creds.GetCredential(_Host, _Port.ToInt(), "Basic");

                using (var msg = new MailMessage())
                {
                    using (var smtp = new SmtpClient())
                    {
                        smtp.Host = _Host;
                        smtp.Port = _Port.ToInt();
                        smtp.Credentials = auth;
                        // ** can be set in config **

                        msg.From = new MailAddress(ReplyTo);


                        msg.To.Add(To);

                        if (CC.NullOrEmpty() == false)
                        {
                            msg.CC.Add(CC);
                        }

                        if (BCC.NullOrEmpty() == false)
                        {
                            msg.Bcc.Add(BCC);
                        }

                        msg.Subject = Subject;
                        msg.Body = Body;
                        msg.IsBodyHtml = true;

                        if (!string.IsNullOrWhiteSpace(FileName))
                        {
                            Attachment attachment = new Attachment(FileName);
                            msg.Attachments.Add(attachment);
                        }

                        smtp.Send(msg);
                    }
                }

                _TmpReturn.Success = true;
            }
            catch (Exception ex)
            {
                _TmpReturn.Success = false;
                _TmpReturn.Exceptions.Add(ex);
                LogError("ACT.Plugins.ACTEmails.SendEmail", "Error Sending email", ex, "", Core.Enums.ErrorLevel.Warning);
            }

            return _TmpReturn;
        }

        public Core.Interfaces.Common.I_TestResult SendEmail(string To, string CC, string BCC, string ReplyTo, string Subject, string Body)
        {
            var _TmpReturn = ACT.Core.CurrentCore<Core.Interfaces.Common.I_TestResult>.GetCurrent();

            try
            {
                string _Host = ACT.Core.SystemSettings.GetSettingByName("SMTP_HOST").Value;
                string _Port = ACT.Core.SystemSettings.GetSettingByName("SMTP_PORT").Value;
                string _UserName = ACT.Core.SystemSettings.GetSettingByName("SMTP_USERNAME").Value;
                string _Password = ACT.Core.SystemSettings.GetSettingByName("SMTP_PASSWORD").Value;

                var creds = new NetworkCredential(_UserName, _Password);
                var auth = creds.GetCredential(_Host, _Port.ToInt(), "Basic");

                using (var msg = new MailMessage())
                {
                    using (var smtp = new SmtpClient())
                    {
                        smtp.Host = _Host;
                        smtp.Port = _Port.ToInt();
                        smtp.Credentials = auth;
                        // ** can be set in config **

                        msg.From = new MailAddress(ReplyTo);


                        msg.To.Add(To);

                        if (CC.NullOrEmpty() == false)
                        {
                            msg.CC.Add(CC);
                        }

                        if (BCC.NullOrEmpty() == false)
                        {
                            msg.Bcc.Add(BCC);
                        }

                        msg.Subject = Subject;
                        msg.Body = Body;
                        msg.IsBodyHtml = true;

                        smtp.Send(msg);
                    }
                }

                _TmpReturn.Success = true;

            }
            catch (Exception ex)
            {
                _TmpReturn.Success = false;
                _TmpReturn.Exceptions.Add(ex);
                LogError("ACT.Plugins.ACTEmails.SendEmail", "Error Sending email", ex, "", Core.Enums.ErrorLevel.Warning);
            }

            return _TmpReturn;
        }

        public Core.Interfaces.Common.I_TestResult SendEmail(string To, string CC, string BCC, string ReplyTo, string Subject, string Body, string Host, int Port, string UserName, string Password)
        {
            var _TmpReturn = ACT.Core.CurrentCore<Core.Interfaces.Common.I_TestResult>.GetCurrent();

            try
            {
                string _Host = Host;
                string _Port = Port.ToString();
                string _UserName = UserName;
                string _Password = Password;

                var creds = new NetworkCredential(_UserName, _Password);
                var auth = creds.GetCredential(_Host, _Port.ToInt(), "Basic");

                using (var msg = new MailMessage())
                {
                    using (var smtp = new SmtpClient())
                    {
                        smtp.Host = _Host;
                        try
                        {
                            smtp.Port = _Port.ToInt();
                        }
                        catch { }

                        smtp.Credentials = auth;
                        // ** can be set in config **

                        msg.From = new MailAddress(ReplyTo);


                        msg.To.Add(To);

                        if (CC.NullOrEmpty() == false)
                        {
                            msg.CC.Add(CC);
                        }

                        if (BCC.NullOrEmpty() == false)
                        {
                            msg.Bcc.Add(BCC);
                        }

                        msg.Subject = Subject;
                        msg.Body = Body;
                        msg.IsBodyHtml = true;

                        smtp.Send(msg);
                    }
                }

                _TmpReturn.Success = true;

            }
            catch (Exception ex)
            {
                _TmpReturn.Success = false;
                _TmpReturn.Exceptions.Add(ex);
                LogError("ACT.Plugins.ACTEmails.SendEmail", "Error Sending email", ex, "", Core.Enums.ErrorLevel.Warning);
            }

            return _TmpReturn;
        }

        public Core.Interfaces.Common.I_TestResult SendEmail(List<string> To, List<string> CC, List<string> BCC, string ReplyTo, string ReplyToDisplayName, string Subject, string Body, string Host, int Port, string UserName, string Password)
        {
            var _TmpReturn = ACT.Core.CurrentCore<Core.Interfaces.Common.I_TestResult>.GetCurrent();

            try
            {
                string _Host = Host;
                string _Port = Port.ToString();
                string _UserName = UserName;
                string _Password = Password;

                var creds = new NetworkCredential(_UserName, _Password);
                var auth = creds.GetCredential(_Host, _Port.ToInt(), "Basic");

                using (var msg = new MailMessage())
                {
                    using (var smtp = new SmtpClient())
                    {
                        smtp.Host = _Host;
                        try
                        {
                            smtp.Port = _Port.ToInt();
                        }
                        catch { }

                        smtp.Credentials = auth;
                        // ** can be set in config **

                        msg.From = new MailAddress(ReplyTo, ReplyToDisplayName);


                        foreach (var t in To)
                        {
                            msg.To.Add(t);
                        }

                        foreach (var c in CC)
                        {
                            msg.CC.Add(c);
                        }

                        foreach (var b in BCC)
                        {
                            msg.Bcc.Add(b);
                        }

                        msg.Subject = Subject;
                        msg.Body = Body;
                        msg.IsBodyHtml = true;

                        smtp.Send(msg);
                    }
                }

                _TmpReturn.Success = true;

            }
            catch (Exception ex)
            {
                _TmpReturn.Success = false;
                _TmpReturn.Exceptions.Add(ex);
                LogError("ACT.Plugins.ACTEmails.SendEmail", "Error Sending email", ex, "", Core.Enums.ErrorLevel.Warning);
            }

            return _TmpReturn;
        }

        /// <summary>
        /// Returns the Required System Settings
        /// </summary>
        /// <returns></returns>
        public override List<string> ReturnSystemSettingRequirements()
        {
			//string[] _S = new string[] { "SMTP_Host", "SMTP_Port", "SMTP_UserName", "SMTP_Password", "SMTP_Tracking_Database_Connection" };
			// TODO MARK - I REMOVED THE LAST PROPERTY BECAUSE WE ARE NOT CURRENTLY USING IT
			string[] _S = new string[] { "SMTP_HOST", "SMTP_PORT", "SMTP_USERNAME", "SMTP_PASSWORD" };
			return _S.ToList<string>();
        }


        public override Core.Interfaces.Common.I_TestResult HealthCheck()
        {

            var _TR = ValidatePluginRequirements();

            return _TR;

        }

        public override Core.Interfaces.Common.I_TestResult ValidatePluginRequirements()
        {
            var _TmpReturn = ACT.Core.SystemSettings.MeetsExpectations(this);

            return _TmpReturn;
        }
    }
}
