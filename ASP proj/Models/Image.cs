namespace ASP_proj.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Filename { get; set; }
        public string Url => "/uploads/" + Filename;
    }
}
