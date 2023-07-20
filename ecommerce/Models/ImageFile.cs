
namespace cms.models
{

    public enum ImageSizes
    {
	w120 = 120,
	w240 = 240,
	w480 = 480,
	w768 = 768,
	w1024 = 1024,
	w1920 = 1920,
	w3840 = 3840
    }
    public class ImageFile : CmsFile
    {

	public float AspectRatio { get; set; }

	public List<ImageSizes> Sizes { get; set; }
    }
}
