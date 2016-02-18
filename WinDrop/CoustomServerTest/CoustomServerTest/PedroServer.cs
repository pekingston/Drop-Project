using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CoustomServerTest
{
    // http://rlbisbe.net/2011/04/25/creando-un-servidor-http-basico-con-c/
    class PedroServer
    {
        private netTools netTool = new netTools();
        private int port;
        private IPAddress localAddr;
        private TcpListener server = null;
        private Byte[] bytes;
        private String data;
        private TcpListener listener;
        private string home;
        private static string NOTFOUND404 = "HTTP/1.1 404 Not Found";
        private static string OK200 = "HTTP/1.1 200 OK\r\n\r\n\r\n";
        private static int MAX_SIZE = 1000;

        // TMP floder and number of subfloder
        private String floderPath;
        private int nTmpFloder;
        private static String INDEXHTMLPATH = "../../HTML/MainPage.html";
        private static String INDEXHTMLNAME = "MainPage.html";


        public PedroServer(int port)
        {
            this.port = port;
            listener = new TcpListener(IPAddress.Any, port);
            floderPath = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString();
            System.IO.Directory.CreateDirectory(this.floderPath);
            File.Copy(INDEXHTMLPATH,  floderPath + "/index.html");
            //File.Copy(@"C:\Users\Pedro\Downloads\test.zip", floderPath + "/test.zip");
            nTmpFloder = 0;
        }

        public void Start()
        {
            listener.Start();
            Console.WriteLine(string.Format("Local server started at localhost:{0}", port));

            Console.CancelKeyPress += delegate
            {
                Console.WriteLine("Stopping server");
                StopServer();
            };
        }

        public void Listen()
        {
            try
            {
                while (true)
                {
                    Byte[] result = new Byte[MAX_SIZE];
                    string requestData;

                    TcpClient client = listener.AcceptTcpClient();
                    NetworkStream stream = client.GetStream();
                    int size = stream.Read(result, 0, result.Length);
                    requestData = System.Text.Encoding.ASCII.GetString(result, 0, size);
                    Console.WriteLine("Received: {0}", requestData);

                    Request request = GetRequest(requestData);
                    ProcessRequest(request, stream);
                    client.Close();
                }
            }
            finally
            {
                listener.Stop();
            }
        }

        private void ProcessRequest(Request request, NetworkStream stream)
        {
            if (request == null)
            {
                return;
            }
            if (request.Path.Equals("/"))
                request.Path = floderPath+"/index.html";
            ParsePath(request);
            if (File.Exists(floderPath+request.Path))
            {
                if(Path.GetExtension(request.Path).ToUpper()==".HTML")
                { 
                    var fileContent = File.ReadAllText(floderPath + request.Path);
                    GenerateResponse(fileContent, stream, OK200);
                    return;
                }
                else
                {
                    
                    byte[] byteContents = File.ReadAllBytes(floderPath + request.Path.Replace(@"/",@"\"));
                    GenerateResponse(byteContents, stream, OK200);
                    return;
                }
            }

            GenerateResponse("Not found", stream, NOTFOUND404);
        }

        private void ParsePath(Request request)
        {
            request.Path.Replace('/', '\\');
            request.Path = home + request.Path;
        }

        private void GenerateResponse(string content,
            NetworkStream stream,
            string responseHeader)
        {
            string response = "HTTP/1.1 200 OK\r\n\r\n\r\n";
            response = response + content;
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(response);
            stream.Write(msg, 0, msg.Length);
            return;
        }

        private void GenerateResponse(byte[] content,
        NetworkStream stream,
        string responseHeader)
            {
            string response = "HTTP/1.1 200 OK\r\n\r\n\r\n";
            byte[] responseBytes = System.Text.Encoding.ASCII.GetBytes(response);
            //response = response + content;
            //byte[] msg = System.Text.Encoding.ASCII.GetBytes(response);
            byte[] msg = new byte[content.Length + responseBytes.Length];
            responseBytes.CopyTo(msg, 0);
            content.CopyTo(msg, responseBytes.Length);
            stream.Write(msg, 0, msg.Length);
            
                return;
            }




        private void StopServer()
        {
            listener.Stop();
        }

        private Request GetRequest(string data)
        {
            Request request = new Request();
            var list = data.Split(' ');
            if (list.Length < 3)
                return null;

            request.Command = list[0];
            request.Path = list[1];
            request.Protocol = list[2].Split('\n')[0];

            Console.WriteLine("Instruction: {0}\nPath: {1}\nProtocol: {2}",
                request.Command,
                request.Path,
                request.Protocol);
            return request;
        }


        public void Stop()
        {
            listener.Stop();
        }

        public class Request
        {
            public string Command;
            public string Path;
            public string Protocol;
        }


        public PedroFileDownload CreateLink(String file,String filename)
        {
            PedroFileDownload data;
            nTmpFloder++;
            Directory.CreateDirectory(floderPath + @"\tmp" + nTmpFloder);
            File.Copy(file, floderPath + @"\tmp"+nTmpFloder+@"\"+filename);
            File.Copy(INDEXHTMLPATH, floderPath + @"\tmp" + nTmpFloder + @"\index.html");
            String uri =  netTool.getLocalWlanAdress() + ":" + this.port + @"/tmp" + nTmpFloder;
            data = new PedroFileDownload(floderPath + @"\tmp" + nTmpFloder, filename,uri);
            ModifyHtml(data);
            return data;
        }


        public void ModifyHtml(PedroFileDownload pfd)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(pfd.Path +  @"\index.html");
            String xPathSentence = "/html/body/a[@id='btnDownloadId']";
            XmlNode htmlRoot = doc.DocumentElement;
            XmlNode button = doc.ChildNodes[0]["body"]["a"];
            if (button != null)
            {
                XmlAttribute href = doc.CreateAttribute("href");
                XmlAttribute download = doc.CreateAttribute("download");
                href.Value = @"http://" + pfd.Url + @"/" + pfd.Filename;
                download.Value = pfd.Filename;
                button.Attributes.Append(href);
                button.Attributes.Append(download);
            }
            doc.Save(pfd.Path + @"/index.html");
            
        }



    }
}
