using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoustomServerTest
{
    class PedroFileDownload
    {
        String path;
        String filename;
        String url;

        /// <summary>
        /// Container for the data to create the web page to share the file
        /// </summary>
        /// <param name="path">File path</param>
        /// <param name="filename">File name with extension</param>
        /// <param name="url">URL to get the file</param>
        public PedroFileDownload(String path, String filename, String url)
        {
            this.path = path;
            this.filename = filename;
            this.url = url;
        }

    }
}
