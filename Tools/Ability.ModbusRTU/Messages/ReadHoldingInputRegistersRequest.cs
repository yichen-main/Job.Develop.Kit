﻿namespace Ability.ModbusRTU.Messages;
class ReadHoldingInputRegistersRequest : ModbusMessage, IModbusRequest
{
    private const int _minimumFrameSize = 6;

    public ReadHoldingInputRegistersRequest()
    {
    }

    public ReadHoldingInputRegistersRequest(byte functionCode, byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        : base(slaveAddress, functionCode)
    {
        StartAddress = startAddress;
        NumberOfPoints = numberOfPoints;
    }

    public ushort StartAddress
    {
        get { return MessageImpl.StartAddress.Value; }
        set { MessageImpl.StartAddress = value; }
    }

    public override int MinimumFrameSize
    {
        get { return _minimumFrameSize; }
    }

    public ushort NumberOfPoints
    {
        get
        {
            return MessageImpl.NumberOfPoints.Value;
        }
        set
        {
            if (value > Modbus.MaximumRegisterRequestResponseSize)
                throw new ArgumentOutOfRangeException("NumberOfPoints", String.Format(CultureInfo.InvariantCulture, "Maximum amount of data {0} registers.", Modbus.MaximumRegisterRequestResponseSize));

            MessageImpl.NumberOfPoints = value;
        }
    }

    public override string ToString()
    {
        return String.Format(CultureInfo.InvariantCulture, "Read {0} {1} registers starting at address {2}.", NumberOfPoints, FunctionCode == Modbus.ReadHoldingRegisters ? "holding" : "input", StartAddress);
    }

    public void ValidateResponse(IModbusMessage response)
    {
        ReadHoldingInputRegistersResponse typedResponse = response as ReadHoldingInputRegistersResponse;
        Debug.Assert(typedResponse != null, "Argument response should be of type ReadHoldingInputRegistersResponse.");
        int expectedByteCount = NumberOfPoints * 2;

        if (expectedByteCount != typedResponse.ByteCount)
        {
            throw new IOException(String.Format(CultureInfo.InvariantCulture,
                "Unexpected byte count. Expected {0}, received {1}.",
                expectedByteCount,
                typedResponse.ByteCount));
        }
    }

    protected override void InitializeUnique(byte[] frame)
    {
        StartAddress = (ushort)IPAddress.NetworkToHostOrder(BitConverter.ToInt16(frame, 2));
        NumberOfPoints = (ushort)IPAddress.NetworkToHostOrder(BitConverter.ToInt16(frame, 4));
    }
}