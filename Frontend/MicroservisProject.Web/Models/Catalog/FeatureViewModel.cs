using System.ComponentModel.DataAnnotations;

namespace MicroservisProject.Web.Models.Catalog
{
    public class FeatureViewModel
    {
        [Display(Name = "Süre")]
        public int Duration { get; set; }
    }
}
