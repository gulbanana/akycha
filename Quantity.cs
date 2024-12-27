namespace Akycha;

public record Quantity(int? PartId, float? Amount)
{
    public const int PowerIcon = -1;
    public const int TransportIcon = -2;
    public const int CouponIcon = -3;

    public const int VisibleIcons = -1;
}