// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="ACT_TextBox.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
#if DOTNETFRAMEWORK
using System.Web.UI;
using System.Web.UI.WebControls;
#endif

namespace ACT.Core.Web.WebForms
{
    #if DOTNETFRAMEWORK
    /// <summary>
    /// Class ACT_TextBox.
    /// Implements the <see cref="System.Web.UI.WebControls.WebControl" />
    /// </summary>
    /// <seealso cref="System.Web.UI.WebControls.WebControl" />
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:ACT_TextBox runat=server></{0}:ACT_TextBox>")]
    public class ACT_TextBox : WebControl
    {
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string Text
        {
            get
            {
                String s = (String)ViewState["Text"];
                return ((s == null) ? String.Empty : s);
            }

            set
            {
                ViewState["Text"] = value;
            }
        }



        /// <summary>
        /// Renders the contents.
        /// </summary>
        /// <param name="output">The output.</param>
        protected override void RenderContents(HtmlTextWriter output)
        {
            output.Write(Text);
        }
    }
#endif
}
