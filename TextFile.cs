using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lab4
{
    [Serializable]
    class TextFile : IOriginator
    {
        public string Path { get; set; }
        public string Content { get; set; }

        public void Serialize(FileStream fs)
        {
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, this);
            fs.Flush();
            fs.Close();
        }
        public void Deserialize(FileStream fs)
        {
            BinaryFormatter bf = new BinaryFormatter();
            TextFile deserialized = (TextFile)bf.Deserialize(fs);
            Path = deserialized.Path;
            Content = deserialized.Content;
            //можно дописать еще свойства
            fs.Close();
        }
        public void XmlSerializer(XmlSerializer serializer)
        {
            XmlSerializer bf = new XmlSerializer(this.GetType());
        }
        object IOriginator.GetMemento()
        {
            return new Memento { Content = this.Content };
        }
        void IOriginator.SetMemento(object memento)
        {
            if (memento is Memento)
            {
                var mem = memento as Memento;
                Content = mem.Content;
            }
        }
    }
}
