using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace LogviewHelper
{
    #region Enums

    /// <summary>
    /// The Generic SNMP Status of the Trap
    /// </summary>
    public enum GenericStatus
    {
        /// <summary>
        /// 0 - The system is starting up for the first time
        /// </summary>
        ColdStart = 0,

        /// <summary>
        /// 1 - The system has rebooted
        /// </summary>
        WarmStart = 1,

        /// <summary>
        /// 2 - The link is down
        /// </summary>
        LinkDown = 2,

        /// <summary>
        /// 3 - The link is up
        /// </summary>
        LinkUp = 3,

        /// <summary>
        /// 4 - An authentication failure occured
        /// </summary>
        AuthenticationFailure = 4,

        /// <summary>
        /// 5
        /// </summary>
        EgpNeighborLoss = 5,

        /// <summary>
        /// 6
        /// </summary>
        EnterpriseSpecific = 6
    }

    public enum DataType
    {
        /// <summary>
        /// 0x02 - A single byte integer (0x00 - 0xFF)
        /// </summary>
        Integer32 = 0x02,

        /// <summary>
        /// 0x03 - String of character information encoded in up to 255 bytes 
        /// </summary>
        DisplayString = 0x03,

        /// <summary>
        /// 0x04 - Stream of bytes up to 255 bytes long
        /// </summary>
        OctetString = 0x04,

        /// <summary>
        /// 0x06 - An SNMP OID, which can be up to 255 bytes long
        /// </summary>
        ObjectIdentifier = 0x06,

        /// <summary>
        /// 0x30 - A sequence of OID,Length,Data triplets where the maximum size can be up to 255 bytes
        /// </summary>
        Sequence = 0x30,

        /// <summary>
        /// 0x40 - A 4 byte octet string representing an IP
        /// </summary>
        IPAddress = 0x40,

        /// <summary>
        /// 0x43 - 1/100 seconds since some epoch as per MIB specification, 4 bytes long
        /// </summary>
        TimeTicks = 0x43,

        /// <summary>
        /// 0x45 - Network Address (IP) and is 4 bytes long
        /// </summary>
        NetworkAddress = 0x45,

        /// <summary>
        /// 0xa4 - Declaration of the main body of the trap
        /// </summary>
        TrapV1Pdu = 0xa4,

    }
    #endregion
    
    public class SNMPClass
    {

        private static readonly byte[] messageOID = { 0x2b, 6, 1, 2, 1, 1, 1 };
        private static readonly byte[] privateEnt = { 0x2b, 6, 1, 4, 1 };
        private static readonly string hostName = Dns.GetHostName();

        public static string Send(string hostto, short port, string message, GenericStatus genericStatus)
        {
            try
            {
                UdpClient udpClient = new UdpClient(hostto, port);
                List<byte> data = GetTrapMessageData(message, genericStatus);
                udpClient.Send(data.ToArray(), data.Count);
                return "True";
            }
            catch (System.Exception ex)
            {
                return "False: " + ex.Message + ", " + ex.Source + ", " + ex.StackTrace;
            }
        }

        #region Byte Conversion Routines

        private static List<byte> GetLengthInBytes(int length)
        {
            List<byte> data = new List<byte>();
            int len = length;

            data.Add((byte)(len & 0xFF));

            len >>= 8;
            while (len != 0)
            {
                data.Add((byte)(len & 0xFF));
                len >>= 8;
            }

            if (length >= 0x80 || data.Count > 1)
            {
                data.Insert(0, (byte)(data.Count | 0x80));
            }

            return data;
        }

        private static List<byte> GetIntegerLengthAndDataBytes(int value)
        {
            List<byte> data = new List<byte>();
            int val = value;
            do
            {
                data.Insert(0, (byte)(val & 0xFF));
                val >>= 8;
            } while (val != 0);

            data.InsertRange(0, GetLengthInBytes(data.Count));
            data.Insert(0, ToByte(DataType.Integer32));

            return data;
        }

        private static byte ToByte(GenericStatus value)
        {
            return (byte)value;
        }

        private static byte ToByte(DataType value)
        {
            return (byte)value;
        }

        private static byte[] ToBytes(string data)
        {
            return Encoding.ASCII.GetBytes(data);
        }

        #endregion

        private static List<byte> GetTrapMessageData(string message, GenericStatus genericStatus)
        {
            List<byte> data = new List<byte>();

            byte[] header = new byte[] { 0x02, 0x01, 0x00, 0x04, 0x06, 0x70, 0x75, 0x62, 0x6C, 0x69, 0x63 };
            data.AddRange(header);

            AddTrapPDU(data, message, genericStatus);

            data.InsertRange(0, GetLengthInBytes(data.Count));
            data.Insert(0, ToByte(DataType.Sequence));

            return data;
        }

        private static void AddTrapPDU(List<byte> data, string message, GenericStatus genericStatus)
        {
            List<byte> innerData = new List<byte>();

            AddTrapSourceOID(innerData);
            AddTrapSourceIP(innerData);
            AddTrapStatusGeneric(innerData, genericStatus);
            AddTrapStatusSpecific(innerData, 0);
            AddTrapTimeSinceLastTrap(innerData);
            AddTrapVariables(innerData, message);

            innerData.InsertRange(0, GetLengthInBytes(innerData.Count));
            innerData.Insert(0, ToByte(DataType.TrapV1Pdu));

            data.AddRange(innerData);
        }

        #region AddTrapSource

        private static void AddTrapSourceOID(List<byte> data)
        {
            data.Add(ToByte(DataType.ObjectIdentifier));
            data.AddRange(GetLengthInBytes(privateEnt.Length));
            data.AddRange(privateEnt);
        }

        private static void AddTrapSourceIP(List<byte> data)
        {
            IPAddress[] ips = Dns.GetHostAddresses(hostName);
            data.Add(ToByte(DataType.IPAddress));
            data.Add((byte)4);

            if (ips.Length > 0)
                data.AddRange(ips[0].GetAddressBytes());
            else
                data.AddRange(new byte[4] { 0, 0, 0, 0 });
        }

        #endregion

        #region AddTrapStatus

        private static void AddTrapStatusGeneric(List<byte> data, GenericStatus genericStatus)
        {
            data.Add(ToByte(DataType.Integer32));
            data.Add((byte)1);
            data.Add(ToByte(genericStatus));
        }

        private static void AddTrapStatusSpecific(List<byte> data, byte specificStatus)
        {
            data.Add(ToByte(DataType.Integer32));
            data.Add((byte)1);
            data.Add(specificStatus);
        }

        #endregion

        private static void AddTrapTimeSinceLastTrap(List<byte> data)
        {
            data.Add(ToByte(DataType.TimeTicks));
            data.Add((byte)1);
            data.Add((byte)0);
        }

        #region AddTrapVariables

        private static void AddTrapVariables(List<byte> data, string message)
        {
            List<byte> innerData = new List<byte>();

            AddTrapMessage(innerData, message);

            innerData.InsertRange(0, GetLengthInBytes(innerData.Count));
            innerData.Insert(0, ToByte(DataType.Sequence));

            data.AddRange(innerData);
        }

        private static void AddTrapMessage(List<byte> data, string message)
        {
            List<byte> innerData = new List<byte>();

            innerData.Add(ToByte(DataType.ObjectIdentifier));
            innerData.AddRange(GetLengthInBytes(messageOID.Length));
            innerData.AddRange(messageOID);

            innerData.Add(ToByte(DataType.OctetString));
            innerData.AddRange(GetLengthInBytes(message.Length));
            innerData.AddRange(ToBytes(message));

            innerData.InsertRange(0, GetLengthInBytes(innerData.Count));
            innerData.Insert(0, ToByte(DataType.Sequence));

            data.AddRange(innerData);
        }

        #endregion

    }
}
