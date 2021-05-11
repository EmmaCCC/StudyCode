using Common;

namespace Model
{
    public class Person
    {
        [ExcelColumn("姓名")]
        public string Name { get; set; }
        [ExcelColumn("年龄")]
        public string Age { get; set; }
        [ExcelColumn]
        public string Gender { get; set; }
    }
}
