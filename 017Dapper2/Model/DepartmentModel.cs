using Dapper.Contrib.Extensions;

namespace _017Dapper2
{
    [Table("Department")]
    public class DepartmentModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
