using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CrudMVCADO.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CrudMVCADO.Controllers
{
    public class EmpDetail1Controller : Controller
    {
        
        SqlConnection con = new SqlConnection("Server = DESKTOP-2NQS68P; Database = EmployeeDB1; integrated security =True");

        public ActionResult ListEmployeeDetail1()
        {
            DataTable dt = new DataTable();
            using (con)
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SP_ListEmployeeDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader sda = cmd.ExecuteReader();
                    dt.Load(sda);

                    

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);

                }

            }

            return View(dt);
        }


        public ActionResult onclick(int id)
        {
            EmployeeDetail1 ed = new EmployeeDetail1();
            DataTable dt = new DataTable();
            using (con)
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("onclick", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("empid", id);

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    cmd.ExecuteNonQuery();
                    sda.Fill(dt);

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);

                }

            }
            return View(dt);
        }

        [HttpGet]
        public ActionResult CreateEmployeeDetail()
        {
            return View(new EmployeeDetail1());
        }


        [HttpPost]

        public ActionResult CreateEmployeeDetail(EmployeeDetail1 ed)
        {
            DataTable dt = new DataTable();
            using (con)
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SP_CreateEmpDetail", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);

                    cmd.Parameters.AddWithValue("@empId", ed.empId);
                    cmd.Parameters.AddWithValue("@dOB", ed.dOB);
                    cmd.Parameters.AddWithValue("@dOJ", ed.dOJ);
                    cmd.Parameters.AddWithValue("@designation", ed.designation);
                    cmd.Parameters.AddWithValue("@degree", ed.degree);
                    cmd.Parameters.AddWithValue("@passOutYear", ed.passOutYear);


                    cmd.ExecuteNonQuery();
                    sda.Fill(dt);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);

                }

            }
            return RedirectToAction("ListEmployeeDetail1");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
   


        public ActionResult Edit(int id)
        {
            EmployeeDetail1 ed = new EmployeeDetail1();
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("EMPDETSIL", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.SelectCommand.Parameters.AddWithValue("@empDetailId", id);
                sda.Fill(dt);
                if (dt.Rows.Count == 1)
                {



                    ed.empDetailId = Convert.ToInt32(dt.Rows[0][0]);


                    ed.dOB = Convert.ToString(dt.Rows[0][2]);
                    ed.dOJ = Convert.ToString(dt.Rows[0][3]);
                   
                    ed.designation = dt.Rows[0][3].ToString();
                    ed.degree = dt.Rows[0][4].ToString();
                    ed.passOutYear = dt.Rows[0][5].ToString();




                    return View(ed);
                }
                else
                {

                    return RedirectToAction("Edit");
                }


            }
            catch (SqlException se)
            {
                return View();

            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmployeeDetail1 ed)
        {
            DataTable dt = new DataTable();
            using (con)
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SP_EditEmpDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);

                    cmd.Parameters.AddWithValue("@empid", ed.empId);
                    cmd.Parameters.AddWithValue("@dOB", ed.dOB);
                    cmd.Parameters.AddWithValue("@dOJ", ed.dOJ);
                    cmd.Parameters.AddWithValue("@designation", ed.designation);
                    cmd.Parameters.AddWithValue("@degree", ed.degree);
                    cmd.Parameters.AddWithValue("@passOutYear", ed.passOutYear);


                    cmd.ExecuteNonQuery();
                    sda.Fill(dt);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);

                }
            }
            return RedirectToAction("ListEmployeeDetail1");
        }

        
        public ActionResult Delete(int id)
        {
            using (con)
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SP_DeleteEmpDetail", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    cmd.Parameters.AddWithValue("@empDetailid", id);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);

                }

            }

            return RedirectToAction("ListEmployeeDetail1");
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

