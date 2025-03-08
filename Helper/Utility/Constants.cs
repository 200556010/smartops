namespace Utility
{
    /// <summary>
    /// Constants
    /// </summary>
    public class Application
    {
        public const string DateFormat = "MM/dd/yyyy";
        public const string DateTimeFormat = "MM/dd/yyyy hh:mm tt";
        public const string CurrencyCultureInfo = "en-US";
        public const string CurrencySymbol = "$ ";
        public const string TimeFormat = "hh:mm tt";
    }

    /// <summary>
    /// Storage Folder
    /// </summary>
    public class StorageFolder
    {
        public const string Profile = "profile";
        public const string Recordings = "recordings";
        public const string Content = "content";
        public const string Audio = "audio";
        public const string AudioCoverImages = "audiocoverimages";
        public const string VideoCoverImages = "videocoverimages";
    }


    /// <summary>
    /// Email Template Constants
    /// </summary>
    public class EmailTemplateConstants
    {
        public const string ForgetPassword = "forgot-password";
        public const string WelcomePatient = "welcome-patient";
        public const string WelcomePractitioner = "welcome-practitioner";
        public const string WelcomeAdmin = "welcome-admin";
        public const string EmailConfirmation = "email-confirmation";
        public const string PractitionerAccountApproval = "practitioner-account-approval";
        public const string PractitionerAccountApproved = "practitioner-account-approved";
        public const string DeleteAccount = "delete-account";
        public const string SubscriptionPurchase = "subscription-purchase";
        public const string SubscriptionPaymentFailed = "subscription-payment-failed";
        public const string SubscriptionCanceled = "subscription-canceled";
        public const string PractitionerAccountRejected = "practitioner-account-rejected";

    }

    /// <summary>
    /// Notifications
    /// </summary>
    public class Notifications
    {
        public const string AllNotifications = "All Notifications";
    }

    public class RegexHelper
    {
        public const string EmailId = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
        //public const string Password = @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^\w\s]).{6,20}$";
        public const string Password = @"^(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^\w\s]).{6,20}$";
        public const string Name = @"^[a-zA-Z]+[ a-zA-Z-_]*$";
        public const string VimeoYoutubeUrl = @"^((?:https?:)?\/\/)?((?:www|m)\.)?((?:youtube(-nocookie)?\.com|youtu.be))(\/(?:[\w\-]+\?v=|embed\/|live\/|v\/)?)([\w\-]+)(\S+)?$|(?:http|https)?:?\/?\/?(?:www\.)?(?:player\.)?vimeo\.com\/(?:channels\/(?:\w+\/)?|groups\/(?:[^\/]*)\/videos\/|video\/|)(\d+)(?:|\/\?)";
        public const string Numbers = @"^(?!0+(\0*)?$)[0-9]+(\[0-9]*)?$";
        public const string PhoneNumber = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";
    }

    public class ModelErrorMessageHelper
    {
        public const string Password = "Password must be 6-20 characters, with at least one lowercase letter, one number, and one special character.";
    }
}