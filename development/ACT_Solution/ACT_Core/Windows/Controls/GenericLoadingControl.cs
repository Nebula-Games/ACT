///-------------------------------------------------------------------------------------------------
// file:	Windows\Controls\GenericLoadingControl.cs
//
// summary:	Implements the generic loading control class
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if DOTNETFRAMEWORK
using System.Windows.Forms;
#endif
using System.Drawing.Drawing2D;
using ACT.Core.Extensions;


namespace ACT.Core.Windows.Controls
{
#if DOTNETFRAMEWORK
    public partial class GenericLoadingControl : UserControl
    {
        public GenericLoadingControl()
        {
            InitializeComponent();
            
        }

        private int radius = 20;
        [DefaultValue(20)]
        public int Radius
        {
            get { return radius; }
            set
            {
                radius = value;
                this.RecreateRegion();
            }
        }
        private GraphicsPath GetRoundRectagle(Rectangle bounds, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(bounds.X, bounds.Y, radius, radius, 180, 90);
            path.AddArc(bounds.X + bounds.Width - radius, bounds.Y, radius, radius, 270, 90);
            path.AddArc(bounds.X + bounds.Width - radius, bounds.Y + bounds.Height - radius,
                        radius, radius, 0, 90);
            path.AddArc(bounds.X, bounds.Y + bounds.Height - radius, radius, radius, 90, 90);
            path.CloseAllFigures();
            return path;
        }
        private void RecreateRegion()
        {
            var bounds = ClientRectangle;
            bounds.Width--; bounds.Height--;
            using (var path = GetRoundRectagle(bounds, this.Radius))
                this.Region = new Region(path);
            this.Invalidate();
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.RecreateRegion();
        }

        public event ACT.Core.Delegates.OnComplete OnCompleted;

        public void StartLoadingProcess(List<string> ResourceFiles)
        {
            BackgroundWorker _Worker = new BackgroundWorker();
            _Worker.DoWork += _Worker_DoWork;            
            _Worker.RunWorkerCompleted += _Worker_RunWorkerCompleted;
            this.Parent.Enabled = false;
        }

        public static Dictionary<string, object> ReturnData = new Dictionary<string, object>();

        private void _Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (OnCompleted == null) { return; }
            OnCompleted(new object[] { ReturnData });
        }
        
        private void _Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            string _tmp = AppDomain.CurrentDomain.BaseDirectory.EnsureDirectoryFormat() + "Resources";
            _tmp.GetAllFilesFromPath(true);
        }

        private static void LoadResource(string ResourceFile)
        {
            if (ResourceFile.ToLower().EndsWith(".json"))
            {
                dynamic tmpObj = Newtonsoft.Json.Linq.JObject.Parse(ResourceFile.ReadAllText());
                ReturnData.Add(ResourceFile, tmpObj);
            }
        }
    }
#endif
}
