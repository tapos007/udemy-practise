using DLL.Model.Interfaces;

namespace DLL.Model
{
    public class Department : ISoftDeletable
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}