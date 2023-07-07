namespace TestCA
{
    public class Doc : IDoc
    {
        public string id { get; set; }

        public Guid guid { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public string content { get; set; }

        public DateTime created { get; set; }
    }

    public interface IDoc
    {
        string id { get; set; }
        Guid guid { get; set; }
        string name { get; set; }
        string description { get; set; }
        string content { get; set; }
        DateTime created { get; set; }
    }
}
