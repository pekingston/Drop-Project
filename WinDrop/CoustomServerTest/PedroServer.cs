using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CoustomServerTest
{
    // http://rlbisbe.net/2011/04/25/creando-un-servidor-http-basico-con-c/
    class PedroServer
    {
        Int32 port;
        IPAddress localAddr;
        TcpListener server = null;
        Byte[] bytes;
        String data;

        public PedroServer()
        {
            this.port = 4564; // puerto
            this.localAddr = IPAddress.Parse("127.0.0.1"); // para escuchar localhost, o sea, nuestros paquetes
            this.server = new TcpListener(localAddr, port); //listener con el puerto y la IP
            server.Start(); // iniciamos el servidor
            this.bytes = new Byte[1000];
            this.data = null; // datos procesados
            while (true)
            {
                TcpClient client = server.AcceptTcpClient(); //Aceptamos la conexión entrante

            }
        }



        private void sendBasicPacket()
        {

        }

    }
}
