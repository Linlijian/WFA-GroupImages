using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace GILibrary
{
   public class FSLibrary
    {
        public FormState state;

        public FSLibrary()
        {
            state = new FormState();
        }

        public void loadConfig()
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "config.xml";
            XmlSerializer ser = new XmlSerializer(typeof(FormState));
            try
            {
                using (FileStream fs = File.OpenRead("config.xml"))
                {
                    state = (FormState)ser.Deserialize(fs);
                }
            }
            catch
            {
                using (XmlWriter writer = XmlWriter.Create("config.xml"))
                {
                    writer.WriteStartElement("FormState");
                    writer.WriteElementString("isDisableMsg", "False");
                    writer.WriteElementString("isSelectSingle", "False");
                    writer.WriteElementString("isSorting", "False");
                    writer.WriteElementString("isMulti", "False");
                    writer.WriteEndElement();
                    writer.Flush();
                }
            }
        }

        public void writeConfig(bool chkSelect, bool chkDisable, bool chkSort, bool chkMulti)
        {
            using (StreamWriter sw = new StreamWriter("config.xml"))
            {
                state.isDisableMsg = chkDisable;
                state.isSelectSingle = chkSelect;
                state.isSorting = chkSort;
                state.isMulti = chkMulti;
                XmlSerializer ser = new XmlSerializer(typeof(FormState));
                ser.Serialize(sw, state);
            }
        }
    }
}
