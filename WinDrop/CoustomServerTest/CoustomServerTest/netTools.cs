using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;

namespace CoustomServerTest
{
    class netTools
    {
        public string getLocalWlanAdress()
        {
            bool findNetwork = false;
            IPv4InterfaceStatistics address= null;
            foreach(NetworkInterface net in NetworkInterface.GetAllNetworkInterfaces())
            {
                if(net.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
                {
                    findNetwork = true;
                    address = net.GetIPv4Statistics();
                }

            }
            String ip = address.ToString();

            return ip;
        }
    }
}
