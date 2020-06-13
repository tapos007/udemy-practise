using DLL.Model.Interfaces;

namespace DLL.Model
{
    public class Student : ISoftDeletable
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        
    }
}