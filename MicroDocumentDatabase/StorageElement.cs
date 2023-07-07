namespace MicroDocumentDatabase
{
    public class StorageElement : IStorageElement
    {
        public string? Name { get; set; }
        public object Value { get; set; }
        public Type Type { get; set; }

        public StorageElement()
        {
        }

        public StorageElement(string? Name, string? Value, Type type)
        {
            this.Name = Name;
            this.Value = Value;
            this.Type = type;
        }
    }

    public interface IStorageElement
    {
        string? Name { get; set; }
        object Value { get; set; }
        Type Type { get; set; }
    }
}
