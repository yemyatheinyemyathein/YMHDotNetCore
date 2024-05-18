namespace YMHDotNetCore.RestApiWithNLayer.Models;

[Table("tbl_Blog")]
public class BlogModel
{
    [Key]
    public int BlogId { get; set; }
    public string? BlogTitle { get; set; }
    public string? BlogAuthor { get; set; }
    public string? BlogContent { get; set; }

}

//new form
//public record BlogEntity(int BlogId, string BlogTitle, string BlogAuthor, string BlogContent);
