using System;

namespace PlanAPark
{
    [Serializable]
    internal class TokenDTO
    {
        internal string AircraftImageUri { get; set; }
        internal double Top { get; set; }
        internal double Left { get; set; }
        internal double Rotation { get; set; }
        internal double Height { get; set; }
        internal double Width { get; set; }
    }
}