using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Test1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            InsertData();
            if (!this.IsPostBack)
            {
                //PlaceHolder placeholder = new PlaceHolder();

                //Populating a DataTable from database.
                DataTable dt = this.GetData();

                //Building an HTML string.
                StringBuilder html = new StringBuilder();

                //Table start.
                html.Append("<table border = '1'>");

                //Building the Header row.
                html.Append("<tr>");
                foreach (DataColumn column in dt.Columns)
                {
                    html.Append("<th>");
                    html.Append(column.ColumnName);
                    html.Append("</th>");
                }
                html.Append("</tr>");

                //Building the Data rows.
                foreach (DataRow row in dt.Rows)
                {
                    html.Append("<tr>");
                    foreach (DataColumn column in dt.Columns)
                    {
                        html.Append("<td>");
                        html.Append(row[column.ColumnName]);
                        html.Append("</td>");
                    }
                    html.Append("</tr>");
                }

                //Table end.
                html.Append("</table>");
                string strText = html.ToString();

                ////Append the HTML string to Placeholder.
                placeholderj.Controls.Add(new Literal { Text = html.ToString() });

            }
        }

        private DataTable GetData()
        {
            string constr = ConfigurationManager.ConnectionStrings["phongban"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Select * from phongban"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
        }

        private int InsertData()
        {
            string constr = ConfigurationManager.ConnectionStrings["phongban"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string str = "INSERT INTO PHONGBAN (MaPB, tenpb)" +
                    " VALUES(@MaPB,@tenpb)";
                using (SqlCommand comm = new SqlCommand(str))
                {

                    comm.Connection = con;
                    comm.CommandText = str;
                    comm.Parameters.AddWithValue("@MaPB", 8);
                    comm.Parameters.AddWithValue("@tenpb", "new jjj");
                    //comm.Parameters.AddWithValue("@val3", txtbox3.Text);
                    try
                    {
                        con.Open();
                        return comm.ExecuteNonQuery();
                    }
                    catch (SqlException e)
                    {
                        // do something with the exception
                        // don't hide it
                    }


                }
                return 0;
            }
        }
    }
}