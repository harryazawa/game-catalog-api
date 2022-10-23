using System.ComponentModel.DataAnnotations;

namespace GameCatalogApi.InputModel;

public class GameInputModel
{
    [Required]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Name of Game must have 3 to 100 characters")]
    public string Name { get; set; }
    [Required]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Name of Developer must have 3 to 100 characters")]
    public string Developer { get; set; }
    [Required]
    [Range(1, 1000, ErrorMessage = "Price must range from 1 to 1000 in current Currency")]
    public double Price { get; set; }
}