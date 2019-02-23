using System;

namespace Model
{
    public class StudentRowDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName
        {
            get { return FirstName +" " + LastName; }
        }

        public int Age { get; set; }

        public double Gpa { get; set; }
    }

}
