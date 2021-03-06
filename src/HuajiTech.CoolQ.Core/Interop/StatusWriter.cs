namespace HuajiTech.CoolQ.Interop
{
    internal class StatusWriter : Writer<Status>
    {
        public override void Write(Status status)
        {
            Write(status.Value);
            Write(status.Unit);
            Write((int)status.Color);
        }
    }
}