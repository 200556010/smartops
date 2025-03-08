using Microsoft.AspNetCore.Http;
using Utility.Contract;
using System.Security.Claims;

namespace Service.Implementation
{

    /// <summary>
    /// Current User Service Implementation.
    /// </summary>
    /// <seealso cref="ICurrentUserService" />
    public class CurrentUserService : ICurrentUserService
    {
        #region || *** Private Variable *** ||
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion

        #region || *** CTOR *** ||
        /// <summary>
        /// Initializes a new instance of the <see cref="CurrentUserService"/> class.
        /// </summary>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            Token = _httpContextAccessor.HttpContext?.Request?.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty) ?? string.Empty;
        }
        #endregion

        #region || *** Public Method *** ||
        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public string? UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        /// <summary>
        /// Gets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string? UserName => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);

        /// <summary>
        /// Gets the user role.
        /// </summary>
        /// <value>
        /// The user role.
        /// </value>
        public string? UserRole => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Role);

        /// <summary>
        /// Gets the full name.
        /// </summary>
        /// <value>
        /// The full name.
        /// </value>
        public string? FullName => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.GivenName) + " " +
            _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Surname);

        /// <summary>
        /// Gets the phone number.
        /// </summary>
        /// <value>
        /// The phone number.
        /// </value>
        public string? PhoneNumber => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.MobilePhone);

        /// <summary>
        /// Gets the token.
        /// </summary>
        /// <value>
        /// The token.
        /// </value>
        public string Token { get; private set; }
        #endregion
    }
}
