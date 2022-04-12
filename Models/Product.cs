using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Project1.Models
{
    public class Product
    {


        public int Id { get; set; }
        public string externalId { get; set; }


        [MinLength(5, ErrorMessage = "Code should be 5 characters at least")]
        [MaxLength(10, ErrorMessage = "Code can not be more than 10 characters")]
        public string code { get; set; }

        [DescriptionValidation(10)]
        public string description { get; set; }

        public string name { get; set; }
        public string barcode { get; set; }

        public string retailPrice { get; set; }

        public string wholesalePrice { get; set; }

        public string discount { get; set; }



    }
}