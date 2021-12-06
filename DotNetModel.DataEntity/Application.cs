using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DotNetModel.DataEntity
{
    [Table("TB_APPL")]
    public class Application
    {
        [Key]
        [Column("ID_APPL")]
        public int Id { get; set; }

        [Column("URL_APPL")]
        public string Url { get; set; }

        [Column("PATH_LOCAL_APPL")]
        public string PathLocal { get; set; }

        [Column("DBG_MODE_APPL")]
        public bool DebuggingMode { get; set; }
    }
}
