using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CafeDevCode.Ultils.Model
{
    public class SiteMapLocation
    {
        public enum eChangeFrequency
        {
            always,
            hourly,
            daily,
            weekly,
            monthly,
            yearly,
            never
        }
        [XmlElement("loc")]
        public string Url { get; set; } = string.Empty;

        [XmlElement("changefreq")]
        public eChangeFrequency? ChangeFrequency { get; set; }
        public bool ShouldSerializeChangeFrequency => ChangeFrequency.HasValue;

        [XmlElement("lastmod")]
        public string? LastModified { get; set; }
        public bool ShouldSerializeLastModified => !string.IsNullOrEmpty(LastModified);

        [XmlElement("priority")]
        public double? Priority { get; set; }
        public bool ShouldSerializePriority => Priority.HasValue;

        [XmlElement("image", Namespace = "http://www.google.com/schemas/sitemap-image/1.1")]
        public List<SiteMapImage>? Images { get; set; }
        public bool ShouldSerializeImages => Images != null && Images.Any();
    }
}
