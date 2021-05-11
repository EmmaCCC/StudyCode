using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace FileUpload.Configuration
{
    public class FileTypeElement : ConfigurationElement
    {
        [ConfigurationProperty("allowFileExtensions", IsRequired = true)]
        public string AllowFileExtensions
        {
            get
            {
                return (string)this["allowFileExtensions"];
            }
            set
            {
                this["allowFileExtensions"] = value;
            }
        }

        [ConfigurationProperty("maxSize", IsRequired = true)]
        public long MaxSize
        {
            get
            {
                return (long)this["maxSize"];
            }
            set
            {
                this["maxSize"] = value;
            }
        }

        [ConfigurationProperty("typeName", IsRequired = true)]
        public string TypeName
        {
            get
            {
                return (string)this["typeName"];
            }
            set
            {
                this["typeName"] = value;
            }
        }

        [ConfigurationProperty("folders", IsRequired = true)]
        [ConfigurationCollection(typeof(FolderElementCollection), AddItemName = "folder")]
        public FolderElementCollection Folders
        {
            get
            {
                return (FolderElementCollection)this["folders"];
            }
            set
            {
                this["folders"] = value;
            }
        }

    }
}