using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaPlace.Core.Models
{

    [Table("Files", Schema = "User")]
    public class File
    {
        [Key]
        public int FileId { get; set; }
        public string FileName { get; set; }

        public string FileDestination { get; set; }

        public string TempName { get; set; }

        public string TempDestination { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        public string Remarks { get; set; }

        public DateTime? UploadedDate { get; set; }

        [ForeignKey("UploadedBy")]
        public string UploadedById { get; set; }
        public virtual Administrator UploadedBy { get; set; }
    }
}
