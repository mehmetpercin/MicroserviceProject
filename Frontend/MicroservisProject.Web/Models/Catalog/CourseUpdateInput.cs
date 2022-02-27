using System.ComponentModel.DataAnnotations;

namespace MicroservisProject.Web.Models.Catalog
{
    public class CourseUpdateInput
    {
        public string Id { get; set; }
        [Display(Name = "İsim")]
        public string Name { get; set; }
        [Display(Name = "Fiyat")]
        public decimal Price { get; set; }
        [Display(Name = "Açıklama")]
        public string Description { get; set; }
        public string UserId { get; set; }
        public string Picture { get; set; }
        public FeatureViewModel Feature { get; set; }
        [Display(Name = "Kategori")]
        public string CategoryId { get; set; }
        [Display(Name = "Kurs Resmi")]
        public IFormFile PhotoFormFile { get; set; }
    }
}
