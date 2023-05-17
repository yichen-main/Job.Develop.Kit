﻿namespace Ability.ModbusRTU.Messages;

class WriteSingleCoilRequestResponse : ModbusMessageWithData<RegisterCollection>, IModbusRequest
{
    private const int _minimumFrameSize = 6;

    public WriteSingleCoilRequestResponse()
    {
    }

    public WriteSingleCoilRequestResponse(byte slaveAddress, ushort startAddress, bool coilState)
        : base(slaveAddress, Modbus.WriteSingleCoil)
    {
        StartAddress = startAddress;
        Data = new RegisterCollection(coilState ? Modbus.CoilOn : Modbus.CoilOff);
    }

    public override int MinimumFrameSize
    {
        get { return _minimumFrameSize; }
    }

    public ushort StartAddress
    {
        get { return MessageImpl.StartAddress.Value; }
        set { MessageImpl.StartAddress = value; }
    }

    public override string ToString()
    {
        Debug.Assert(Data != null, "Argument Data cannot be null.");
        Debug.Assert(Data.Count == 1, "Data should have a count of 1.");

        return String.Format(CultureInfo.InvariantCulture, "Write single coil {0} at address {1}.",
            Data[0] == Modbus.CoilOn ? 1 : 0, StartAddress);
    }

    public void ValidateResponse(IModbusMessage response)
    {
        WriteSingleCoilRequestResponse typedResponse = (WriteSingleCoilRequestResponse)response;

        if (StartAddress != typedResponse.StartAddress)
        {
            throw new IOException(String.Format(CultureInfo.InvariantCulture,
                "Unexpected start address in response. Expected {0}, received {1}.", StartAddress, typedResponse.StartAddress));
        }

        if (Data[0] != typedResponse.Data[0])
        {
            throw new IOException(String.Format(CultureInfo.InvariantCulture,
                "Unexpected data in response. Expected {0}, received {1}.", Data[0], typedResponse.Data[0]));
        }
    }

    protected override void InitializeUnique(byte[] frame)
    {
        StartAddress = (ushort)IPAddress.NetworkToHostOrder(BitConverter.ToInt16(frame, 2));
        Data = new RegisterCollection(CollectionUtility.Slice(frame, 4, 2));
    }
}