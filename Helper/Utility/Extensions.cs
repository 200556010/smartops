using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Web;

namespace Utility
{
    /// <summary>
    /// Extensions
    /// </summary>
    public static class Extensions
    {

        #region || ** Type Conversion Extension ** ||


        /// <summary>
        /// Converts to type.
        /// </summary>
        /// <typeparam name="TReturn">The type of the return.</typeparam>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static TReturn ToType<TReturn>(this object model)
        {
            if (model == null)
#pragma warning disable CS8603 // Possible null reference return.
                return default;
#pragma warning restore CS8603 // Possible null reference return.

            var jsonString = JsonConvert.SerializeObject(model, new JsonSerializerSettings()
            {
                //PreserveReferencesHandling = PreserveReferencesHandling.All,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore
            });
#pragma warning disable CS8603 // Possible null reference return.
            return JsonConvert.DeserializeObject<TReturn>(jsonString);
#pragma warning restore CS8603 // Possible null reference return.
        }

        #endregion

        #region || ** Date Time Extension ** ||

        /// <summary>
        /// Converts to local.
        /// </summary>
        /// <param name="utcDateTime">The UTC date time.</param>
        /// <returns></returns>
        public static DateTime ToLocal(this DateTime utcDateTime)
        {
            TimeZoneInfo timeZoneInfo = TimeZoneInfo.Local;
            var utcKindDate = DateTime.SpecifyKind(utcDateTime, DateTimeKind.Utc);
            return TimeZoneInfo.ConvertTimeFromUtc(utcKindDate, timeZoneInfo);
        }

        /// <summary>
        /// Converts to local.
        /// </summary>
        /// <param name="utcDateTime">The UTC date time.</param>
        /// <param name="timeZone">The time zone.</param>
        /// <returns></returns>
        public static DateTime? ToLocal(this DateTime? utcDateTime, string? timeZone = null)
        {
            if (!utcDateTime.HasValue)
                return utcDateTime;

            TimeZoneInfo timeZoneInfo = TimeZoneInfo.Local;

            if (!string.IsNullOrEmpty(timeZone))
                timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZone);

            var utcKindDate = DateTime.SpecifyKind(utcDateTime.Value, DateTimeKind.Utc);
            return TimeZoneInfo.ConvertTimeFromUtc(utcKindDate, timeZoneInfo);
        }

        /// <summary>
        /// Gets the age.
        /// </summary>
        /// <param name="dateOfBirth">The date of birth.</param>
        /// <returns></returns>
        public static int? GetAge(this DateTime? dateOfBirth)
        {
            var today = DateTime.Today;

            var a = (today.Year * 100 + today.Month) * 100 + today.Day;
            var b = (dateOfBirth?.Year * 100 + dateOfBirth?.Month) * 100 + dateOfBirth?.Day;

            return (a - b) / 10000;
        }

        /// <summary>
        /// Converts to datestring.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static string? ToDateString(this DateTime? dateTime)
        {
            if (dateTime.HasValue)
            {
                return Convert.ToDateTime(dateTime).ToString(Application.DateFormat);
            }
            return null;
        }

        /// <summary>
        /// Converts to datetimestring.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static string? ToDateTimeString(this DateTime? dateTime)
        {
            if (dateTime.HasValue)
            {
                return Convert.ToDateTime(dateTime).ToString(Application.DateTimeFormat);
            }
            return null;
        }

        /// <summary>
        /// Betweens the specified start.
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns></returns>
        public static bool Between(this DateTime dt, DateTime start, DateTime end)
        {
            if (start < end) return dt >= start && dt <= end;
            return dt >= end && dt <= start;
        }

        /// <summary>
        /// Converts total minutes to 00:00:00 format.
        /// </summary>
        /// <param name="totalMinutes">The total minutes.</param>
        /// <returns></returns>
        public static string ToHMSFormat(this string? totalMinutes)
        {
            if (string.IsNullOrEmpty(totalMinutes))
                return string.Empty;

            if (double.TryParse(totalMinutes, out double totalMinuteInt))
            {
                TimeSpan time = TimeSpan.FromMinutes(totalMinuteInt);
                return ConvertTimeToText(time);
            }
            return string.Empty;
        }
        public static string ConvertToHMSFormat(string? hour, string? minute, string? second)
        {
            TimeSpan time = new TimeSpan(Convert.ToInt32(hour), Convert.ToInt32(minute), Convert.ToInt32(second));

            return ConvertTimeToText(time);
        }

        private static string ConvertTimeToText(TimeSpan timeSpan)
        {
            int hours = timeSpan.Hours;
            int minutes = timeSpan.Minutes;
            int seconds = timeSpan.Seconds;

            string textRepresentation = "";

            if (hours > 0)
            {
                //textRepresentation += $"{hours} {(hours == 1 ? "hour" : "hours")} ";
                textRepresentation += $"{hours} h ";
            }

            if (minutes > 0)
            {
                // textRepresentation += $"{minutes} {(minutes == 1 ? "minute" : "minutes")} ";
                textRepresentation += $"{minutes} min ";
            }

            if (seconds > 0)
            {
                //textRepresentation += $"{seconds} {(seconds == 1 ? "second" : "seconds")} ";
                textRepresentation += $"{seconds} sec ";
            }

            // Trim any trailing space
            textRepresentation = textRepresentation.Trim();

            return textRepresentation;
        }

        #endregion

        #region || ** Numeric Extension ** ||

        /// <summary>
        /// Converts to uscurrency.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <returns></returns>
        public static string? ToUSCurrency(this decimal? amount)
        {
            if (amount.HasValue)
                return Convert.ToDecimal(amount).ToString("C", new CultureInfo(Application.CurrencyCultureInfo));
            return null;
        }

        /// <summary>
        /// Converts to uscurrency.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <returns></returns>
        public static string ToUSCurrency(this decimal amount)
        {
            return Convert.ToDecimal(amount).ToString("C", new CultureInfo(Application.CurrencyCultureInfo));
        }

        #endregion

        #region || ** Enum Extension ** ||

        /// <summary>
        /// Gets the display name.
        /// </summary>
        /// <param name="enu">The enu.</param>
        /// <returns></returns>
        public static string? GetDisplayName(this Enum enu)
        {

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            FieldInfo fieldInfo = enu.GetType().GetField(enu.ToString());
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

#pragma warning disable CS8604 // Possible null reference argument.
            return fieldInfo.GetCustomAttribute(typeof(DisplayAttribute)) switch
            {
                DisplayAttribute attribute => attribute.Name,
                _ => enu.ToString()
            };
#pragma warning restore CS8604 // Possible null reference argument.
        }

        /// <summary>
        /// Enums to select list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static SelectList? EnumToSelectList<T>()
        {
            var list = new List<KeyValuePair<int?, string?>>();
            Type ET = typeof(T);
            foreach (var item in Enum.GetValues(ET).Cast<Enum>())
                list.Add(new KeyValuePair<int?, string?>((int)Enum.Parse(item.GetType(), item.ToString()), item.GetDisplayName()));
            return new SelectList(list, "Key", "Value");
        }

        #endregion

        #region || ** File Extension ** ||

        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        /// <param name="fileUrl">The file URL.</param>
        /// <returns></returns>
        public static string GetFileName(this string fileUrl)
        {
            fileUrl = HttpUtility.UrlDecode(fileUrl);
            var fileName = new Uri(fileUrl).Segments.Last();
            return Uri.UnescapeDataString(fileName);
        }

        /// <summary>
        /// Gets the type of the content.
        /// </summary>
        /// <param name="filepath">The filepath.</param>
        /// <returns></returns>
        public static string GetContentType(this string filepath)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(filepath).ToLowerInvariant();
            return types[ext];
        }

        /// <summary>
        /// Gets the MIME types.
        /// </summary>
        /// <returns></returns>
        private static Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                    {".txt", "text/plain"},
                    {".pdf", "application/pdf"},
                    {".doc", "application/vnd.ms-word"},
                    {".docx", "application/vnd.ms-word"},
                    {".xls", "application/vnd.ms-excel"},
                    {".xlsx", "application/vnd.openxmlformats"},
                    {".png", "image/png"},
                    {".jpg", "image/jpeg"},
                    {".jpeg", "image/jpeg"},
                    {".gif", "image/gif"},
                    {".csv", "text/csv"}
            };
        }

        #endregion

        #region || ** URL Helper Extension ** ||

        /// <summary>
        /// Resets the password callback link.
        /// </summary>
        /// <param name="urlHelper">The URL helper.</param>
        /// <param name="resetUrl">The reset URL.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="code">The code.</param>
        /// <param name="scheme">The scheme.</param>
        /// <returns></returns>
        public static string ResetPasswordCallbackLink(this IUrlHelper urlHelper, string resetUrl, string userId, string code, string scheme)
        {
            return $"{resetUrl}?code={code}&userid={userId}";
        }

        /// <summary>
        /// Confirms the email callback link.
        /// </summary>
        /// <param name="urlHelper">The URL helper.</param>
        /// <param name="confirmEmailUrl">The confirm email URL.</param>
        /// <param name="code">The code.</param>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        public static string ConfirmEmailCallbackLink(this IUrlHelper urlHelper, string confirmEmailUrl, string code, string email, string scheme)
        {
            return $"{confirmEmailUrl}?code={code}&email={email}";
        }

        /// <summary>
        /// URLs the friendly.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="maxLength">The maximum length.</param>
        /// <returns></returns>
        public static string UrlFriendly(string text, int maxLength = 0)
        {
            // Return empty value if text is null
            if (text == null) return "";

            var normalizedString = text
                // Make lowercase
                .ToLowerInvariant()
                // Normalize the text
                .Normalize(NormalizationForm.FormD);

            var stringBuilder = new StringBuilder();
            var stringLength = normalizedString.Length;
            var prevdash = false;
            var trueLength = 0;

            char c;

            for (int i = 0; i < stringLength; i++)
            {
                c = normalizedString[i];

                switch (CharUnicodeInfo.GetUnicodeCategory(c))
                {
                    // Check if the character is a letter or a digit if the character is a
                    // international character remap it to an ascii valid character
                    case UnicodeCategory.LowercaseLetter:
                    case UnicodeCategory.UppercaseLetter:
                    case UnicodeCategory.DecimalDigitNumber:
                        if (c < 128)
                            stringBuilder.Append(c);
                        else
                            stringBuilder.Append(RemapInternationalCharToAscii(c));

                        prevdash = false;
                        trueLength = stringBuilder.Length;
                        break;

                    // Check if the character is to be replaced by a hyphen but only if the last character wasn't
                    case UnicodeCategory.SpaceSeparator:
                    case UnicodeCategory.ConnectorPunctuation:
                    case UnicodeCategory.DashPunctuation:
                    case UnicodeCategory.OtherPunctuation:
                    case UnicodeCategory.MathSymbol:
                        if (!prevdash)
                        {
                            stringBuilder.Append('-');
                            prevdash = true;
                            trueLength = stringBuilder.Length;
                        }
                        break;
                }

                // If we are at max length, stop parsing
                if (maxLength > 0 && trueLength >= maxLength)
                    break;
            }

            // Trim excess hyphens
            var result = stringBuilder.ToString().Trim('-');

            // Remove any excess character to meet maxlength criteria
            return maxLength <= 0 || result.Length <= maxLength ? result : result.Substring(0, maxLength);
        }

        /// <summary>
        /// Remaps the international character to ASCII.
        /// </summary>
        /// <param name="c">The c.</param>
        /// <returns></returns>
        public static string RemapInternationalCharToAscii(char c)
        {
            string s = c.ToString().ToLowerInvariant();
            if ("àåáâäãåą".Contains(s))
            {
                return "a";
            }
            else if ("èéêëę".Contains(s))
            {
                return "e";
            }
            else if ("ìíîïı".Contains(s))
            {
                return "i";
            }
            else if ("òóôõöøőð".Contains(s))
            {
                return "o";
            }
            else if ("ùúûüŭů".Contains(s))
            {
                return "u";
            }
            else if ("çćčĉ".Contains(s))
            {
                return "c";
            }
            else if ("żźž".Contains(s))
            {
                return "z";
            }
            else if ("śşšŝ".Contains(s))
            {
                return "s";
            }
            else if ("ñń".Contains(s))
            {
                return "n";
            }
            else if ("ýÿ".Contains(s))
            {
                return "y";
            }
            else if ("ğĝ".Contains(s))
            {
                return "g";
            }
            else if (c == 'ř')
            {
                return "r";
            }
            else if (c == 'ł')
            {
                return "l";
            }
            else if (c == 'đ')
            {
                return "d";
            }
            else if (c == 'ß')
            {
                return "ss";
            }
            else if (c == 'þ')
            {
                return "th";
            }
            else if (c == 'ĥ')
            {
                return "h";
            }
            else if (c == 'ĵ')
            {
                return "j";
            }
            else
            {
                return "";
            }
        }
        #endregion

        /// <summary>
        /// Converts to json.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static string ToJson<T>(this T obj) =>
       System.Text.Json.JsonSerializer.Serialize<T>(obj, new JsonSerializerOptions
       {
           PropertyNameCaseInsensitive = true
       });

        public enum SubscriptionsInverval
        {
            Day = 1,
            Week = 2,
            Month = 3,
            Quarter = 4,
            Semiannual = 5,
            Year = 6
        }

        #region || ** String Extension ** ||
        /// <summary>
        /// Truncates the specified maximum chars.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="maxChars">The maximum chars.</param>
        /// <returns></returns>
        public static string Truncate(this string? text, int? maxChars)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;
            return text.Length <= maxChars ? text : text.Substring(0, maxChars ?? 20) + "...";
        }
        #endregion
    }
}
