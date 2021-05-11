using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using FileUpload.Configuration;

namespace FileUpload.Configuration
{
    public class FolderElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new FolderElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((FolderElement)element).FolderName;
        }


    }
}