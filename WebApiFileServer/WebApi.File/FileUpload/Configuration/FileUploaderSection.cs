using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;


namespace FileUpload.Configuration
{
    public class FileUploaderSection : ConfigurationSection
    {
        public const string SectionName = "fileUploader";

        [ConfigurationProperty("fileTypes", IsRequired = true)]
        [ConfigurationCollection(typeof(FileTypeElementCollection), AddItemName = "fileType")]
        public FileTypeElementCollection FileTypes
        {
            get
            {
                return (FileTypeElementCollection)this["fileTypes"];
            }
            set
            {
                this["fileTypes"] = value;
            }
        }
    }
}