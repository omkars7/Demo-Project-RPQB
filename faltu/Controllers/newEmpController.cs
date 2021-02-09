using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using CrudMVCADO.Models;
namespace CrudMVCADO.Controllers
{
    public class newEmpController : Controller
    {
        SqlConnection con = new SqlConnection("Server = DESKTOP-2NQS68P; Database = EmployeeDB1; integrated security =True");
        // GET: newEmpController
        [HttpGet]
        public ActionResult ListEmployee()
        {
            DataTable dt = new DataTable();
            using (con)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("ListEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);

            }
            return View(dt);
        }


        public ActionResult Create()
        {
            return View(new Employee());
        }


        [HttpPost]

        public ActionResult Create(Employee emp, Department dept)
        {
            DataTable dt = new DataTable();
            using (con)
            {

                con.Open();
                SqlCommand cmd = new SqlCommand("Sp_InsertEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                //cmd.Parameters.AddWithValue("@empId", emp.empId);
                cmd.Parameters.AddWithValue("@deptId",emp.deptId );
                cmd.Parameters.AddWithValue("@empName", emp.empName);
                cmd.ExecuteNonQuery();
                sda.Fill(dt);
            }
            return RedirectToAction("ListEmployee");
        }

        public ActionResult Edit(int id)
        {
            Employee e = new Employee();
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("EMP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("@empId", id);
                //sda.SelectCommand.Parameters.AddWithValue
                sda.Fill(dt);

                if (dt.Rows.Count == 1)
                {
                    e.empId = Convert.ToInt32(dt.Rows[0][0]);
                    e.empName = dt.Rows[0][1].ToString();
                    e.deptId = Convert.ToInt32(dt.Rows[0][2]);

                    return View(e);
                }
                else
                {
                    return RedirectToAction("Edit");
                }


            }
            catch(SqlException se)
            {
                return View();
            }
            
        }

        // POST: DepartmentController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee e)
        {
            DataTable dt = new DataTable();
            using (con)
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SP_EDITEMP", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    cmd.Parameters.AddWithValue("@empId", e.empId);
                    cmd.Parameters.AddWithValue("@empName", e.empName);
                    cmd.Parameters.AddWithValue("@deptId", e.deptId);

                    cmd.ExecuteNonQuery();
                    sda.Fill(dt);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return RedirectToAction("ListEmployee");
            }
        }

        public ActionResult Delete(int id)
        {
            using(con)
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SP_DELETEEMP", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    cmd.Parameters.AddWithValue("@empId", id);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return RedirectToAction("ListEmployee");
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

