
using System.Text.Json.Serialization;

namespace cms.models
{

    [JsonPolymorphic(
       TypeDiscriminatorPropertyName = "discriminator",
   UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToNearestAncestor)]

    [JsonDerivedType(typeof(ImageFile), typeDiscriminator: "ImageFile")]
    public class CmsFile
    {
	public int Id { get; set; }
	public string FileName { get; set; }
	public DateTime UploadDate { get; set; } = DateTime.UtcNow;
	public int FileFolderId { get; set; }
	public virtual FileFolder? FileFolder { get; set; }
    }
}
