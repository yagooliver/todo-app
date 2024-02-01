using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Domain.Enums
{
    public enum ItemStatusEnum
    {
        New = 1,
        InProgress = 2,
        Done = 3,
        OnHold = 4,
        Blocked = 5,
        Deleted = 6
    }
}
