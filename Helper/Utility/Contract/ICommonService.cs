using Microsoft.AspNetCore.Mvc.Rendering;

namespace Utility.Contract
{
    /// <summary>
    /// ICommonService
    /// </summary>
    public interface ICommonService
    {
        /// <summary>
        /// Gets default profile url.
        /// </summary>
        /// <value>
        /// The default profile url.
        /// </value>
        string? DefaultProfilePhotoUrl { get; }


        /// <summary>
        /// Gets the time zones.
        /// </summary>
        /// <returns></returns>
        List<SelectListItem> GetTimeZones();

        /// <summary>
        /// Gets the users list.
        /// </summary>
        /// <returns></returns>
        List<SelectListItem> GetUsersList();

        /// <summary>
        /// Generates the unique code asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<string> GenerateUniqueCodeAsync();


    }
}
