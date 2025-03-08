using Microsoft.AspNetCore.Identity;

namespace Data.Identity
{
    /// <summary>
    /// Application User
    /// </summary>
    /// <seealso cref="IdentityUser" />
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public short Status { get; set; }
    }
}
