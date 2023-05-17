﻿namespace Ability.ModbusRTU.Messages;
class ReadCoilsInputsResponse : ModbusMessageWithData<DiscreteCollection>, IModbusMessage
{
    private const int _minimumFrameSize = 3;

    public ReadCoilsInputsResponse()
    {
    }

    public ReadCoilsInputsResponse(byte functionCode, byte slaveAddress, byte byteCount, DiscreteCollection data)
        : base(slaveAddress, functionCode)
    {
        ByteCount = byteCount;
        Data = data;
    }

    public byte ByteCount
    {
        get { return MessageImpl.ByteCount.Value; }
        set { MessageImpl.ByteCount = value; }
    }

    public override int MinimumFrameSize
    {
        get { return _minimumFrameSize; }
    }

    public override string ToString()
    {
        return String.Format(CultureInfo.InvariantCulture, "Read {0} {1} - {2}.", Data.Count,
            FunctionCode == Modbus.ReadInputs ? "inputs" : "coils", Data);
    }

    protected override void InitializeUnique(byte[] frame)
    {
        if (frame.Length < 3 + frame[2])
            throw new FormatException("Message frame data segment does not contain enough bytes.");

        ByteCount = frame[2];
        Data = new DiscreteCollection(CollectionUtility.Slice<byte>(frame, 3, ByteCount));
    }
}