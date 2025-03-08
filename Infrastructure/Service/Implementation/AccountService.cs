using Data.Entity;
using Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model.Account;
using Service.Contract;
using Service.Helper;
using System.Text;
using Utility;
using Utility.Contract;
using static Data.Helpers.PaginationExtensionMethod;

namespace Service.Implementation
{
    /// <summary>
    /// Account Service Implementation.
    /// </summary>
    /// <seealso cref="Service.Contract.IAccountService" />
    public class AccountService : IAccountService
    {
        #region || *** Private Variable *** ||
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<AccountService> _logger;
        private readonly SmartOpsContext _smartOpsContext;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ICurrentUserService _currentUserService;
        #endregion

        #region || *** Constructor *** ||
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountService" /> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="signInManager">The sign in manager.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="smsContext">The SMS context.</param>
        /// <param name="roleManager">The role manager.</param>
        /// <param name="currentUserService">The current user service.</param>
        public AccountService(UserManager<ApplicationUser> userManager,
           SignInManager<ApplicationUser> signInManager,
           ILogger<AccountService> logger, 
           SmartOpsContext smartOpsContext, 
           RoleManager<ApplicationRole> roleManager, 
           ICurrentUserService currentUserService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _smartOpsContext = smartOpsContext;
            _roleManager = roleManager;
            _currentUserService = currentUserService;
        }
        #endregion

        #region || *** Public Method *** ||
        /// <summary>
        /// Logins the asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response> LoginAsync(LoginModel model)
        {
            var response = new Response();
            try
            {
                model.Email = model.Email?.Trim();

                var user = await _userManager.FindByEmailAsync(model.Email ?? "");

                if (user == null)
                {
                    response.Success = false;
                    response.Message = "User not found.";
                    return response;
                }

                var roles = await _userManager.GetRolesAsync(user);

                if (roles == null || !roles.Any())
                {
                    response.Success = false;
                    response.Message = "User not found.";
                    return response;
                }

                var result = await _signInManager.PasswordSignInAsync(model.Email ?? "", model.Password ?? "", model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    response.Success = true;
                    response.Message = "Login successful.";
                }
                else if (result.IsLockedOut)
                {
                    response.Success = false;
                    response.Message = "User is locked out.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Login failed.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                response.Success = false;
                response.Message = "something went wrong.";
            }
            return response;
        }

        /// <summary>
        /// Forgots the password.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task<ResponseData<UserEmailModel>> ForgotPasswordAsync(ForgotPasswordModel model)
        {
            var response = new ResponseData<UserEmailModel>();
            try
            {
                model.Email = model.Email?.Trim();

                var user = await _userManager.FindByEmailAsync(model.Email ?? "");

                if (user == null)
                {
                    response.Success = false;
                    response.Message = "User not found.";
                    return response;
                }

                var roles = await _userManager.GetRolesAsync(user);

                if (roles == null || !roles.Any())
                {
                    response.Success = false;
                    response.Message = "User not found.";
                    return response;
                }

                var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

                if (!string.IsNullOrEmpty(resetToken))
                {
                    var encodedToken = Convert.ToBase64String(Encoding.UTF8.GetBytes(resetToken));

                    response.Success = true;
                    response.Message = "Token generated successfully.";
                    response.Data = new UserEmailModel
                    {
                        Token = encodedToken,
                        Email = model.Email ?? "",
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                    };
                    return response;
                }

                response.Success = false;
                response.Message = "Token generation failed.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                response.Success = false;
                response.Message = "something went wrong.";
            }

            return response;
        }

        /// <summary>
        /// Resets the password asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<ResponseData<UserEmailModel>> ResetPasswordAsync(ResetPasswordModel model)
        {
            var response = new ResponseData<UserEmailModel>();
            try
            {
                model.Email = model?.Email?.Trim();

                var user = await _userManager.FindByEmailAsync(model?.Email ?? string.Empty);

                if (user == null)
                {
                    response.Success = false;
                    response.Message = "User not found.";
                    return response;
                }

                var roles = await _userManager.GetRolesAsync(user);

                if (roles == null || !roles.Any())
                {
                    response.Success = false;
                    response.Message = "User not found.";
                    return response;
                }

                if (string.IsNullOrEmpty(model?.Code))
                {
                    response.Success = false;
                    response.Message = Messages.InvalidToken;
                    return response;
                }

                var decodedCode = Encoding.UTF8.GetString(Convert.FromBase64String(model.Code));

                var result = await _userManager.ResetPasswordAsync(user, decodedCode, model.Password ?? string.Empty);

                if (result.Succeeded)
                {
                    response.Success = true;
                    response.Message = "Password reset successful.";
                    response.Data = new UserEmailModel
                    {
                        Email = model.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                    };
                    return response;
                }

                response.Success = false;
                response.Message = "Password reset failed.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                response.Success = false;
                response.Message = "something went wrong.";
            }
            return response;
        }

        /// <summary>
        /// Logouts the asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<Response> LogoutAsync()
        {
            var response = new Response();
            try
            {
                await _signInManager.SignOutAsync();

                response.Success = true;
                response.Message = "Logout successful.";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                response.Success = false;
                response.Message = "something went wrong.";
            }
            return response;
        }

        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response> ChangePasswordAsync(ChangePasswordModel model)
        {
            var response = new Response();
            try
            {
                model.UserId ??= _currentUserService.UserId;

                var user = await _userManager.FindByIdAsync(model.UserId ?? "");

                if (user == null)
                {
                    response.Success = false;
                    response.Message = "User not found.";
                    return response;
                }

                var result = await _userManager.ChangePasswordAsync(user, model.OldPassword ?? "", model.NewPassword ?? "");

                if (!result.Succeeded)
                {
                    response.Success = false;
                    response.Message = string.Join(", ", result.Errors.Select(x => x.Description));
                    return response;
                }

                response.Success = true;
                response.Message = "Password changed successfully.";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                response.Success = false;
                response.Message = "something went wrong.";
            }
            return response;
        }

        /// <summary>
        /// Registraions the asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<ResponseData<UserEmailModel>> RegistrationAsync(UserModel model)
        {
            var response = new ResponseData<UserEmailModel>();
            try
            {
                if (IsExist(model.Email ?? "", model.Id)?.Success == true)
                {
                    response.Message = string.Format(Messages.AlreadyExist, model.Email);
                    response.Success = false;
                    response.Data = null;
                    return response;
                }

                var existingUser = await _userManager.FindByEmailAsync(model.Email ?? "");

                if (existingUser != null)
                {
                    existingUser.FirstName = model.FirstName.Trim();
                    existingUser.LastName = model.LastName.Trim();
                    existingUser.Email = model.Email;
                    existingUser.UserName = model.Email;
                    existingUser.NormalizedEmail = model.Email?.ToUpper();
                    existingUser.NormalizedUserName = model.Email?.ToUpper();
                    //existingUser.Status = (short)Status.Active;
                    //existingUser.EmailConfirmed = true;
                    //existingUser.DateOfBirth = DateTime.UtcNow;
                    //existingUser.UpdatedBy = _currentUserService.UserId;
                    //existingUser.UpdatedOn = DateTime.UtcNow;

                    var result = await _userManager.UpdateAsync(existingUser);

                    if (result.Succeeded)
                    {
                        if (!await _roleManager.RoleExistsAsync("Admin"))
                        {
                            await _roleManager.CreateAsync(new ApplicationRole { Name = "Admin" });
                        }

                        if (await _roleManager.RoleExistsAsync("Admin"))
                        {
                            await _userManager.AddToRoleAsync(existingUser, "Admin");
                        }

                        response.Success = true;
                        response.Message = "Registration successful.";
                        response.Data = null;
                        return response;
                    }

                    response.Success = false;
                    response.Message = string.Join(", ", result.Errors.Select(x => x.Description));
                    response.Data = null;
                    return response;

                }
                else
                {
                    var user = new ApplicationUser
                    {
                        FirstName = model.FirstName.Trim(),
                        LastName = model.LastName.Trim(),
                        Email = model.Email,
                        UserName = model.Email,
                        NormalizedEmail = model.Email?.ToUpper(),
                        NormalizedUserName = model.Email?.ToUpper(),
                        EmailConfirmed = true,
                    };

                    var result = await _userManager.CreateAsync(user, model.Password ?? "");

                    if (result.Succeeded)
                    {
                        if (!await _roleManager.RoleExistsAsync("Admin"))
                        {
                            await _roleManager.CreateAsync(new ApplicationRole { Name = "Admin" });
                        }

                        if (await _roleManager.RoleExistsAsync("Admin"))
                        {
                            await _userManager.AddToRoleAsync(user, "Admin");
                        }

                        response.Success = true;
                        response.Message = "Registration successful.";
                        response.Data = new UserEmailModel()
                        {
                            Email = model.Email,
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                        };
                        return response;
                    }

                    response.Success = false;
                    response.Message = string.Join(", ", result.Errors.Select(x => x.Description));
                    response.Data = null;
                    return response;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                response.Success = false;
                response.Message = "something went wrong.";
                response.Data = null;
            }
            return response;
        }

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <param name="dtParam">The dt parameter.</param>
        /// <returns></returns>
        public async Task<ResponseData<DataTableList<AdminListModel>>> GetListAsync(DTParameters dtParam)
        {
            var response = new ResponseData<DataTableList<AdminListModel>>();
            try
            {
                var query = _smartOpsContext.AspNetUsers
                    .Include(x => x.Roles)
                    .Where(x => x.Roles.Any(y => y.Name == "Admin"))
                    .Select(x => new AdminListModel
                    {
                        Id = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Email = x.Email,
                    }).OrderByDescending(o => o.CreatedOn).AsQueryable();

                if (dtParam.Columns != null)
                {
                    var sbFirstName = dtParam.Columns.FirstOrDefault(x => x.Name == nameof(AdminListModel.FirstName))?.Search?.Value?.Trim();
                    var sbLastName = dtParam.Columns.FirstOrDefault(x => x.Name == nameof(AdminListModel.LastName))?.Search?.Value?.Trim();
                    var sbEmail = dtParam.Columns.FirstOrDefault(x => x.Name == nameof(AdminListModel.Email))?.Search?.Value?.Trim();

                    if (!string.IsNullOrEmpty(sbFirstName))
                        query = query.Where(r => r.FirstName.ToLower().Contains(sbFirstName.ToLower()));

                    if (!string.IsNullOrEmpty(sbLastName))
                        query = query.Where(r => r.LastName.ToLower().Contains(sbLastName.ToLower()));

                    if (!string.IsNullOrEmpty(sbEmail))
                        query = query.Where(r => !string.IsNullOrEmpty(r.Email) && r.Email.ToLower().Contains(sbEmail.ToLower()));

                }

                if (dtParam.Order != null && dtParam.Order.Length > 0 && dtParam.Order[0].Column.ToString() != "0")
                {
                    query = DataTable.OrderBy(query, dtParam, AdminSort);
                }

                var result = new PagedResult<AdminListModel>();
                if (string.IsNullOrEmpty(dtParam.exportColumns))
                {
                    result = await query.GetPagedAsync(dtParam.PageIndex == 0 ? 1 : dtParam.PageIndex + 1, dtParam.Length);
                }
                else
                {
                    result.Results = query.ToList();
                }
                response.Data = new DataTableList<AdminListModel>()
                {
                    draw = dtParam.Draw,
                    recordsFiltered = result.RowCount,
                    recordsTotal = result.RowCount,
                    data = result.Results.AsEnumerable()
                };

                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                response.Success = false;
                response.Message = "something went wrong.";
            }
            return response;
        }
        #endregion

        #region || *** Private Methods *** ||

        /// <summary>
        /// Determines whether the specified email is exist.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        private Response IsExist(string email, string? id)
        {
            var response = new Response();
            if (!string.IsNullOrEmpty(email) && id != Guid.Empty.ToString())
            {
                response.Success = _smartOpsContext.AspNetUsers.Any(x => x.Email == email && x.Id != id);
                response.Message = response.Success ? Messages.Success : Messages.Error;
            }
            else
            {
                response.Success = _smartOpsContext.AspNetUsers.Any(x => x.Email == email);
                response.Message = response.Success ? Messages.Success : Messages.Error;
            }
            return response;
        }

        /// <summary>
        /// Taxes the sort.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="sortOrder">The sort order.</param>
        /// <returns></returns>
        private static IOrderedQueryable<AdminListModel> AdminSort(IOrderedQueryable<AdminListModel> query, string sortOrder)
        {
            if (!string.IsNullOrEmpty(sortOrder))
            {
                query = sortOrder switch
                {
                    "firstName" => query.ThenBy(x => x.FirstName),
                    "firstName DESC" => query.ThenByDescending(x => x.FirstName),
                    "lastName" => query.ThenBy(x => x.LastName),
                    "lastName DESC" => query.ThenByDescending(x => x.LastName),
                    "email" => query.ThenBy(x => x.Email),
                    "email DESC" => query.ThenByDescending(x => x.Email),
                    "dateOfBirth" => query.ThenBy(x => x.DateOfBirth),
                    "dateOfBirth DESC" => query.ThenByDescending(x => x.DateOfBirth),
                    _ => query.ThenByDescending(x => x.CreatedOn),
                };
            }
            return query;
        }


        #endregion
    }
}
