using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Senac.Fecomercio.Common
{
    public class StrReader
    {
        public class Field
        {
            public string Name { get; set; }

            public string Value { get; set; }

            public int Length { get; set; }

            public int Start { get; set; }
        }

        public static List<Field> buscaCampos(string mappingFile)
        {
            List<Field> fields = new List<Field>();

            XmlDocument map = new XmlDocument();

            map.Load(mappingFile);

            XmlNodeList fieldNodes = map.SelectNodes("/StringMap/Field");

            if (fieldNodes != null)
            {
                foreach (XmlNode fieldNode in fieldNodes)
                {
                    Field field = new Field
                    {
                        Name = fieldNode.Attributes["Name"].Value,
                        Length = Convert.ToInt32(fieldNode.Attributes["Length"].Value),
                        Start = Convert.ToInt32(fieldNode.Attributes["Start"].Value)
                    };

                    fields.Add(field);
                }
            }

            return fields;
        }
    }
}
