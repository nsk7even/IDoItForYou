using System;
using System.Globalization;
using System.IO;
using Avalonia.Data.Converters;

namespace IDIFY.ValueConverters;

public class DirectoryInfoToStringConverter : IValueConverter
{
    /// <summary>
    /// Converts DirectoryInfo to string, using DirectoryInfo.FullName
    /// </summary>
    /// <param name="value"></param>
    /// <param name="targetType"></param>
    /// <param name="parameter"></param>
    /// <param name="culture"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value == null)
        {
            return string.Empty;
        }
        else
        {
            try
            {
                if (value is DirectoryInfo directory)
                {
                    return directory.FullName;
                }
                else
                {
                    throw new ArgumentException($"{nameof(DirectoryInfoToStringConverter)}.Convert({value}, {targetType}, {parameter}): value is of wrong type!");
                }
            }
            catch (Exception e)
            {
                throw new ArgumentException($"{nameof(DirectoryInfoToStringConverter)}.Convert({value}, {targetType}, {parameter}): ERROR: {e.Message}");
            }
        }
    }

    /// <summary>
    /// Converts string to DirectoryInfo, while interpreting string as path
    /// </summary>
    /// <param name="value"></param>
    /// <param name="targetType"></param>
    /// <param name="parameter"></param>
    /// <param name="culture"></param>
    /// <returns></returns>
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        try
        {
            if (value is string s)
            {
                return string.IsNullOrWhiteSpace(s) ? null : new DirectoryInfo(s);
            }
            else
            {
                return null;
            }
        }
        catch (Exception e)
        {
            throw new ArgumentException($"{nameof(DirectoryInfoToStringConverter)}.ConvertBack({value}, {targetType}, {parameter}): ERROR: {e.Message}");
        }
    }
}