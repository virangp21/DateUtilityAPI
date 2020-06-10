using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace DateUtilityAPI.Domain
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum State
    {
       [EnumMember(Value ="All")]
        All = 0, // To Represents National Holidays

        [EnumMember(Value = "ACT")]
        ACT,

        [EnumMember(Value = "NSW")]
        NSW,

        [EnumMember(Value = "NT")]
        NT,

        [EnumMember(Value = "QLD")]
        QLD,

        [EnumMember(Value = "SA")]
        SA,

        [EnumMember(Value = "TAS")]
        TAS,

        [EnumMember(Value = "VIC")]
        VIC,

        [EnumMember(Value = "WA")]
        WA
    }
}
