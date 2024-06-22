using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using Avalonia.Data.Converters;

namespace IDIFY.ValueConverters;

public class FileInfoConverter : IMultiValueConverter
{
    /// <summary>
    /// Converts FileInfo via custom logic to a string representation
    /// </summary>
    /// <param name="values">FileInfo object and string object that is interpreted as data expression (regex)</param>
    /// <param name="targetType">not used</param>
    /// <param name="parameter">not used</param>
    /// <param name="culture">not used</param>
    /// <returns>String, containing informational piece of the original FileInfo</returns>
    /// <exception cref="ArgumentException">if no FileInfo object is in <paramref name="values"/></exception>
    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        FileInfo? fileInfo = null;
        string? dataExpression = null;
        
        foreach (var value in values)
        {
            if (value is FileInfo fi)
            {
                fileInfo = fi;
            }
            else if (value is string s)
            {
                dataExpression = s;
            }
        }

        if (fileInfo != null)
        {
            if (dataExpression != null)
            {
                try
                {
                    if (App.Debug) Console.WriteLine("FileInfoConverter doing regex with: " + dataExpression);
                    return Regex.Match(fileInfo.FullName, dataExpression).Value;
                }
                catch (Exception e)
                {
                    throw new Exception($"{nameof(FileInfoConverter)} PROCESSING ERROR: {e}");
                }
            }
            if (App.Debug) Console.WriteLine("FileInfoConverter returned fullname, parameter=" + parameter);
            return fileInfo.FullName;
        }

        throw new ArgumentException($"{nameof(FileInfoConverter)} ERROR: no FileInfo in values array!");
    }
}