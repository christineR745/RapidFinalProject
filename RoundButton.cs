using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace RapidFinalProject
{
    public partial class RoundButton : Button
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(this.Margin.Left,this.Margin.Top,
                this.Width-(this.Margin.Left+this.Margin.Right),
                this.Height-(this.Margin.Top+this.Margin.Bottom));
            this.Region = new System.Drawing.Region(path);
            base.OnPaint(e);
        }
    }
}
