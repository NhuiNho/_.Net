namespace API_MongoDB.Models.Database
{
    public interface ITCHStoreDatabaseSettings
    {
        Dictionary<string, string> TCHCoursesCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
