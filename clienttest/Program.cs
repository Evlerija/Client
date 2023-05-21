using System;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Diagnostics;

namespace client
{
    class Client
    {
        static void Main()
        {
            string serverIP = "127.0.0.1";
            int serverPort = 12345;
            try
            {
                TcpClient client = new TcpClient(serverIP, serverPort);
                StreamWriter writer = new StreamWriter(client.GetStream(), Encoding.ASCII);
                string filePath = "data.txt";
                while (!writer.Equals("aaaa"))
                {
                    System.Threading.Thread.Sleep(5000);
                    //float cpuLoad = GetCPULoad();
                    float ramUsage = GetRAMUsage();
                    //string ipAddress = GetIPAddress();
                    try
                    {
                        using (StreamWriter fileWriter = new StreamWriter(filePath, false))
                        {
                            //fileWriter.WriteLine("CPU: " + cpuLoad);
                            fileWriter.WriteLine("RAM: " + ramUsage);
                            //fileWriter.WriteLine("IP Address: " + ipAddress);
                        }
                       // writer.WriteLine(cpuLoad);
                        writer.WriteLine(ramUsage);
                        //writer.WriteLine(ipAddress);
                        writer.Flush();
                        Console.WriteLine("Данные успешно отправлены на сервер.");
                        System.Threading.Thread.Sleep(10000);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Произошла ошибка: " + ex.Message);
                    }
                }
                writer.Close();
                client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка: " + ex.Message);
            }
            Console.ReadLine();
        }

        static float GetCPULoad()
        {
            PerformanceCounter cpuCounter = new PerformanceCounter();
            System.Threading.Thread.Sleep(500);
            float cpuLoad = cpuCounter.NextValue();
            cpuLoad = cpuCounter.NextValue();
            return cpuLoad;
        }

        static float GetRAMUsage()
        {
            PerformanceCounter ramCounter = new PerformanceCounter();
            System.Threading.Thread.Sleep(500);
            float ramUsage = ramCounter.NextValue();
            ramUsage = ramCounter.NextValue();
            return ramUsage;
        }

        static string GetIPAddress()
        {
            string ipAddress = string.Empty;
            IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
            System.Threading.Thread.Sleep(500);
            foreach (IPAddress address in hostEntry.AddressList)
            {
                if (address.AddressFamily == AddressFamily.InterNetwork)
                {
                    ipAddress = address.ToString();
                    break;
                }
            }
            return ipAddress;
        }
    }
}