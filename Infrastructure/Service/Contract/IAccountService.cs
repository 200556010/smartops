using Microsoft.AspNetCore.Http;
using Model.Account;
using Service.Helper;
using Utility;

namespace Service.Contract
{
    /// <summary>
    /// Account Service Contract.
    /// </summary>
    public interface IAccountService
    {

        /// <summary>
        /// Logins the asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response> LoginAsync(LoginModel model);

        /// <summary>
        /// Forgots the password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        Task<ResponseData<UserEmailModel>> ForgotPasswordAsync(ForgotPasswordModel model);

        /// <summary>
        /// Resets the password asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<ResponseData<UserEmailModel>> ResetPasswordAsync(ResetPasswordModel model);

        /// <summary>
        /// Logouts the asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<Response> LogoutAsync();

        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response> ChangePasswordAsync(ChangePasswordModel model);

        /// <summary>
        /// Registrations the asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<ResponseData<UserEmailModel>> RegistrationAsync(UserModel model);

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <param name="dtParam">The dt parameter.</param>
        /// <returns></returns>
        Task<ResponseData<DataTableList<AdminListModel>>> GetListAsync(DTParameters dtParam);

    }
}
