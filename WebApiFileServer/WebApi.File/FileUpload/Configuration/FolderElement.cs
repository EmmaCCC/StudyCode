using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace FileUpload.Configuration
{
    public class FolderElement : ConfigurationElement
    {
        [ConfigurationProperty("folderName", IsRequired = true, IsKey = true)]
        public string FolderName
        {
            get
            {
                return (string)this["folderName"];
            }
            set
            {
                this["folderName"] = value;
            }
        }

        [ConfigurationProperty("value", IsRequired = true)]
        public int Value
        {
            get
            {
                return (int)this["value"];
            }
            set
            {
                this["value"] = value;
            }
        }

        [ConfigurationProperty("desc", IsRequired = true)]
        public string Desc
        {
            get
            {
                return (string)this["desc"];
            }
            set
            {
                this["desc"] = value;
            }
        }

    }
}