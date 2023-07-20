
namespace cms.models
{
    public class FileFolder
    {
	public int Id { get; set; }
	public string Name { get; set; }
	public int? ParentFolderId { get; set; }
	public virtual FileFolder? ParentFolder { get; set; }
	public virtual ICollection<FileFolder>? Subfolders { get; set; }
	public virtual ICollection<CmsFile>? Files { get; set; }
    }
}
