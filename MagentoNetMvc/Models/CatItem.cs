using System;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MagentoNetMvc.Models
{
    public class CatItem
    {
        [DisplayFormat(NullDisplayText = "No value available!")]
        public int ID { get; set; }
        [DisplayFormat(NullDisplayText = "No value available!")]
        public string Name { get; set; }
        [DisplayFormat(NullDisplayText = "No value available!")]
        public string Title { get; set; }
        [DisplayFormat(NullDisplayText = "No value available!")]
        public string Description { get; set; }
    }
}

