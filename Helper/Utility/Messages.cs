namespace Utility
{
    /// <summary>
    /// Messages
    /// </summary>
    public static class Messages
    {
        /// <summary>
        /// Gets the success.
        /// </summary>
        /// <value>
        /// Success
        /// </value>
        public static string Success { get; } = "Success";
        /// <summary>
        /// Gets the error.
        /// </summary>
        /// <value>
        /// Internal Error
        /// </value>
        public static string Error { get; } = "Internal Error";
        /// <summary>
        /// Gets the already exist.
        /// </summary>
        /// <value>
        /// already exist
        /// </value>
        public static string AlreadyExist { get; } = "{0} already exist";
        /// <summary>
        /// Gets the inserted.
        /// </summary>
        /// <value>
        /// successfully created
        /// </value>
        public static string Inserted { get; } = "{0} successfully created";
        /// <summary>
        /// Gets the linked.
        /// </summary>
        /// <value>
        /// The linked.
        /// </value>
        public static string Linked { get; } = "{0} successfully linked";
        /// <summary>
        /// Gets the already linked.
        /// </summary>
        /// <value>
        /// The already linked.
        /// </value>
        public static string AlreadyLinked { get; } = "{0} already linked with company";
        /// <summary>
        /// Gets the updated.
        /// </summary>
        /// <value>
        /// successfully updated
        /// </value>
        public static string Updated { get; } = "{0} successfully updated";
        /// <summary>
        /// Gets the deleted.
        /// </summary>
        /// <value>
        /// successfully deleted
        /// </value>
        public static string Deleted { get; } = "{0} has been deleted";
        /// <summary>
        /// Gets the no record found.
        /// </summary>
        /// <value>
        /// No Record Found
        /// </value>
        public static string NoRecordFound { get; } = "No Record Found";
        /// <summary>
        /// Gets the no image.
        /// </summary>
        /// <value>
        /// Please Select an Image
        /// </value>
        public static string NoImage { get; } = "Please Select an Image";

        /// <summary>
        /// Gets the not found.
        /// </summary>
        /// <value>
        /// not found.
        /// </value>
        public static string NotFound { get; } = "{0} not found.";

        /// <summary>
        /// Gets the saved.
        /// </summary>
        /// <value>
        /// successfully saved!
        /// </value>
        public static string Saved { get; } = "{0} successfully saved!";
        /// <summary>
        /// Gets the added.
        /// </summary>
        /// <value>
        /// successfully added!
        /// </value>
        public static string Added { get; } = "{0} successfully added!";

        /// <summary>
        /// The Successfully.
        /// </summary>
        /// <value>
        /// successfully
        /// </value>
        public static string Successfully { get; } = "{0} successfully";

        public static string ChangePassword { get; } = "Password updated successfully";

        public static string InvitationRejected { get; } = "Invitation rejected successfully";

        public static string InvitationAccepted { get; } = "Invitation accepted successfully";

        public static string InvalidToken { get; } = "Invalid token";

        public static string TokenExpired { get; } = "Token has expired.";

        public static string MeetingRequest { get; } = "Meeting requested sent successfully";

        public static string MeetingAccepted { get; } = "Meeting accepted successfully";

        public static string MeetingDeclined { get; } = "Meeting declined successfully";

        public static string InvitationTokenExpired { get; } = "It looks like the invitation link you clicked has expired. Please ask the inviter to send you a new one, or get in touch with support if you need help.";

        public static string AlreadyResponse { get; } = "The user has already responded to this {0}.";

        public static string InviteSent { get; } = "{0} invitation sent successfully.";

        public static string InviteAccepted { get; } = "{0} invitation accepted successfully.";

        public static string InviteRejected { get; } = "{0} invitation rejected successfully.";

        public static string InviteCancelled { get; } = "{0} invitation cancelled successfully.";

        public static string AlreadyInFamilyConnection { get; } = "{0} is already in your connection.";

        public static string InterviewInviteSent { get; } = "The interview invitation has been sent successfully.";
        public static string RescheduleInterviewInviteSent { get; } = "The rescheduled interview invitation has been sent successfully.";
        public static string PendingInvitition { get; } = "You have aready invited {0}, please check pending invitations.";

    }
}
