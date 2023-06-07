using System;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;

namespace Optimum
{
    public class Internet
    {
        [DllImport("wininet.dll")]
        public static extern bool InternetGetConnectedState(ref internetConnectionState lpdwFlags, int dwReserved);

        [Flags]
        public enum internetConnectionState : int
        {
            INTERNET_CONNECTION_MODEM = 0x1,
            INTERNET_CONNECTION_LAN = 0x2,
            INTERNET_CONNECTION_PROXY = 0x4,
            INTERNET_RAS_INSTALLED = 0x10,
            INTERNET_CONNECTION_OFFLINE = 0x20,
            INTERNET_CONNECTION_CONFIGURED = 0x40
        }

        public static object syncObj = new object();

        /// <summary>
        /// Проверка, есть ли соединение с интернетом
        /// </summary>
        /// <returns>Результат соединения с интернетом</returns>
        public static bool CheckConnection()
        {
            lock (syncObj)
            {
                try
                {
                    internetConnectionState flags = internetConnectionState.INTERNET_CONNECTION_CONFIGURED | 0;
                    bool checkStatus = InternetGetConnectedState(ref flags, 0);

                    // Проверить соединение интернета путем попытки подключиться к одному из указанных серверов
                    if (checkStatus)
                        return PingServer(new string[]
                        {
                            @"google.com",
                            @"microsoft.com",
                            @"ibm.com"
                        });
                    return checkStatus;
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Пингануть сервера, при первом получении ответа от любого сервера возвращает true 
        /// </summary>
        /// <param name="serverList">Список серверов</param>
        /// <returns>Флаг наличия соединения с интернетом</returns>
        public static bool PingServer(string[] serverList)
        {
            bool haveAnInternetConnection = false;
            Ping ping = new Ping();
            for (int i = 0; i < serverList.Length; i++)
            {
                PingReply pingReply = ping.Send(serverList[i]);
                haveAnInternetConnection = pingReply.Status == IPStatus.Success;
                if (haveAnInternetConnection)
                    break;
            }
            return haveAnInternetConnection;
        }
    }
}