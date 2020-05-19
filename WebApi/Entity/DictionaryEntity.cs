using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entity
{
    [Table("zf_dictionary")]
    public class DictionaryEntity
    {
        public int Id { get; set; }
        public int PId { get; set; }
        public string Name { get; set; }

        public int Sort { get; set; }

        public int Value { get; set; }

        public string Enable { get; set; }

        public DateTime AddTime { get; set; }

        public string AddUserId { get; set; }

    }
}
