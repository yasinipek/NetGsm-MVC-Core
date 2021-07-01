using System.Xml.Serialization;

namespace NetGsm
{
	

	[XmlRoot(ElementName = "msg")]
	public class Msg
	{
		private string _name; // field

		[XmlElement(ElementName = "gonderen")]
		public double Gonderen { get; set; }

		[XmlElement(ElementName = "mesaj")]
		public string Mesaj { get; set; }

        [XmlElement(ElementName = "tarih")]
        public string Tarih  // property
        {
            get { return _name.Trim(); }
            set { _name = value; }
        }


        [XmlElement(ElementName = "operator")]
		public string Operator { get; set; }

		[XmlElement(ElementName = "gorevid")]
		public int Gorevid { get; set; }
	}

	[XmlRoot(ElementName = "mainbody")]
	public class Mainbody
	{

		[XmlElement(ElementName = "msg")]
		public Msg Msg { get; set; }
	}
}
