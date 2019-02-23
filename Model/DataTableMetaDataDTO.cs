using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class DataTableMetaDataDTO
    {
        public List<ClassTeacherDto> Data { get; set; }
    }

    public class TableDataDTO
    {
        public List<StudentRowDto> Data { get; set; }

        public int Id { get; set; }
    }
}
