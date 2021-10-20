using System;
using System.Text.Json.Serialization;

namespace API.Serializing
{
    public partial class JsonSerializerOptions
    {
        public JsonNumberHandling NumberHandling { get; set; }
    }

      [Flags]
    public enum JsonNumberHandling : byte
    {
        /// <summary>
        /// No specified number handling behavior. Numbers can only be read from <see cref="JsonTokenType.Number"/> and will only be written as JSON numbers (without quotes).
        /// </summary>
        None = 0x0,
        /// <summary>
        /// Numbers can be read from <see cref="JsonTokenType.String"/>. Does not prevent numbers from being read from <see cref="JsonTokenType.Number"/>.
        /// </summary>
        AllowReadingFromString = 0x1,
        /// <summary>
        /// Numbers will be written as JSON strings (with quotes), not as JSON numbers.
        /// </summary>
        WriteAsString = 0x2,
        /// Floating point constants represented as <see cref="JsonTokenType.String"/> tokens
        /// such as "NaN", "Infinity", "-Infinity", can be read when reading, and such CLR values
        /// such as <see cref="float.NaN"/>, <see cref="double.PositiveInfinity"/>, <see cref="float.NegativeInfinity"/> will be written as their corresponding JSON string representations.
        AllowNamedFloatingPointLiterals = 0x4
    }

     public partial class JsonNumberHandlingAttribute : JsonAttribute
    {
        public JsonNumberHandling Handling { get; }
    
        public JsonNumberHandlingAttribute(JsonNumberHandling handling)
        {
            Handling = handling;
        }
    }
}