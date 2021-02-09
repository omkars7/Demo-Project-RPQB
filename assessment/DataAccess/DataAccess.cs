//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Data;
//using System.Data.SqlClient;
//using System.Configuration;
//using CrudMVCADO.Models;

//namespace CrudMVCADO.DataAccess
//{
//    public class DataAccess
//    {
//        SqlConnection con = new SqlConnection("Data Source=DESKTOP-2NQS68P;Initial Catalog=EmployeeDB;Integrated Security=True");
//        public void Add_Record(Models.Employee emp)
//        {
//            SqlCommand com = new SqlCommand("InsertEmpDetail3", con);
//            com.CommandType = CommandType.StoredProcedure;
//            com.Parameters.AddWithValue("@empName", emp.empName);
//            con.Open();
//            com.ExecuteNonQuery();
//            con.Close();
//        }
//        public DataSet Show_Record()
//        {
//            SqlCommand com = new SqlCommand("Sp_index", con);
//            com.CommandType = CommandType.StoredProcedure;
//            SqlDataAdapter da = new SqlDataAdapter(com);
//            DataSet ds = new DataSet();
//            da.Fill(ds);
//            return ds;

//        }
//    }
//}
