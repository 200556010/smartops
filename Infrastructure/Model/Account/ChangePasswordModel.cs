using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Account
{
    public class ChangePasswordModel
    {
        /// <summary>
        /// Gets or sets the user Id.
        /// </summary>
        /// <value>
        /// The user Id.
        /// </value>
        public string? UserId { get; set; }

        /// <summary>
        /// Gets or sets the old password.
        /// </summary>
        /// <value>
        /// The old password.
        /// </value>
        public string? OldPassword { get; set; }

        /// <summary>
        /// Gets or sets the new password.
        /// </summary>
        /// <value>
        /// The new password.
        /// </value>
        public string? NewPassword { get; set; }
    }
}
