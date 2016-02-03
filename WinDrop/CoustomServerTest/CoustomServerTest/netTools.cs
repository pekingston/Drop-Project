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
        /// <summary>
        /// Returns the ip from the current wlan interface UP, if there are no wlan interficies or all the wlan interficies are down, returns "";
        /// </summary>
        /// <returns>String IP address v4</returns>
        public string getLocalWlanAdress()
        {
            bool findNetwork = false;
            IPInterfaceProperties ipData;
            String ipString = "";
            foreach (NetworkInterface net in NetworkInterface.GetAllNetworkInterfaces())
            {
                if(net.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
                {
                    ipData = net.GetIPProperties();
                    foreach ( UnicastIPAddressInformation ip in ipData.UnicastAddresses )
                    {
                        if(net.OperationalStatus == OperationalStatus.Up && ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            findNetwork = true;
                            ipString = ip.Address.ToString();
                        }
                    }
                }

            }
            return ipString;
        }
    }
}
