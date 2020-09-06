///-------------------------------------------------------------------------------------------------
// file:	Interfaces\Communication\I_Emails.cs
//
// summary:	Declares the I_Emails interface
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ACT.Core.Interfaces.Common;

namespace ACT.Core.Interfaces.Communication
{

	/// <summary>
	/// This is the email processor plugin
	/// TODO MARK - Add ability to send multiple emails under the same smtp client connection
	/// TODO MARK - Add ability to pass multiple bodies (if body missing for one of the recs then we can't use an index when looping)
	/// TODO MARK - Add ability to pass FROM address as well
	/// TODO MARK - Add ability to add multiple CC and/or BCC
	/// </summary>
    public interface I_Emails : I_Plugin
    {
        Guid StartBatch();

        bool SendBatch();

        I_TestResult SendEmail(List<string> To, List<string> CC, List<string> BCC, string ReplyTo, string Subject, string Body);

        I_TestResult SendEmail(string To, string CC, string BCC, string ReplyTo, string Subject, string Body);

        I_TestResult SendEmail(string To, string CC, string BCC, string ReplyTo, string Subject, string Body, string FileName);

        I_TestResult SendEmail(string To, string CC, string BCC, string ReplyTo, string Subject, string Body, string Host, int Port, string UserName, string Password);

        I_TestResult SendEmail(List<string> To, List<string> CC, List<string> BCC, string ReplyTo, string ReplyToDisplayName, string Subject, string Body, string Host, int Port, string UserName, string Password);

    }
}
