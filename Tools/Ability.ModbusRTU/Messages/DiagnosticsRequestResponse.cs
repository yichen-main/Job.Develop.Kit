﻿namespace Ability.ModbusRTU.Messages;
class DiagnosticsRequestResponse : ModbusMessageWithData<RegisterCollection>, IModbusMessage
{
    private const int _minimumFrameSize = 6;

    public DiagnosticsRequestResponse()
    {
    }

    public DiagnosticsRequestResponse(ushort subFunctionCode, byte slaveAddress, RegisterCollection data)
        : base(slaveAddress, Modbus.Diagnostics)
    {
        SubFunctionCode = subFunctionCode;
        Data = data;
    }

    public override int MinimumFrameSize
    {
        get { return _minimumFrameSize; }
    }

    [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "May implement addtional sub function codes in the future.")]
    public ushort SubFunctionCode
    {
        get { return MessageImpl.SubFunctionCode.Value; }
        set { MessageImpl.SubFunctionCode = value; }
    }

    public override string ToString()
    {
        Debug.Assert(SubFunctionCode == Modbus.DiagnosticsReturnQueryData, "Need to add support for additional sub-function.");

        return String.Format(CultureInfo.InvariantCulture, "Diagnostics message, sub-function return query data - {0}.", Data);
    }

    protected override void InitializeUnique(byte[] frame)
    {
        SubFunctionCode = (ushort)IPAddress.NetworkToHostOrder(BitConverter.ToInt16(frame, 2));
        Data = new RegisterCollection(CollectionUtility.Slice(frame, 4, 2));
    }
}