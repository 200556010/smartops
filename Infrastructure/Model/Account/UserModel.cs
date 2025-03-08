namespace Model.Account
{
    public class UserModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; set; } = null!;
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
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>
        /// The phone number.
        /// </value>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the profile image.
        /// </summary>
        /// <value>
        /// The profile image.
        /// </value>
        public string? ProfileImage { get; set; }

        /// <summary>
        /// Gets or sets the date of birth.
        /// </summary>
        /// <value>
        /// The date of birth.
        /// </value>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public short? Status { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string? Password {  get; set; }

        /// <summary>
        /// Gets or sets the confirm password.
        /// </summary>
        /// <value>
        /// The confirm password.
        /// </value>
        public string? ConfirmPassword {  get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the created on.
        /// </summary>
        /// <value>
        /// The created on.
        /// </value>
        public DateTime? CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the updated by.
        /// </summary>
        /// <value>
        /// The updated by.
        /// </value>
        public string? UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets the updated on.
        /// </summary>
        /// <value>
        /// The updated on.
        /// </value>
        public DateTime? UpdatedOn { get; set; }

        /// <summary>
        /// Gets or sets the months billed.
        /// </summary>
        /// <value>
        /// The months billed.
        /// </value>
        public short? MonthsBilled { get; set; }
        /// <summary>
        /// Gets or sets the next billing date.
        /// </summary>
        /// <value>
        /// The next billing date.
        /// </value>
        public DateTime? NextBillingDate { get; set; }

        /// <summary>
        /// Gets or sets the billing amount.
        /// </summary>
        /// <value>
        /// The billing amount.
        /// </value>
        public decimal? BillingAmount { get; set; }

        /// <summary>
        /// Gets or sets the device token.
        /// </summary>
        /// <value>
        /// The device token.
        /// </value>
        public string? DeviceToken {  get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public string? CountryCode { get; set; }
        /// <summary>
        /// Gets or sets the last sync time
        /// </summary>
        public DateTime? LastSync { get; set; }

        /// <summary>
        /// Gets or sets the User subscription.
        /// </summary>
        /// <value>
        /// The User has subscribed or not.
        /// </value>
        public bool IsSubscribed { get; set; } = false;
        /// <summary>
        /// Gets or sets the User subscription cancellation status.
        /// </summary>
        /// <value>
        /// The User has cancelled or not.
        /// </value>
        public bool IsSubscriptionCancelled { get; set; } = false;
    }
}
