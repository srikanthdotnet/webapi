using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http.Cors;
using System.Configuration;
using Mywebapi.Models;
using SPANDANA_REST.Models;

namespace Mywebapi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class studentsuserController : ApiController
    {
        string connn = ConfigurationManager.ConnectionStrings["con"].ToString();
        [HttpGet]

        public HttpResponseMessage Getstudent()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connn))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_students_user", con))
                    {
                        using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
                        {
                            using (DataSet ds = new DataSet())
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.CommandTimeout = 0;
                                cmd.Parameters.AddWithValue("@Flag", "S");
                                dd.Fill(ds);
                                if (ds != null && ds.Tables.Count > 0)
                                {
                                    return Request.CreateResponse<Detailsstart>(HttpStatusCode.OK, new Detailsstart
                                    {
                                        Studenttotal = ds.Tables[0].AsEnumerable().Select(Y => new Student
                                          {
                                              id = Convert.ToInt32(Y["id"]),
                                              Name = Y["Name"].ToString()

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
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        public HttpResponseMessage GetEmpiduserdetails([FromUri] string username, [FromUri] string Password, [FromUri] string userroll)
        {
            ////localhost: 50450 / api / Employee / GetEmpiddetails ? id = 2
            try
            {
                using (SqlConnection cons = new SqlConnection(connn))
                {
                    using (SqlCommand cmds = new SqlCommand("usp_students_user", cons))
                    {
                        using (SqlDataAdapter dc = new SqlDataAdapter(cmds))
                        {
                            using (DataSet ds = new DataSet())
                            {
                                cmds.CommandType = CommandType.StoredProcedure;
                                cmds.CommandTimeout = 0;
                                cmds.Parameters.AddWithValue("@Flag", "I");
                                cmds.Parameters.AddWithValue("@username", username);
                                cmds.Parameters.AddWithValue("@pwd", Password);
                                dc.Fill(ds);
                                if (ds != null && ds.Tables.Count > 0)
                                {
                                    return Request.CreateResponse<Detailsstartend>(HttpStatusCode.OK, new Detailsstartend
                                    {
                                        usersrolestotal = ds.Tables[0].AsEnumerable().Select(Y => new usersroles
                                        {

                                            username = Y["username"].ToString(),
                                            Password = Y["Password"].ToString(),
                                            userroll = Y["userroll"].ToString()
                                            //Emp_Designation = Y["Emp_Designation"].ToString()
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



        

    }
}
