using JoExport.Domain.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoExport.Domain.Model
    {
    public class Category
        {
        public int Id { get; set; }
        [Required]
        public Categorys Name { get; set; }
        [Required]
        [RegularExpression(@"\b\w+\.(jpg|JPG|PNG|png)\b", ErrorMessage = "This Image not supported")]
        public string CategoryImg { get; set; }
        }
    }
