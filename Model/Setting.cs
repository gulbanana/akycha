using System.ComponentModel.DataAnnotations;

namespace Akycha.Model;

public class Setting
{
    [Key] public required string Key { get; set; }

    public required string Value { get; set; }
}
