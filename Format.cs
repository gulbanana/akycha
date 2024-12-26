static class Format
{
    public static string Power(double usage)
    {
        if (usage == 0)
        {
            return "";
        }
        else if (usage < 1000)
        {
            return $"{usage} MW";
        }
        else
        {
            return $"{Math.Round((double)usage / 1000.0, 1):0.0} GW";
        }
    }
}