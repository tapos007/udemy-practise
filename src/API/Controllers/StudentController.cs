using System.Collections.Generic;
using System.Linq;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    
    public class StudentController : MainApiController
    {
        [HttpGet]
        public IActionResult GetAll([FromQuery] string rollNumber, [FromQuery] string nickName)
        {
            return Ok(StudentStatic.GetAllStudent());
        }

        [HttpGet("{email}")]
        public IActionResult GetA(string email)
        {
            return Ok(StudentStatic.GetAStudent(email));
        }
        
        
        [HttpPost]
        public IActionResult Insert([FromForm] Student department)
        {
            return Ok(StudentStatic.InsertStudent(department));
        }
        
        [HttpPut("{code}")]
        public IActionResult Update(string code,Student department)
        {
            return Ok(StudentStatic.UpdateStudent(code,department));
        }
        
        [HttpDelete("{code}")]
        public IActionResult Delete(string code)
        {
            return Ok(StudentStatic.DeleteStudent(code));
        }
    }
    
    
    public static class StudentStatic
    {

        private static List<Student> AllStudent { get; set; } = new List<Student>();

        public static Student InsertStudent( Student department)
        {
            AllStudent.Add(department);
            return department;
        }

        public static List<Student> GetAllStudent()
        {
            return AllStudent;
        }

        public static Student GetAStudent(string email)
        {
            return AllStudent.FirstOrDefault(x => x.Email == email);
        }

        public static Student UpdateStudent(string email, Student department)
        {
            Student result = new Student();
            foreach (var aStudent in AllStudent)
            {
                if (email == aStudent.Email)
                {
                    aStudent.Name = department.Name;
                    result = aStudent;
                }
                
            }

            return result;
        }

        public static Student DeleteStudent(string email)
        {
            var student = AllStudent.FirstOrDefault(x => x.Email == email);
            AllStudent = AllStudent.Where(x => x.Email != student.Email).ToList();
            return student;
        }
    }
}