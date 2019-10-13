using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Mywebapi.Models;
namespace Mywebapi.Controllers
{
    [RoutePrefix("Api/Employee")]
    public class EmployeeAPIController : ApiController
    {
        MIDLANDEntities objEntity = new MIDLANDEntities();

        [HttpGet]
        [Route("AllEmployeeDetails")]
        public List<EmployeeDetail> GetEmaployee()
        {
            try
            {

                List<EmployeeDetail> _obj = new List<EmployeeDetail>()
                {
                    new EmployeeDetail{EmpId=1,EmpName="a",Gender="male",Address="hyd",DateOfBirth=DateTime.Now,EmailId="a@gmail.com",PinCode="1234"},
                    new EmployeeDetail{EmpId=1,EmpName="b",Gender="female",Address="hyd1",DateOfBirth=DateTime.Now,EmailId="b@gmail.com",PinCode="1234"}
                };

                return _obj;




                //return objEntity.EmployeeDetails;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetEmployeeDetailsById/{employeeId}")]
        public IHttpActionResult GetEmaployeeById(string employeeId)
        {
            EmployeeDetail objEmp = new EmployeeDetail();
            int ID = Convert.ToInt32(employeeId);
            try
            {
                objEmp = objEntity.EmployeeDetails.Find(ID);
                if (objEmp == null)
                {
                    return NotFound();
                }

            }
            catch (Exception)
            {
                throw;
            }

            return Ok(objEmp);
        }

        [HttpPost]
        [Route("InsertEmployeeDetails")]
        public IHttpActionResult PostEmaployee(EmployeeDetail data)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                objEntity.EmployeeDetails.Add(data);
                objEntity.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }



            return Ok(data);
        }

        [HttpPut]
        [Route("UpdateEmployeeDetails")]
        public IHttpActionResult PutEmaployeeMaster(EmployeeDetail employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                EmployeeDetail objEmp = new EmployeeDetail();
                objEmp = objEntity.EmployeeDetails.Find(employee.EmpId);
                if (objEmp != null)
                {
                    objEmp.EmpName = employee.EmpName;
                    objEmp.Address = employee.Address;
                    objEmp.EmailId = employee.EmailId;
                    objEmp.DateOfBirth = employee.DateOfBirth;
                    objEmp.Gender = employee.Gender;
                    objEmp.PinCode = employee.PinCode;

                }
                int i = this.objEntity.SaveChanges();

            }
            catch (Exception)
            {
                throw;
            }
            return Ok(employee);
        }
        [HttpDelete]
        [Route("DeleteEmployeeDetails")]
        public IHttpActionResult DeleteEmaployeeDelete(int id)
        {
            //int empId = Convert.ToInt32(id);
            EmployeeDetail emaployee = objEntity.EmployeeDetails.Find(id);
            if (emaployee == null)
            {
                return NotFound();
            }

            objEntity.EmployeeDetails.Remove(emaployee);
            objEntity.SaveChanges();

            return Ok(emaployee);
        }
    }
}

