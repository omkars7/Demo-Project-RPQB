using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using CrudMVCADO.Models;
using CrudMVCADO.Controllers;

namespace CrudMVCADO.Controllers
{
    public class BankController : Controller
    {
        SqlConnection con = new SqlConnection("Server = DESKTOP-2NQS68P; Database = EmployeeDB1; integrated security =True");
        public ActionResult ListBankDetail()
        {
            DataTable dt = new DataTable();

            using (con)
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Sp_ShowBankDetails", con);


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


        public ActionResult Details(int id)
        {
            return View();
        }

        
        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BankDetail bd)
        {
            DataTable dt = new DataTable();
            using (con)
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SP_InsertBankDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    cmd.Parameters.AddWithValue("@empid", bd.empId);
                    ;
                    cmd.Parameters.AddWithValue("@bankName", bd.bankName);
                    cmd.Parameters.AddWithValue("@accNo", bd.accNo);
                    cmd.Parameters.AddWithValue("@basicSal", bd.basicSal);;
                    cmd.Parameters.AddWithValue("@hRA", bd.hRA);
                    cmd.Parameters.AddWithValue("@otherAllowances", bd.otherAllowances);




                    cmd.Parameters.AddWithValue("@pF", bd.pF);
                    cmd.Parameters.AddWithValue("@medicalPremium", bd.medicalPremium);
                    cmd.Parameters.AddWithValue("@tDS", bd.tDS);

                    cmd.ExecuteNonQuery();
                    sda.Fill(dt);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);

                }
            }
            return RedirectToAction("ListBankDetail");
        }


        public ActionResult Edit(int id)
        {
            BankDetail bd = new BankDetail();
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("BANKEDIT", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.SelectCommand.Parameters.AddWithValue("@bankId", id);
                sda.Fill(dt);
                if (dt.Rows.Count == 1)
                {



                    bd.bankId = Convert.ToInt32(dt.Rows[0][0]);

                    bd.bankName = dt.Rows[0][1].ToString();


                    bd.accNo = Convert.ToInt32(dt.Rows[0][2]);
                    bd.basicSal = Convert.ToInt32(dt.Rows[0][3]);
                    bd.hRA = Convert.ToInt32(dt.Rows[0][4]);
                    bd.otherAllowances = Convert.ToInt32(dt.Rows[0][5]);

                    bd.pF = Convert.ToInt32(dt.Rows[0][6]);
                    bd.medicalPremium = Convert.ToInt32(dt.Rows[0][7]);
                    bd.tDS = Convert.ToInt32(dt.Rows[0][8]);



                    return View(bd);
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



            return View(new BankDetail());
        }
    

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BankDetail bd)
        {
            using (con)
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SP_EDITBANK", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    cmd.Parameters.AddWithValue("@empid", bd.empId);
                    cmd.Parameters.AddWithValue("@bankName", bd.bankName);
                    cmd.Parameters.AddWithValue("@accNo", bd.accNo);
                    cmd.Parameters.AddWithValue("@basicSal", bd.basicSal);
                    cmd.Parameters.AddWithValue("@hRA", bd.hRA);
                    cmd.Parameters.AddWithValue("@otherAllowances", bd.otherAllowances);



                    cmd.Parameters.AddWithValue("@pF", bd.pF);
                    cmd.Parameters.AddWithValue("@medicalPremium", bd.medicalPremium);
                    cmd.Parameters.AddWithValue("@tDS", bd.tDS);



                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);

                }
            }
            return RedirectToAction("ListBankDetail");
        }

        
        public ActionResult Delete(int id)
        {
            using (con)
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SP_Delete_Bank", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    cmd.Parameters.AddWithValue("@bankId", id);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);

                }

            }

            return RedirectToAction("ListBankDetail");
        }


        public ActionResult ShowNetSalary(int id)
        {
            DataTable dt = new DataTable();

            using (con)
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("ShowNetSal", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    cmd.Parameters.AddWithValue("@empDetailId", id);
                    sda.Fill(dt);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);

                }

            }
            return View(dt);
        }


        public ActionResult ShowGrossSalary(int id)
        {
            DataTable dt = new DataTable();

            using (con)
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("ShowGross", con);


                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    cmd.Parameters.AddWithValue("@empDetailId", id);
                    sda.Fill(dt);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);

                }

            }
            return View(dt);
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














