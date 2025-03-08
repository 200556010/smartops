namespace Utility.Contract
{
    /// <summary>
    /// Current User Service Contract
    /// </summary>
    public interface ICurrentUserService
    {
        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        string? UserId { get; }
        /// <summary>
        /// Gets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        string? UserName { get; }
        /// <summary>
        /// Gets the user role.
        /// </summary>
        /// <value>
        /// The user role.
        /// </value>
        string? UserRole { get; }
        /// <summary>
        /// Gets the full name.
        /// </summary>
        /// <value>
        /// The full name.
        /// </value>
        string? FullName { get; }

    }
}
