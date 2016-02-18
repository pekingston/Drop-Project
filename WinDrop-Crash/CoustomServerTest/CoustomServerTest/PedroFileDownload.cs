using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoustomServerTest
{
    public class PedroFileDownload
    {
        String path;
        String filename;
        String url;

        public string Path
        {
            get
            {
                return path;
            }

            set
            {
                path = value;
            }
        }

        public string Filename
        {
            get
            {
                return filename;
            }

            set
            {
                filename = value;
            }
        }

        public string Url
        {
            get
            {
                return url;
            }

            set
            {
                url = value;
            }
        }

        /// <summary>
        /// Container for the data to create the web page to share the file
        /// </summary>
        /// <param name="path">File path</param>
        /// <param name="filename">File name with extension</param>
        /// <param name="url">URL to get the file</param>
        public PedroFileDownload(String path, String filename, String url)
        {
            this.Path = path;
            this.Filename = filename;
            this.Url = url;
        }



    }
}
