// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="Email.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.IO;
using System.Linq;
using System.Net.Mail;


namespace ACT.Core.Communications
{
    /// <summary>
    /// Various Email Headers
    /// </summary>
    public static class EmailHelper
    {
        /// <summary>
        /// GenerateEmail To Open On Client Application
        /// </summary>
        /// <param name="msg">EmailMessage msg</param>
        /// <returns>byte[]</returns>
        public static byte[] GenerateEmail(ACT.Core.Types.Communication.EmailMessage msg)
        {

            var mailMessage = new MailMessage();

            mailMessage.From = msg.From;
            foreach (var t in msg.To) { mailMessage.To.Add(t); }
            foreach (var t in msg.CC) { mailMessage.CC.Add(t); }
            foreach (var t in msg.BCC) { mailMessage.Bcc.Add(t); }
            foreach(var t in msg.ReplyTo) { mailMessage.ReplyToList.Add(t); }

            mailMessage.Subject = msg.Subject;
            mailMessage.Body = msg.Body;

            // mark as draft
            mailMessage.Headers.Add("X-Unsent", "1");

            var stream = new MemoryStream();
            ToEmlStream(mailMessage, stream, msg.From.Address);
            stream.Position = 0;
            
            return stream.ToArray();
        }

        /// <summary>
        /// Converts to emlstream.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <param name="str">The string.</param>
        /// <param name="dummyEmail">The dummy email.</param>
        private static void ToEmlStream(MailMessage msg, Stream str, string dummyEmail)
        {
            using (var client = new SmtpClient())
            {
                var id = Guid.NewGuid();

                var tempFolder = Path.Combine(Path.GetTempPath(), System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);

                tempFolder = Path.Combine(tempFolder, "MailMessageToEMLTemp");

                // create a temp folder to hold just this .eml file so that we can find it easily.
                tempFolder = Path.Combine(tempFolder, id.ToString());

                if (!Directory.Exists(tempFolder))
                {
                    Directory.CreateDirectory(tempFolder);
                }

                client.UseDefaultCredentials = true;
                client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                client.PickupDirectoryLocation = tempFolder;
                client.Send(msg);

                // tempFolder should contain 1 eml file
                var filePath = Directory.GetFiles(tempFolder).Single();

                // create new file and remove all lines that start with 'X-Sender:' or 'From:'
                string newFile = Path.Combine(tempFolder, "modified.eml");
                using (var sr = new StreamReader(filePath))
                {
                    using (var sw = new StreamWriter(newFile))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            if (!line.StartsWith("X-Sender:") &&
                                !line.StartsWith("From:") &&
                                // dummy email which is used if receiver address is empty
                                !line.StartsWith("X-Receiver: " + dummyEmail) &&
                                // dummy email which is used if receiver address is empty
                                !line.StartsWith("To: " + dummyEmail))
                            {
                                sw.WriteLine(line);
                            }
                        }
                    }
                }

                // stream out the contents
                using (var fs = new FileStream(newFile, FileMode.Open))
                {
                    fs.CopyTo(str);
                }
            }
        }
    }
}
