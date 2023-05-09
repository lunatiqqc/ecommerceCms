using System.Text.Json.Serialization;
namespace cms.Models
{
    public class Page
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Url { get; set; }
        public virtual List<Page>? Children { get; set; }
        [JsonIgnore]
        public virtual Page? Parent { get; set; }
        [JsonIgnore]
        public bool VisibleInMenu { get; set; }
        [JsonIgnore]
        public Users.UserRoles? RequiredRole { get; set; }
        [JsonIgnore]
        public bool IsSystemPage { get; set; }
    }
}
