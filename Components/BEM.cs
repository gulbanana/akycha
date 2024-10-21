using System.Text;

namespace Akycha.Components;

static class BEM
{
    public static string Class(string blockElement, params (bool, string)[] modifiers)
    {
        var classes = new StringBuilder();

        classes.Append(blockElement);

        foreach (var (test, modifier) in modifiers)
        {
            if (test)
            {
                classes.Append(' ');
                classes.Append(modifier);
            }
        }

        return classes.ToString();
    }
}