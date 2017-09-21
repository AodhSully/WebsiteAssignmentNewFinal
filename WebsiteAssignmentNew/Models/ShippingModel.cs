using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebsiteAssignmentNew.Models
{
    public class ShippingModel
    {
       
        [Required]
        //[RegularExpression("[A-Za-z]+")]
        public string Address1 { get; set; }

       
        [Required]
        //[RegularExpression("[A-Za-z]+")]
        public string Address2 { get; set; }


        [Required]
        //[RegularExpression("[A-Za-z]+")]
        public string Address3 { get; set; }

        

        [Required]
        //[RegularExpression("[A-Za-z]+")]
        public string City { get; set; }

       

        [Required]
        //[RegularExpression("[A-Za-z]+")]
        public string Country { get; set; }

        
        [Required]
        //[RegularExpression("[A-Za-z]+")]
        public string PostCode { get; set; }


    }
}