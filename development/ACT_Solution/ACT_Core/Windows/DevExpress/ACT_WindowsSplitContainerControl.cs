//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using DevExpress.XtraEditors;
//using DevExpress.XtraEditors.Drawing;
//using System.ComponentModel;

//namespace ACT.Core.Windows.DevExpressControls
//{
    
//    /// <summary>
//    /// Custom Split Container For Allowing Splitter Size Manipulation
//    /// </summary>
//    [ToolboxItem(true)]
//    public class ACT_DevExpressSplitContainercontrol : SplitContainerControl
//    {
//        private int _MySplitterSize = 10;

//        /// <summary>
//        /// Gets / Sets the Splitter Size
//        /// </summary>
//        public int MySplitterSize
//        {
//            get { return _MySplitterSize; }
//            set
//            {
//                _MySplitterSize = value;
//                OnPropertiesChanged();
//                PerformLayout();
//            }
//        }

//        /// <summary>
//        /// Public Constructor
//        /// </summary>
//        public ACT_DevExpressSplitContainercontrol()
//            : base()
//        {
//        }

//        /// <summary>
//        /// Override the ContainerInfo with new Custom One
//        /// </summary>
//        /// <returns></returns>
//        protected override SplitContainerViewInfo CreateContainerInfo()
//        {
//            return new ACT_DevExpressSplitContainerViewInfo(this);
//        }
//    }

//    /// <summary>
//    /// Custom Split Container View Info
//    /// </summary>
//    public class ACT_DevExpressSplitContainerViewInfo : SplitContainerViewInfo
//    {
//        /// <summary>
//        /// Open Constructor calls Base
//        /// </summary>
//        /// <param name="container"></param>
//        public ACT_DevExpressSplitContainerViewInfo(SplitContainerControl container)
//            : base(container)
//        {
//        }

//        /// <summary>
//        /// Gets the Splitter Size
//        /// </summary>
//        /// <returns>Integer</returns>
//        protected override int GetSplitterSize()
//        {
//            return (this.Container as ACT_DevExpressSplitContainercontrol).MySplitterSize;
//        }
//    }
//}
