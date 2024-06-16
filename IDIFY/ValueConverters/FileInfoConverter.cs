using System;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using Avalonia.Data.Converters;
using IDIFY.ViewModels;

namespace IDIFY.ValueConverters;

public class FileInfoConverter : IValueConverter
{
    /// <summary>
    /// Converts FileInfo via custom logic to a string representation
    /// </summary>
    /// <param name="value">FileInfo object</param>
    /// <param name="targetType"></param>
    /// <param name="parameter">Regular expression to apply on full filename (complete path + filename)</param>
    /// <param name="culture"></param>
    /// <returns>String, containing informational piece of the original FileInfo</returns>
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
                if (value is FileInfo file)
                {
                    // ugly workaround as ConverterParameter always brings an Avalonia.Markup.Xaml.MarkupExtensions.CompiledBindingExtension
                    return Regex.Match(file.FullName, MainWindowViewModel.DataElementExpressionStatic).Value;
                    if (parameter is string dataExpression)
                    {
                        Console.WriteLine("FileInfoConverter doing regex with: " + dataExpression);
                        return Regex.Match(file.FullName, dataExpression).Value;
                    }
                    Console.WriteLine("FileInfoConverter returned fullname, parameter=" + parameter);
                    return file.FullName;
                }
                else
                {
                    throw new ArgumentException($"{nameof(FileInfoConverter)}.Convert({value}, {targetType}, {parameter}): value is of wrong type!");
                }
            }
            catch (Exception e)
            {
                throw new ArgumentException($"{nameof(FileInfoConverter)}.Convert({value}, {targetType}, {parameter}): ERROR: {e.Message}");
            }
        }
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return null;
    }
}