using System.Xml.Serialization;

namespace ElectronicStore
{
    public abstract class BaseModel
    {
        [XmlIgnore]
        public int? Id { get; set; } = null;
    }
}
