using System.Text.RegularExpressions;
using System.Text;

namespace MicroDocumentDatabase
{
    public static class Serialize
    {
        static List<string> IgnoredElements = new List<string>() { "<?b", "<ro", "</ro", "\r\n" };
        static Dictionary<string, Type> TypeDictionary { get; set; } = BuildDictionary();
        static Dictionary<string, Type> BuildDictionary()
        {
            Dictionary<string, Type> o = new Dictionary<string, Type>();

            o.Add("System.String", typeof(string));
            //o.Add("String", typeof(string));
            //o.Add("string", typeof(string));
            o.Add("System.Guid", typeof(Guid));
            //o.Add("guid", typeof(Guid));
            //o.Add("Guid", typeof(Guid));
            o.Add("System.Boolean", typeof(bool));
            //o.Add("Boolean", typeof(bool));
            //o.Add("bool", typeof(bool));
            o.Add("System.Byte", typeof(Byte));
            //o.Add("Byte", typeof(Byte));
            //o.Add("byte", typeof(byte));
            o.Add("System.SByte", typeof(SByte));
            //o.Add("sbyte", typeof(sbyte));
            o.Add("System.Char", typeof(Char));
            //o.Add("Char", typeof(Char));
            //o.Add("char", typeof(char));
            o.Add("System.Decimal", typeof(decimal));
            //o.Add("Decimal", typeof(Decimal));
            //o.Add("decimal", typeof(decimal));
            o.Add("System.Double", typeof(double));
            //o.Add("Double", typeof(Double));
            //o.Add("double", typeof(double));
            o.Add("System.Single", typeof(Single));
            //o.Add("Single", typeof(Single));
            //o.Add("single", typeof(Single));
            o.Add("System.Int16", typeof(Int16));
            //o.Add("Int16", typeof(Int16));
            o.Add("int", typeof(int));
            o.Add("UInt16", typeof(UInt16));
            o.Add("IntPtr", typeof(IntPtr));
            o.Add("UIntPtr", typeof(UIntPtr));
            o.Add("Int64", typeof(Int64));
            o.Add("UInt64", typeof(UInt64));
            o.Add("short", typeof(Int16));
            o.Add("ushort", typeof(UInt16));
            o.Add("System.DateTime", typeof(DateTime));
            //o.Add("DateTime", typeof(DateTime));

            return TypeDictionary = o;
        }

        static string BOF = "<root>";
        static string EOF = "</root>";

        public static string SerializeDatabase<T>(List<T> elements)
        {
            var sb = new StringBuilder();

            //sb.AppendLine(Header);
            sb.AppendLine(BOF);

            foreach (var element in elements)
            {
                sb.AppendLine(element.Serializer());
            }
            sb.AppendLine(EOF);
            return sb.ToString();
        }

        public static string Serializer<T>(this T obj)
        {
            var sb = new StringBuilder();

            sb.AppendLine($"<record>");

            var props = obj.GetType().GetProperties();

            // add values
            foreach (var prop in props)
            {
                sb.AppendLine($"<{prop.Name} type='{prop.PropertyType}'>{prop.GetValue(obj, null)}</{prop.Name}>");
            }

            sb.AppendLine($"</record>");

            return sb.ToString();
        }

        public static Dictionary<string, T> DeSerializeDatabase<T>(string filename)
        {
            // declare dictionary
            var InMemoryDatabase = new Dictionary<string, T>();
            var eleDict = new Dictionary<string, IStorageElement>();

            // clean raw elements from list and bulk elements into grouped element records
            var Records = File.ReadAllLines(filename).ToList().PruneElements().BulkInto();
           
            // process record data
            for (var x = 0; x < Records.Count; x++)
            {
                eleDict = new Dictionary<string, IStorageElement>();

                // collect elements for the record 
                for (var i = 0; i < Records[x].Count; i++)
                {
                    var elementDetails = Records[x][i].GetElement();
                    eleDict.Add(elementDetails.Name, elementDetails);
                }

                var data = DeSerializer<T>(eleDict);
                InMemoryDatabase.TryAdd(x.ToString(), data);
            }
            return InMemoryDatabase;
        }

        public static byte[] BinarySerializer<T>(this T obj) => Encoding.UTF8.GetBytes(obj.Serializer<T>());

        public static T DeSerializer<T>(Dictionary<string, IStorageElement> element)
        {
            var o = (T)Activator.CreateInstance<T>();

            var props = o.GetType().GetProperties();

            foreach (var prop in props)
            {
                // match element name with property name set value
                if (element.TryGetValue(prop.Name, out var e))
                {
                    // type override
                    if (prop.PropertyType.Name == "Guid")
                    {
                        Guid result = new Guid(e.Value.ToString());
                        prop.SetValue(o, result);
                    }
                    else
                    if (prop.PropertyType.Name == "DateTime")
                    {
                        if (DateTime.TryParse(e.Value.ToString(), out DateTime r))
                        {
                            prop.SetValue(o, r);
                        }
                    }
                    else
                    {
                        prop.SetValue(o, e.Value);
                    }
                }
            }
            return o;
        }

        static IStorageElement GetElement(this string element)
        {
            // learn element 
            var FullElement = new Regex(@"(<[a-zA-Z].+>)([a-zA-Z0-9 -\/:.]+)(<\/[a-zA-Z]+>)").Match(element).Groups;
            // get element name
            var ElementName = FullElement[0].Value.Split(" ")[0].Replace("<", "");
            // remove inner elementnames and brakets
            ElementName = ElementName.Replace("<", "").Replace(">", "");
            // seperate element
            var ElementNameSplit = ElementName.Split(" ");
            // element inner type
            var ElementStringType = FullElement[1].Value.Split(" ")[1].Replace("<", "").Replace(">", "");
            // element inner type 
            var ElementType = typeof(Nullable);
            // cast to type
            if (ElementStringType != null && ElementStringType.Length > 0)
            {
                var ActualTypeString = ElementStringType.GetElementType();
                ElementType = Type.GetType(ActualTypeString);
            }

            // element value of <element>
            var ElementValue = FullElement[2].Value;
            // return new storage element
            return new StorageElement(ElementName, ElementValue, ElementType);
        }

        static string GetElementType(this string typename)
        {
            if (typename == null) return " ";

            // clean up the string and get the inner type string name
            if (typename.StartsWith("type"))
            {
                typename = typename.Replace("type='", "").Replace("'", "");
            }

            if (TypeDictionary.TryGetValue(typename, out var t))
                return t.ToString();
            else
            {
                //Debug.WriteLine(typename);
                throw new Exception("type not known to element type database");
            }

        }

        static string GetElementTypeString(this string elementstring)
        {
            var elementsplit = elementstring.Split('\'');

            // if well formed inner element
            if (elementsplit.Length == 3)
                return elementsplit[1];
            else
                return elementstring;
        }

        public static List<List<string>> BulkInto(this List<string> elements)
        {
            var Records = new List<List<string>>();
            var InnerList = new List<string>();

            for (var ndx = 0; ndx < elements.Count;)
            {
                var info = elements[ndx];

                if (elements[ndx].StartsWith($"<record>"))
                {
                    InnerList = new List<string>();
                }
                else
                if (elements[ndx].StartsWith($"</record>"))
                {
                    Records.Add(InnerList);
                }
                else
                if (!elements[ndx].StartsWith($"<record>"))
                {
                    InnerList.Add(info);
                }
                ndx++;
            }

            return Records;
        }

        public static List<string> PruneElements(this List<string> elements)
        {
            for (var ndx = elements.Count - 1; ndx >= 0; ndx--)
            {
                for (var i = 0; i < IgnoredElements.Count; i++)
                {
                    // check ignored and null line check
                    if (elements[ndx].StartsWith(IgnoredElements[i]) || elements[ndx].Length == 0)
                    {
                        elements.RemoveAt(ndx);
                        break;
                    }
                }
            }
            return elements;
        }

    }
}
