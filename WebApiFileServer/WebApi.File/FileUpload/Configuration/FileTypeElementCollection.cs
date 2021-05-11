using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using FileUpload.Configuration;


namespace FileUpload.Configuration
{
    public class FileTypeElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new FileTypeElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((FileTypeElement)element).TypeName;
        }


       
    }
}