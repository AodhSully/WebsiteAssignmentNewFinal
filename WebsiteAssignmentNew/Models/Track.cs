using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteAssignmentNew.Models
{
    public class Track
    {
        public string TrackCode { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }        
        public int Quantity { get; set; }
        //public decimal PriceDownload { get; set; }
        //public decimal PriceCD { get; set; }
        public decimal PriceVinyl { get; set; }
        public string Artwork { get; set; }
        public string TrackAudio { get; set; }       
    }

  
}