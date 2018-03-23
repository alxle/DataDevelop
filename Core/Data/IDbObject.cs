namespace DataDevelop.Data
{
	public interface IDbObject
	{
		Database Database { get; }

		string Name { get; }
	}
}
