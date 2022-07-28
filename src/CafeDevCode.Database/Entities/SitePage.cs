using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeDevCode.Database.Entities
{
    public class SitePage : BaseEntity
    {
        public int Id { get; set; }
        public string? Key { get; set; }
        /// <summary>
        /// Common
        /// </summary>
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Logo { get; set; }
        public string? KeyWords { get; set; }
        public string? NewKeyWords { get; set; }
        public string? AppleMobileWebAppCapable { get; set; }
        public string? AppleMobileWebAppTitle { get; set; }
        public string? ArticleId { get; set; }
        public string? CategoryId { get; set; }
        public string? SiteId { get; set; }
        public string? SiteIdNew { get; set; }
        public string? ListFolder { get; set; }
        public string? ListFolderName { get; set; }
        public string? PageType { get; set; }
        public string? PageTypeNew { get; set; }

        /// <summary>
        /// Facebook
        /// </summary>
        public string? OgSiteName { get; set; }
        public string? OgRichAttachment { get; set; }
        public string? OgType { get; set; }
        public string? OgUrl { get; set; }
        public string? OgImage { get; set; }
        public string? OgImageWidth { get; set; }
        public string? OgImageHeight { get; set; }
        public string? OgTitle { get; set; }
        public string? ArticleTags { get; set; }
        public string? OgDescription { get; set; }
        public string? ArticlePublisher { get; set; }

        /// <summary>
        /// Site
        /// </summary>
        public string? Medium { get; set; }
        public string? InLanguage { get; set; }
        public string? ArticleSection { get; set; }
        public string? Source { get; set; }
        public string? Copyright { get; set; }
        public string? Author { get; set; }
        public string? Robots { get; set; }
        public string? GeoPlaceName { get; set; }
        public string? GeoRegion { get; set; }
        public string? GeoPosition { get; set; }
        public string? ICBM { get; set; }
        public string? RevisitAfter { get; set; }

        /// <summary>
        /// Twitter
        /// </summary>
        public string? TwitterCard { get; set; }
        public string? TwitterUrl { get; set; }
        public string? TwitterTitle { get; set; }
        public string? TwitterDescription { get; set; }
        public string? TwitterImage { get; set; }
        public string? TwitterSite { get; set; }
        public string? TwitterCreator { get; set; }

        /// <summary>
        /// Google
        /// </summary>
        public string? PubDate { get; set; }
        public string? LastMod { get; set; }


        public SitePage SetCommon(string title, string keywords, string description)
        {
            this.Title = title;
            this.KeyWords = keywords;
            this.Description = description;
            return this;
        }

        public SitePage SetUrl(string url)
        {
            this.OgUrl = url;
            this.TwitterUrl = url;
            return this;
        }

        public SitePage SetData(string? title, string? keywords,
            string? description, string? image, string? url)
        {
            this.Title = title ?? string.Empty;
            this.OgTitle = title ?? string.Empty;
            this.TwitterTitle = title ?? string.Empty;

            this.KeyWords = keywords ?? string.Empty;
            this.Description = description ?? string.Empty;
            this.OgDescription = description ?? string.Empty;
            this.TwitterDescription = description ?? string.Empty;

            this.TwitterImage = image ?? string.Empty;
            this.OgImage = image ?? string.Empty;

            this.OgUrl = url ?? string.Empty;
            this.TwitterUrl = url ?? string.Empty;

            return this;
        }
    }
}
