using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Akycha.Model;

public class Setting
{
    [Key] public required string Key { get; set; }

    public bool Value { get; set; }
}
