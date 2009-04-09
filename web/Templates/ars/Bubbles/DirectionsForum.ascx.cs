using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Mubble.Models;
using System.Collections.Generic;
using Mubble;

public partial class Templates_ars_DirectionsForum : Mubble.UI.UserControl
{
    protected string DefaultLinks = "<li><a href=\"http://r1.fmpub.net/r.php?r=http%3A%2F%2Fepisteme.arstechnica.com%2Feve%2Fforums%2Fa%2Ftpc%2Ff%2F901006821931%2Fm%2F678008691931&k1=ars-intel&k2=Directionsbox-May&k3=web20-SOA-myths\">Web 2.0, SOA and other myths...</a></li><li><a href=\"http://r1.fmpub.net/r.php?r=http%3A%2F%2Fepisteme.arstechnica.com%2Feve%2Fforums%2Fa%2Ftpc%2Ff%2F901006821931%2Fm%2F404006331931&k1=ars-intel&k2=Directionsbox-May&k3=parallel-programming\">What will Intel do to make parallel programming practical for pedestrian programmers?</a></li><li><a href=\"http://r1.fmpub.net/r.php?r=http%3A%2F%2Fepisteme.arstechnica.com%2Feve%2Fforums%2Fa%2Ftpc%2Ff%2F901006821931%2Fm%2F581007012931&k1=ars-intel&k2=Directionsbox-May&k3=secure-PC\">How do you make your PC \'secure\'?</a></li><li><a href=\"http://r1.fmpub.net/r.php?r=http%3A%2F%2Fepisteme.arstechnica.com%2Feve%2Fforums%2Fa%2Ftpc%2Ff%2F901006821931%2Fm%2F996002402931&k1=ars-intel&k2=Directionsbox-May&k3=web20-cloud\">Intel: \"Web 2.0\"-style cloud computing just a passing vapor</a></li><li><a href=\"http://r1.fmpub.net/r.php?r=http%3A%2F%2Fepisteme.arstechnica.com%2Feve%2Fforums%2Fa%2Ftpc%2Ff%2F901006821931%2Fm%2F505006902931&k1=ars-intel&k2=Directionsbox-May&k3=vPro-technology\">What is Intel vPro Technology?</a></li>";
    public Templates_ars_DirectionsForum()
    {
        this.Load += new EventHandler(DoLoad);   
    }

    void DoLoad(object sender, EventArgs e)
    {
        try
        {
            string file = Server.MapPath(System.IO.Path.Combine(this.TemplateSourceDirectory, "../ads/directions.html"));
            if (System.IO.File.Exists(file))
            {
                this.DefaultLinks = System.IO.File.ReadAllText(file);
            }
        }
        catch { }
        this.DefaultLinks = this.DefaultLinks.Replace(Environment.NewLine, "").Replace("'", "\\'");

        this.Links.InnerHtml = "<script type=\"text/javascript\">document.write('" + this.DefaultLinks + "');</script>";
    }
}
