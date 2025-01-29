using System;
using System.Collections.Generic;

namespace EtherumData.Models;

public partial class Transaction
{
    public string Id { get; set; } = null!;

    public long Value { get; set; }

    public long GasUsed { get; set; }

    public long BlockNumber { get; set; }

    public string FromAddress { get; set; } = null!;

    public string ToAdress { get; set; } = null!;
}
