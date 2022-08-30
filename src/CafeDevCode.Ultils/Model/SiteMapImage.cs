using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CafeDevCode.Ultils.Model
{
    [XmlType(Namespace = "http://www.google.com/schemas/sitemap-image/1.1")]
    public class SiteMapImage
    {
        [XmlElement("loc")]
        public string Location { get; set; } = string.Empty;

        [XmlElement("caption")]
        public string Caption { get; set; } = string.Empty;
        public bool ShouldSerializeCaption => !string.IsNullOrEmpty(Caption);

        [XmlElement("title")]
        public string Title { get; set; } = string.Empty;
        public bool ShouldSerializeTitle => !string.IsNullOrEmpty(Title);

        [XmlElement("geo_location")]
        public string GeoLocation { get; set; } = string.Empty;
        public bool ShouldSerializeGeoLocation => !string.IsNullOrEmpty(GeoLocation);

    }
}
