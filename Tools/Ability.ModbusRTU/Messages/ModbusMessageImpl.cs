﻿namespace Ability.ModbusRTU.Messages;

/// <summary>
/// Class holding all implementation shared between two or more message types. 
/// Interfaces expose subsets of type specific implementations.
/// </summary>
class ModbusMessageImpl
{
    public ModbusMessageImpl()
    {
    }

    public ModbusMessageImpl(byte slaveAddress, byte functionCode)
    {
        SlaveAddress = slaveAddress;
        FunctionCode = functionCode;
    }

    public byte? ByteCount { get; set; }

    public byte? ExceptionCode { get; set; }

    public ushort TransactionId { get; set; }

    public byte FunctionCode { get; set; }

    public ushort? NumberOfPoints { get; set; }

    public byte SlaveAddress { get; set; }

    public ushort? StartAddress { get; set; }

    public ushort? SubFunctionCode { get; set; }

    public IModbusMessageDataCollection Data { get; set; }

    public byte[] MessageFrame
    {
        get
        {
            List<byte> frame = new List<byte>();
            frame.Add(SlaveAddress);
            frame.AddRange(ProtocolDataUnit);

            return frame.ToArray();
        }
    }

    public byte[] ProtocolDataUnit
    {
        get
        {
            List<byte> pdu = new List<byte>();

            pdu.Add(FunctionCode);

            if (ExceptionCode.HasValue)
                pdu.Add(ExceptionCode.Value);

            if (SubFunctionCode.HasValue)
                pdu.AddRange(BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)SubFunctionCode.Value)));

            if (StartAddress.HasValue)
                pdu.AddRange(BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)StartAddress.Value)));

            if (NumberOfPoints.HasValue)
                pdu.AddRange(BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)NumberOfPoints.Value)));

            if (ByteCount.HasValue)
                pdu.Add(ByteCount.Value);

            if (Data != null)
                pdu.AddRange(Data.NetworkBytes);

            return pdu.ToArray();
        }
    }

    public void Initialize(byte[] frame)
    {
        if (frame == null)
            throw new ArgumentNullException("frame", "Argument frame cannot be null.");

        if (frame.Length < Modbus.MinimumFrameSize)
            throw new FormatException(String.Format(CultureInfo.InvariantCulture, "Message frame must contain at least {0} bytes of data.", Modbus.MinimumFrameSize));

        SlaveAddress = frame[0];
        FunctionCode = frame[1];
    }
}