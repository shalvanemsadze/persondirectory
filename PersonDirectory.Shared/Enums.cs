using System;
using System.Collections.Generic;
using System.Text;

namespace PersonDirectory.Shared
{
    public enum OperationTypeEnum : byte
    {
        Add = 1,
        Edit = 2
    }

    public enum GenderEnum : byte
    {
        Male = 1,
        Female = 2
    }

    public enum RelationTypeEnum : byte
    {
        Colleague = 1,
        Acquaintance = 2,
        Relative = 3
    }

    public enum PhoneNumberTypeEnum : byte
    {
        Mobile = 1,
        Office = 2,
        Home = 3
    }
}
