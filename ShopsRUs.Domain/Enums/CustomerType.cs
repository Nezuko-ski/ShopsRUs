using System.Runtime.Serialization;

namespace ShopsRUs.Domain.Enums
{
    public enum CustomerType
    {
        [EnumMember(Value ="Employee")]
        Employee,
        [EnumMember(Value = "Affiliate")]
        Affiliate,
        [EnumMember(Value = "Customer")]
        Customer
    }
}
