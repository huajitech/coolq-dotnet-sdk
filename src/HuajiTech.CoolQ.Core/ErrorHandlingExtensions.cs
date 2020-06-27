namespace HuajiTech.CoolQ
{
    internal static class ErrorHandlingExtensions
    {
        internal static int CheckError(this int returnValue)
        {
            if (returnValue < 0)
            {
                throw new ApiException(
                    string.Format(
                        System.Globalization.CultureInfo.CurrentCulture,
                        CoreResources.UnexpectedReturnValue,
                        returnValue),
                    returnValue);
            }

            return returnValue;
        }

        internal static T CheckError<T>(this T? returnValue)
            where T : class
        {
            if (returnValue is null)
            {
                throw new ApiException(CoreResources.NullReturnValue);
            }

            return returnValue;
        }
    }
}