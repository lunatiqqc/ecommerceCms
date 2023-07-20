namespace cms.models

{
    public class GlobalHeader
    {
	public int Id { get; set; }
	public virtual GridContent? GridContent { get; set; }
    }
    public class GlobalFooter
    {
	public int Id { get; set; }

	public virtual GridContent? GridContent { get; set; }
    }

}
