using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace StudentRecords
{
	public partial class Default : System.Web.UI.Page
	{
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DefaultStudentRecord();
            }
        }
        private void DefaultStudentRecord()
        {
            //creating dataTable   
            DataTable dt = new DataTable();
            DataRow dr;
            dt.TableName = "StudentDetails";
            dt.Columns.Add(new DataColumn("Name", typeof(string)));
            dt.Columns.Add(new DataColumn("Roll No", typeof(string)));
            dt.Columns.Add(new DataColumn("Age", typeof(string)));
            dt.Columns.Add(new DataColumn("Gender", typeof(string)));
            dr = dt.NewRow();
            dt.Rows.Add(dr);
            //saving databale into viewstate   
            ViewState["StudentDetails"] = dt;
            //bind Gridview   
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        private void AddNewRecordRowToGrid()
        {
            // check view state is not null   
            if (ViewState["StudentDetails"] != null)
            {
                //get datatable from view state   
                DataTable dtCurrentTable = (DataTable)ViewState["StudentDetails"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        //add each row into data table   
                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["Name"] = Name.Text;
                        drCurrentRow["Roll No"] = RollNo.Text;
                        drCurrentRow["Age"] = Age.Text;
                        drCurrentRow["Gender"] = Gender.Text;
                    }
                    //Remove initial blank row   
                    if (dtCurrentTable.Rows[0][0].ToString() == "")
                    {
                        dtCurrentTable.Rows[0].Delete();
                        dtCurrentTable.AcceptChanges();
                    }
                    //add created Rows into dataTable   
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    //Bind Gridview with latest Row   
                    GridView1.DataSource = dtCurrentTable;
                    GridView1.DataBind();
                }
            }
        }
        protected void AddStudentDetails_Click(object sender, EventArgs e)
        {
            AddNewRecordRowToGrid();
        }
        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Name", typeof(string)));
            dt.Columns.Add(new DataColumn("Roll No", typeof(string)));
            dt.Columns.Add(new DataColumn("Age", typeof(string)));
            dt.Columns.Add(new DataColumn("Gender", typeof(string)));
            string Name = string.Empty;
            string RollNo = string.Empty;
            string Age = string.Empty;
            string Gender = string.Empty;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow row = (GridViewRow)GridView1.Rows[i];
                if (i != e.RowIndex)
                {
                    Name = row.Cells[1].Text;
                    RollNo = row.Cells[2].Text;
                    Age = row.Cells[3].Text;
                    Gender = row.Cells[4].Text;
                }
                else
                {
                    Name = ((TextBox)row.Cells[1].Controls[0]).Text;
                    RollNo = ((TextBox)row.Cells[2].Controls[0]).Text;
                    Age = ((TextBox)row.Cells[3].Controls[0]).Text;
                    Gender = ((TextBox)row.Cells[4].Controls[0]).Text;
                }
                DataRow dr = dt.NewRow();
                dr["Name"] = Name;
                dr["Roll No"] = RollNo;
                dr["Age"] = Age;
                dr["Gender"] = Gender;
                dt.Rows.Add(dr);
            }
            GridView1.EditIndex = -1;
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("Name", typeof(string)));
                dt.Columns.Add(new DataColumn("Roll No", typeof(string)));
                dt.Columns.Add(new DataColumn("Age", typeof(string)));
                dt.Columns.Add(new DataColumn("Gender", typeof(string)));
                string Name = string.Empty;
                string RollNo = string.Empty;
                string Age = string.Empty;
                string Gender = string.Empty;
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    GridViewRow row = (GridViewRow)GridView1.Rows[i];
                    if (i != e.RowIndex)
                    {
                        Name = row.Cells[1].Text;
                        RollNo = row.Cells[2].Text;
                        Age = row.Cells[3].Text;
                        Gender = row.Cells[4].Text;
                        DataRow dr = dt.NewRow();
                        dr["Name"] = Name;
                        dr["Roll No"] = RollNo;
                        dr["Age"] = Age;
                        dr["Gender"] = Gender;
                        dt.Rows.Add(dr);
                    }
                }
                GridView1.EditIndex = -1;
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        }
        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            BindStudentDetails();
        }
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            BindStudentDetails();
        }
        private void BindStudentDetails()
        {
            if (ViewState["StudentDetails"] != null)
            {
                //get datatable from view state   
                DataTable dtCurrentTable = (DataTable)ViewState["StudentDetails"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        //add each row into data table   
                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["Name"] = Name.Text;
                        drCurrentRow["Roll No"] = RollNo.Text;
                        drCurrentRow["Age"] = Age.Text;
                        drCurrentRow["Gender"] = Gender.Text;
                    }
                    //Remove initial blank row   
                    if (dtCurrentTable.Rows[0][0].ToString() == "")
                    {
                        dtCurrentTable.Rows[0].Delete();
                        dtCurrentTable.AcceptChanges();
                    }
                    //Bind Gridview with latest Row   
                    GridView1.DataSource = dtCurrentTable;
                    GridView1.DataBind();
                }
            }
        }

		protected void Search(object sender, EventArgs e)
		{
            try
            {
                DataTable dtCurrentTable = (DataTable)ViewState["StudentDetails"];
                DataView dvCurrentTable = new DataView(dtCurrentTable);
                dvCurrentTable.RowFilter = String.Format("Name like '%" + txtSearch.Text + "%' ");
                GridView1.DataSource = dvCurrentTable;
                GridView1.DataBind();
                return;
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + ex.Message + "');", true);
            }
        }
	}
}