namespace Integration.Shared.Google;

public class DateTimeConverterService : IDateTimeConverterService
{
    public string Convert(string datetime)
    {
        return string.Format("{0}-{1}-{2} {3}:{4}:{5}{6}",
                            datetime.Substring(0, 4),
                            datetime.Substring(4, 2),
                            datetime.Substring(6, 2),
                            datetime.Substring(9, 2),
                            datetime.Substring(11, 2),
                            datetime.Substring(13, 2),
                            datetime.Substring(16, 6));
    }
}
