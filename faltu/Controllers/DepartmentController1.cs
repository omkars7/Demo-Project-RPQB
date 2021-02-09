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
    public class DepartmentController1 : Controller
    {
        SqlConnection con = new SqlConnection("Server = DESKTOP-2NQS68P; Database = EmployeeDB1; integrated security =True");

        public ActionResult ListDepartment()
        {
            DataTable dt = new DataTable();
            using (con)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("ListDepartment", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);

            }
            return View(dt);

        }


        public ActionResult CreateDepartment()
        {
            return View(new Department());
        }


        [HttpPost]

        public ActionResult CreateDepartment(Department dept)
        {
            DataTable dt = new DataTable();
            using (con)
            {
                try
                {


                    con.Open();
                    SqlCommand cmd = new SqlCommand("SP_CreateDepartment", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    //cmd.Parameters.AddWithValue("@Empid", ed.Empid);
                    // cmd.Parameters.AddWithValue("@Deptid", dept.Deptid);
                    cmd.Parameters.AddWithValue("@DeptName", dept.deptName);



                    cmd.ExecuteNonQuery();
                    sda.Fill(dt);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);

                }
            }
            return RedirectToAction("ListDepartment");
        }

        // GET: DepartmentController1/Edit/5
        public ActionResult Edit(int id)
        {
            Department d = new Department();
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DEPT", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.SelectCommand.Parameters.AddWithValue("@deptid", id);
                sda.Fill(dt);

                if (dt.Rows.Count == 1)
                {



                    d.deptId = Convert.ToInt32(dt.Rows[0][0]);
                    d.deptName = dt.Rows[0][1].ToString();




                    return View(d);
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

        // POST: DepartmentController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Department d)
        {
            using (con)
            {

                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SP_EDITDEPT", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    //cmd.Parameters.AddWithValue("@Empid", ed.Empid);
                    cmd.Parameters.AddWithValue("@deptid", d.deptId);
                    cmd.Parameters.AddWithValue("@deptName", d.deptName);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);

                }



            }
            return RedirectToAction("ListDepartment");
        }

        // GET: DepartmentController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DepartmentController1/Delete/5
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