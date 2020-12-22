namespace _017Dapper.Model
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string ClassId { get; set; }
    }

    //这里为使用Dapper执行存储过程插入，
    //但是没必要，已经使用了匿名对象
    public class Person2
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string ClassId { get; set; }
    }
}
