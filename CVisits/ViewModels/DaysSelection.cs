using System;
using System.ComponentModel.DataAnnotations;

namespace CVisits.ViewModels
{
    public class DaysSelection
    {        
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Must enter a Start Date")]
        DateTime StartDate { get; set; }

     
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Must enter an End Date")]
        DateTime EndDate { get; set; }
    }
}