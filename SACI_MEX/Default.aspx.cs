using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SACI_MEX
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("Formularios/default.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }  
        }
    }
}