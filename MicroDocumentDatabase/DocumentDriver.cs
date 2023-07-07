using System.Text;

namespace MicroDocumentDatabase
{
    public class DocumentDriver<T> where T : class
    {
        public Dictionary<string, T> InMemoryDocuments = new Dictionary<string, T>();

        public void Dump() => File.WriteAllText(GetFileName(), Serialize.SerializeDatabase(InMemoryDocuments.Values.ToList()));

        public void Load() => InMemoryDocuments = Serialize.DeSerializeDatabase<T>(GetFileName());

        static string GetFileName() => string.Concat(AppDomain.CurrentDomain.BaseDirectory, @$"\MicroDatabase_{typeof(T).ToString().Split(".").LastOrDefault()}.data");

        public static string GetUniqueId()
        {
            // get unique id
            var builder = new StringBuilder();

            Enumerable
               .Range(65, 26)
                .Select(e => ((char)e).ToString())
                .Concat(Enumerable.Range(97, 26).Select(e => ((char)e).ToString()))
                .Concat(Enumerable.Range(0, 10).Select(e => e.ToString()))
                .OrderBy(e => Guid.NewGuid())
                .Take(11)
                .ToList().ForEach(e => builder.Append(e));
            string id = builder.ToString();

            return id;
        }

        public T Get(string id)
        {
            if (InMemoryDocuments.TryGetValue(id, out var e))
            {
                return e;
            }
            else
                throw new Exception("Document not found");
        }

        public void Add(T rec) => InMemoryDocuments.Add(GetUniqueId(), rec);

        public void Delete(string id) => InMemoryDocuments.Remove(id);

        public void Save() => Serialize.SerializeDatabase<T>(InMemoryDocuments.Values.ToList());

        public void Update(string id, T rec)
        {
            if (InMemoryDocuments.TryGetValue(id, out var r))
            {
                InMemoryDocuments[id] = rec;
            }
            else
            {
                throw new Exception("Document does not exist");
            }
        }

    }
}