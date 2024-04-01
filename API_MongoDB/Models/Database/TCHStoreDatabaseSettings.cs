
namespace API_MongoDB.Models.Database
{
    public class TCHStoreDatabaseSettings : ITCHStoreDatabaseSettings
    {
        Dictionary<string, string> ITCHStoreDatabaseSettings.TCHCoursesCollectionName { get; set; } = new Dictionary<string, string>();
        public string ConnectionString { get; set; } = String.Empty;
        public string DatabaseName { get; set; } = String.Empty;
    }
}
