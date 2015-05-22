public interface Ibuff : Ispell
{
    int MaxBuffValue { get; set; }
    float BuffValueVariance { get; set; }
    float BaseBuffDuration { get; set; }
    float BuffTimeLeft { get; }
}