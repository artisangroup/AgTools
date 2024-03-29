﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Artisan.Tools.Extensions;

namespace Artisan.Tools.Database
{
    public class BroadcastUdpMsg : IDisposable
    {
        private const int SocketTimeoutExceptionCode = 10060;
        private int _port = 0;
        private byte[] _message = new byte[0];
        private TimeSpan _timeout = TimeSpan.FromSeconds(0);
        private UdpClient? _udpClient;
        private CancellationTokenSource? _cancellation;

        //
        // constructor
        //
        public BroadcastUdpMsg()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<string> SendMsgGetResponses(int port, byte[] message, TimeSpan timeout)
        {
            var responses = new List<string>();
            try
            {
                _port = port;
                _message = message;
                _timeout = timeout;
                _udpClient = new UdpClient { Client = { ReceiveTimeout = 1000 } };
                _udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, 0));
                _udpClient.EnableBroadcast = true;
                _cancellation = new CancellationTokenSource();
                _cancellation.CancelAfter(timeout);
                var receive = new Task((cancelToken) =>
                {
                    var anyEndPoint = new IPEndPoint(IPAddress.Any, 0);
                    while (!_cancellation.IsCancellationRequested)
                    {
                        try
                        {
                            var receiveBuffer = _udpClient.Receive(ref anyEndPoint);
                            responses.Add(Encoding.UTF8.GetString(receiveBuffer));
                        }
                        catch (SocketException se)
                        {
                            if (se.ErrorCode != SocketTimeoutExceptionCode) throw;
                        }
                    }
                }, _cancellation.Token);

                receive.Start();

                // 
                // send the udp
                //
                _udpClient.Send(_message, _message.Length, new IPEndPoint(IPAddress.Broadcast, _port));

                //
                // wait for the reponse
                //
                Task.WaitAll(new[] { receive }, _timeout.Milliseconds);

            }
            catch (AgException aex)
            {

            }
            catch (Exception ex)
            {

            }
            return responses;
        }

        public void Dispose()
        {
            _udpClient?.Dispose();
            _cancellation?.Dispose();
        }
    }

    public record SqlServerInstance(string ServerName, string InstanceName, bool IsClustered, string Version, int TcpPort, string NamedPipe);

    public static class AgSqlBrowser
    {

        public static List<SqlServerInstance> FindServers()
        {
            byte[] _getInstancesMessage = new byte[1] { 2 };
            using var client = new BroadcastUdpMsg();
            var responses = client.SendMsgGetResponses(1434, _getInstancesMessage, new TimeSpan(0, 0, 5));
            var instances = new List<SqlServerInstance>();
            try
            {
                foreach (var response in responses)
                {
                    string[] tokens = response.Split(";");
                    string ServerName = "";
                    string InstanceName = "";
                    bool IsClustered = false;
                    string Version = "0.0.0";
                    int TcpPort = 1439;
                    string NamedPipe = "";
                    for (int i = 0; i < tokens.Length; i++)
                    {
                        if (string.IsNullOrEmpty(tokens[i]))
                        {
                            if (!string.IsNullOrEmpty(ServerName) && !string.IsNullOrEmpty(InstanceName))
                                instances.Add(new SqlServerInstance(ServerName, InstanceName, IsClustered, Version, TcpPort, NamedPipe));
                            ServerName = "";
                            InstanceName = "";
                            IsClustered = false;
                            Version = "0.0.0";
                            TcpPort = 1439;
                            NamedPipe = "";
                        }
                        else
                        {
                            if (tokens[i].AgContains("ServerName"))
                                ServerName = tokens[++i];
                            if (tokens[i].AgContains("InstanceName"))
                                InstanceName = tokens[++i];
                            if (tokens[i].AgContains("IsClustered"))
                                IsClustered = tokens[++i].AgIsTrue();
                            if (tokens[i].AgContains("Version"))
                                Version = tokens[++i];
                            if (tokens[i].AgContains("TcpPort"))
                                TcpPort = Convert.ToInt16(tokens[++i]);
                            if (tokens[i].AgContains("NamedPipe"))
                                NamedPipe = tokens[++i];
                        }
                    }
                    if (!string.IsNullOrEmpty(ServerName) && !string.IsNullOrEmpty(InstanceName))
                        instances.Add(new SqlServerInstance(ServerName, InstanceName, IsClustered, Version, TcpPort, NamedPipe));
                }
            }
            catch
            {
            }
            return instances;
        }
    }
}
