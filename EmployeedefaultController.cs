using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SPANDANA_REST.Models;
using System.Web.Http.Cors;
using Mywebapi.Models;
namespace Mywebapi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EmployeedefaultController : ApiController
    {
        string connn = ConfigurationManager.ConnectionStrings["con"].ToString();
        [HttpGet]
        public HttpResponseMessage GetEmp()
        {
            try
            {

                using (SqlConnection config = new SqlConnection(connn))
                {
                    using (SqlCommand cmd = new SqlCommand("Usp_Employee_Details_new", config))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            using (DataSet ds = new DataSet())
                            {

                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.CommandTimeout = 0;
                                cmd.Parameters.AddWithValue("@Flag", "S");
                                da.Fill(ds);
                                if (ds != null && ds.Tables.Count > 0)
                                {

                                    return Request.CreateResponse<Details>(HttpStatusCode.OK, new Details
                                    {
                                        TotalDatils = ds.Tables[0].AsEnumerable().Select(Y => new Employee
                                        {
                                            Emp_Id = Convert.ToInt32(Y["Emp_Id"]),
                                            Emp_Name = Y["Emp_Name"].ToString(),
                                            Emp_salary = Y["Emp_salary"].ToString(),
                                            Emp_status = Y["Emp_status"].ToString(),
                                            Emp_Designation = Y["Emp_Designation"].ToString()
                                        }).ToList()
                                    });
                                }
                                else
                                {
                                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,common_Message.nodatafoud );
                                }



                            }

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }
        [HttpGet]
        public HttpResponseMessage GetLoginDetails( string username,string password,string userroll)
        {
            try
            {

                using (SqlConnection config = new SqlConnection(connn))
                {
                    using (SqlCommand cmd = new SqlCommand("Usp_EmployeeLoginDetails", config))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            using (DataSet ds = new DataSet())
                            {

                                var userolldata = Convert.ToInt32(userroll);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.CommandTimeout = 0;
                                cmd.Parameters.AddWithValue("@username",username);
                                cmd.Parameters.AddWithValue("@password",password);
                                cmd.Parameters.AddWithValue("@userroll",userroll);
                                da.Fill(ds);
                                if (ds != null && ds.Tables.Count > 0)
                                {
                                    return Request.CreateResponse(HttpStatusCode.OK, Newtonsoft.Json.JsonConvert.SerializeObject(ds));
                                }
                                else
                                {
                                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, common_Message.nodatafoud);
                                }
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }
        [HttpPost]

        public HttpResponseMessage Postemp([FromBody]  Employee objEmployee)
        {
            try
            {
                using (SqlConnection config = new SqlConnection(connn))
                {
                    using (SqlCommand cmd = new SqlCommand("Usp_Employee_Details_new", config))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            using (DataSet ds = new DataSet())
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.CommandTimeout = 0;
                                cmd.Parameters.AddWithValue("@Flag", "I");
                                cmd.Parameters.AddWithValue("@Emp_Name", objEmployee.Emp_Name);
                                cmd.Parameters.AddWithValue("@Emp_salary", objEmployee.Emp_salary);
                                cmd.Parameters.AddWithValue("@Emp_status", objEmployee.Emp_status);
                                cmd.Parameters.AddWithValue("@Emp_Designation", objEmployee.Emp_Designation);

                                config.Open();

                                int i = cmd.ExecuteNonQuery();
                                config.Close();
                                if (i == 1)
                                {

                                    return Request.CreateResponse(HttpStatusCode.OK, "Saved Successfully");
                                }
                                else
                                {
                                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Nodata");
                                }

                            }
                        }
                    }
                }
            }


            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }


        }
        [HttpGet]
        public HttpResponseMessage GetEmpiddetails([FromUri] int id)
        {
            ////localhost: 50450 / api / Employee / GetEmpiddetails ? id = 2
            try
            {
                using (SqlConnection cons = new SqlConnection(connn))
                {
                    using (SqlCommand cmds = new SqlCommand("Usp_Employee_Details_new", cons))
                    {
                        using (SqlDataAdapter dc = new SqlDataAdapter(cmds))
                        {
                            using (DataSet ds = new DataSet())
                            {
                                cmds.CommandType = CommandType.StoredProcedure;
                                cmds.CommandTimeout = 0;
                                cmds.Parameters.AddWithValue("@Flag", "L");
                                cmds.Parameters.AddWithValue("@Emp_Id", id);
                                dc.Fill(ds);
                                if (ds != null && ds.Tables.Count > 0)
                                {
                                    return Request.CreateResponse<Details>(HttpStatusCode.OK, new Details
                                    {
                                        TotalDatils = ds.Tables[0].AsEnumerable().Select(Y => new Employee
                                        {
                                            Emp_Id = Convert.ToInt32(Y["Emp_Id"]),
                                            Emp_Name = Y["Emp_Name"].ToString(),
                                            Emp_salary = Y["Emp_salary"].ToString(),
                                            Emp_status = Y["Emp_status"].ToString(),
                                            Emp_Designation = Y["Emp_Designation"].ToString()
                                        }).ToList()
                                    });
                                }
                                else
                                {
                                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Nodata");
                                }
                            }

                        }
                    }
                }


            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpDelete]
        public HttpResponseMessage DeleteEmp([FromUri] int id)
        {
            //localhost: 50450 / api / Employee / DeleteEmp ? id = 3
            try
            {
                using (SqlConnection cons1 = new SqlConnection(connn))
                {
                    using (SqlCommand cmds1 = new SqlCommand("Usp_Employee_Details_new", cons1))
                    {
                        using (SqlDataAdapter dc1 = new SqlDataAdapter(cmds1))
                        {
                            using (DataSet ds1 = new DataSet())
                            {
                                cmds1.CommandType = CommandType.StoredProcedure;
                                cmds1.CommandTimeout = 0;
                                cmds1.Parameters.AddWithValue("@Flag", "D");
                                cmds1.Parameters.AddWithValue("@Emp_Id", id);
                                dc1.Fill(ds1);
                                var ab = ds1.Tables[0].Rows[0]["Mesage"].ToString();
                                if (ds1 != null && ds1.Tables.Count > 0)
                                {
                                    return Request.CreateResponse(HttpStatusCode.OK, ab);
                                }
                                else
                                {
                                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Nodata");
                                }




                            }

                        }
                    }
                }


            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        public HttpResponseMessage PutEmp([FromBody]  Employee objEmployee)
        {
            try
            {
                using (SqlConnection config = new SqlConnection(connn))
                {
                    using (SqlCommand cmd = new SqlCommand("Usp_Employee_Details_new", config))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            using (DataSet ds = new DataSet())
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.CommandTimeout = 0;
                                cmd.Parameters.AddWithValue("@Flag", "U");
                                cmd.Parameters.AddWithValue("@Emp_Id", objEmployee.Emp_Id);
                                cmd.Parameters.AddWithValue("@Emp_Name", objEmployee.Emp_Name);
                                cmd.Parameters.AddWithValue("@Emp_salary", objEmployee.Emp_salary);
                                cmd.Parameters.AddWithValue("@Emp_status", objEmployee.Emp_status);
                                cmd.Parameters.AddWithValue("@Emp_Designation", objEmployee.Emp_Designation);

                                config.Open();

                                int i = cmd.ExecuteNonQuery();
                                config.Close();
                                if (i == 1)
                                {

                                    return Request.CreateResponse(HttpStatusCode.OK, "Updated Successfully");
                                }
                                else
                                {
                                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Nodata");
                                }

                            }
                        }
                    }
                }
            }


            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError,ex.Message);
                
            }

        }

        //userservice

       


    }
}
