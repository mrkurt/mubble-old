using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Mubble.Models;
using System.Collections.Generic;
using Mubble;

public partial class Templates_ars_Intel : Mubble.UI.UserControl
{
    protected string DefaultLinks = "<li><a href=\"http://r1.fmpub.net/r.php?r=http%3A%2F%2Fwww.intel.com%2Fit%2Fpdf%2Fmulticore_virtualization.pdf%3Fppc_cid%3DEC2DSy5AR1H08us_1&k1=ppc_cid%3DEC2DSy5AR1H08us_1&k2=ent1\">Comparing Multi-Core Processors for Server Virtualization</a></li><li><a href=\"http://r1.fmpub.net/r.php?r=http%3A%2F%2Fwww.intel.com%2Fit%2Fpdf%2Fclient-pcs-as-strategic-assets.pdf%3Fppc_cid%3DEC2DSy5AR1H08us_6&k1=ppc_cid%3DEC2DSy5AR1H08us_6&k2=ent6\">Client PCs as Strategic Assets</a></li><li><a href=\"http://r1.fmpub.net/r.php?r=http%3A%2F%2Fsoftwarecommunity.intel.com%2Farticles%2Feng%2F1273.htm%3Fppc_cid%3DEC2DSy5AR1H08us_4&k1=ppc_cid%3DEC2DSy5AR1H08us_4&k2=ent4\">Transitioning Software to Future Generations of Multi-Core</a></li><li><a href=\"http://r1.fmpub.net/r.php?r=http%3A%2F%2Fcommunities.intel.com%2Fservlet%2FJiveServlet%2FpreviewBody%2F1083-102-2-1041%2Fbuilding-real-world-model-to-assess-virtualization-platforms.pdf%3Fppc_cid%3DEC2DSy5AR1H08us_2&k1=ppc_cid%3DEC2DSy5AR1H08us_2&k2=ent2\">Building a Real-World Model to Assess Virtualization Platforms</a></li><li><a href=\"http://r1.fmpub.net/r.php?r=http%3A%2F%2Fwww.intel.com%2Fbusiness%2Fbusiness-pc%2Froi%2Fcentrinoprowhitepaper.pdf%3Fppc_cid%3DEC2DSy5AR1H08us_5&k1=ppc_cid%3DEC2DSy5AR1H08us_5&k2=ent5\">The ROI with Intel Centrino with vPro technology</a></li><li><a href=\"http://r1.fmpub.net/r.php?r=http%3A%2F%2Fprincipledtechnologies.com%2Fclients%2Freports%2FIntel%2FE7340SPECint0907.pdf%3Fppc_cid%3DEC2DSy5AR1H08us_3&k1=ppc_cid%3DEC2DSy5AR1H08us_3&k2=ent3\">Comparative EEP Benchmarks: SPEC CPU2006 SPECint Rate Based Performance & Power Consumption</a></li>";
    public Templates_ars_Intel()
    {
        this.Load += new EventHandler(DoLoad);   
    }

    void DoLoad(object sender, EventArgs e)
    {
        //try
        //{
        //    string file = Server.MapPath(System.IO.Path.Combine(this.TemplateSourceDirectory, "../ads/intel.html"));
        //    if (System.IO.File.Exists(file))
        //    {
        //        this.DefaultLinks = System.IO.File.ReadAllText(file);
        //    }
        //}
        //catch { }
        //this.DefaultLinks = this.DefaultLinks.Replace(Environment.NewLine, "").Replace("'", "\\'");
        //this.Links.InnerHtml= "<script type=\"text/javascript\">document.write('" + this.DefaultLinks + "');</script>";
    }
}