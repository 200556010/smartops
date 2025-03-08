using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using static System.Net.Mime.MediaTypeNames;

namespace Service.Helper
{

    /// <summary>
    /// Files Common Helper
    /// </summary>
    public static class FilesCommonHelper
    {
        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        /// <param name="fileUrl">The file URL.</param>
        /// <returns></returns>
        public static string GetFileName(string fileUrl)
        {
            fileUrl = HttpUtility.UrlDecode(fileUrl);
            var fileName = new Uri(fileUrl).Segments.Last();
            return Uri.UnescapeDataString(fileName);
        }

        /// <summary>
        /// Gets the MIME types.
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetMimeTypes() => new Dictionary<string, string>
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
                {".csv", "text/csv"},
                {".mp3", "audio/mpeg"},
                {".aac", "audio/aac"},
                {".mp4", "video/mp4"},
                {".mpeg", "video/mpeg"}

        };

        /// <summary>
        /// Gets the type of the content.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public static string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }
    }

    /// <summary>
    /// Common Helper
    /// </summary>
    public static class CommonHelper
    {
        public static string GetUniqueCode()
        {
            // Create an instance of Random class
            Random random = new Random();

            // Define the alphabet characters
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            // Generate a StringBuilder to store the unique text
            StringBuilder uniqueText = new StringBuilder();

            // Generate 10 random characters from the alphabet
            for (int i = 0; i < 10; i++)
            {
                // Get a random index within the range of the alphabet
                int randomIndex = random.Next(0, alphabet.Length);

                // Append the randomly selected character to the unique text
                uniqueText.Append(alphabet[randomIndex]);
            }
            return uniqueText.ToString();
        }

        /// <summary>
        /// Parses the scheduled date time.
        /// </summary>
        /// <param name="scheduledDate">The scheduled date.</param>
        /// <param name="scheduledTime">The scheduled time.</param>
        /// <param name="timeZone">The time zone.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Invalid scheduled date format.</exception>
        public static DateTime ParseScheduledDateTime(string scheduledDate, string scheduledTime, string timeZone)
        {
            // Define acceptable date formats
            string[] dateFormats = { "MMM dd, yyyy", "MMM d, yyyy", "dd/MM/yyyy", "MM/dd/yyyy", "yyyy-MM-dd", "MM-dd-yyyy" };
            // Add more formats if needed

            // Try parsing the scheduled date with the provided formats
            if (!DateTime.TryParseExact(scheduledDate, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
            {
                throw new Exception("Invalid scheduled date format.");
            }

            // Combine the parsed date with the scheduled time
            var combinedDateTimeString = $"{date:dd-MM-yyyy} {scheduledTime}";

            // Parse the combined date and time
            var scheduledLocalTime = DateTime.ParseExact(
                combinedDateTimeString, // Combine date and time
                "dd-MM-yyyy HH:mm", // Expected format for parsing (adjust as necessary)
                CultureInfo.InvariantCulture
            );

            // Convert scheduled time to UTC based on the provided timezone
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZone);
            return TimeZoneInfo.ConvertTimeToUtc(scheduledLocalTime, timeZoneInfo); // Convert to UTC
        }

        /// <summary>
        /// Gets the hours and minutes.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <returns></returns>
        public static string GetHoursAndMinutes(string time)
        {
            // Check if the input is not null or empty
            if (string.IsNullOrEmpty(time))
            {
                return "Invalid time format.";
            }

            // Split the time string by ':'
            string[] timeParts = time.Split(':');

            // Ensure the split resulted in at least 2 parts (for hours and minutes)
            if (timeParts.Length < 2)
            {
                return "Invalid time format.";
            }

            // Get hours and minutes
            string hours = timeParts[0]; // First part is hours
            string minutes = timeParts[1]; // Second part is minutes

            // Format the result as "HH:mm"
            return $"{hours}:{minutes}";
        }

        public static string ConvertToCustomFormat(DateTime dateTime)
        {
            return dateTime.ToString("MMM dd yyyy hh:mm tt"); // Returns the date and time formatted as "Oct 08 2024 03:30 PM"
        }

        public static string ImagePath()
        {
            return "images/image-avatar.svg";
        }

        /// <summary>
        /// Generates the slug.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <returns></returns>
        public static string GenerateSlug(string title)
        {
            // Convert to lowercase
            title = title.ToLowerInvariant();

            // Remove invalid characters
            title = Regex.Replace(title, @"[^a-z0-9\s-]", "");

            // Replace spaces with hyphens
            title = Regex.Replace(title, @"\s+", "-").Trim();

            // Trim excess hyphens
            title = Regex.Replace(title, @"-+", "-");

            return title;
        }

    }


}
