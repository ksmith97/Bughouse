/* Origional Author: Kevin Smith
 * Additional Contributors:
 * 
 * The goal of this file is to contain all functionality dealing with the creation of sockets
 * It will also contain the network protocol parser
 * and should pass methods to the appropriate object.
 * This it must be part of a parent object in order to gain access to all the inherited objects.
 * 
 * Current Issues
 * -WHen a client disconnects client count does not decrement may need to consider swapping from an
 * array to a queue/stack or just a dynamic array.
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace Bughouse
{
    public partial class MainWindow
    {
        private Socket m_socListener;
        private Socket [] m_socWorker;
        private int port = 8223;
        private int m_clientCount = 0;

        public int Port
        {
            get { return port; }
            set { port = value; }
        }

        //List<Socket> peers;
        private AsyncCallback workercallback;

        public class CSocketPacket
        {
            public System.Net.Sockets.Socket thisSocket;
            public byte[] dataBuffer = new byte[1024];
        }

        private void IntializeServer()
        {
            m_socWorker = new Socket[10];
            try
            {
                // Create the listening socket...
                m_socListener = new Socket(AddressFamily.InterNetwork,
                                          SocketType.Stream,
                                          ProtocolType.Tcp);
                IPEndPoint ipLocal = new IPEndPoint(IPAddress.Any, port);
                // Bind to local IP Address...
                m_socListener.Bind(ipLocal);

                // Start listening...
                m_socListener.Listen(4);
                // Create the call back for any client connections...
                m_socListener.BeginAccept(new AsyncCallback(OnClientConnect), null);
            }
			catch(SocketException se)
			{
				cb.SetText( se.Message );
			}


        }

        public void OnClientConnect(IAsyncResult asyn)
        {   
            try
            {
                m_socWorker[m_clientCount] = m_socListener.EndAccept(asyn);

                cb.SetText("Client Connected");

                WaitForData(m_socWorker[m_clientCount]);

                ++m_clientCount;

                m_socListener.BeginAccept(new AsyncCallback(OnClientConnect), null);
            }
            catch (ObjectDisposedException)
            {
                cb.SetText("OnClientConnection: Socket has been closed");

            }
            catch (SocketException se)
            {
                cb.SetText(se.Message);
            }

        }

        public void WaitForData(Socket worker)
        {
            try
            {
                if (workercallback == null)
                {
                    workercallback = new AsyncCallback(OnDataReceived);
                }
                CSocketPacket csocket = new CSocketPacket();
                csocket.thisSocket = worker;
                worker.BeginReceive(csocket.dataBuffer, 0, csocket.dataBuffer.Length, SocketFlags.None, workercallback, csocket);
            }
            catch (SocketException se)
            {
                Console.WriteLine(se);
            }
        }
        public void OnDataReceived(IAsyncResult asyn)
        {
            try
            {
                CSocketPacket theSockId = (CSocketPacket)asyn.AsyncState;
                //end receive...
                int iRx = 0;
                iRx = theSockId.thisSocket.EndReceive(asyn);
                char[] chars = new char[iRx + 1];
                System.Text.Decoder d = System.Text.Encoding.UTF8.GetDecoder();
                int charLen = d.GetChars(theSockId.dataBuffer, 0, iRx, chars, 0);
                System.String szData = new System.String(chars);
                parseData(szData);
                WaitForData(m_socWorker[m_clientCount]);
            }
            catch (ObjectDisposedException)
            {
                cb.SetText("OnDataReceived: Socket has been closed");
            }
            catch (SocketException se)
            {
                cb.SetText(se.Message);
            }
        }


        public void ConnectToServer(String ip)
        {
            Socket m_clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            System.Net.IPAddress ipAdd = System.Net.IPAddress.Parse(ip);
            System.Net.IPEndPoint remoteEP = new IPEndPoint(ipAdd,port);
            m_clientSocket.Connect(remoteEP);

            try
            {
                String szData = "Hello There";
                byte[] byData = System.Text.Encoding.ASCII.GetBytes(szData);
                m_socListener.Send(byData);
                WaitForData(m_clientSocket);
            }
            catch (SocketException se)
            {
                cb.SetText(se.Message);
            }
            Environment.Exit(3);
        }

        private void parseData(string data)
        {
            cb.SetText(data);
            data.Split(" ".ToCharArray());

            switch (Convert.ToInt32(data[0]))
            {
                case 100:
                    break;
                case 500:

                    foreach (char s in data)
                        cb.SetText(data);
                    break;
                default:
                    break;
            }
        }   
    }
}
