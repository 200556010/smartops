using Microsoft.AspNetCore.Mvc;
using Model.Category;
using Service.Contract;
using Service.Helper;
using SmartOps.Models.Category;
using Utility;

namespace SmartOps.Controllers
{
    /// <summary>
    /// Category Controller.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class CategoryController : Controller
    {
        #region || *** Private Variable *** ||
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoryController> _logger;
        //private readonly IFlashMessage _flashMessage;
        #endregion

        #region || *** Constructor *** ||
        /// <summary>
        /// Initializes a new instance of the <see cref="PlanController"/> class.
        /// </summary>
        /// <param name="planService">The plan service.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="flashMessage">The flash message.</param>
        public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger/*, IFlashMessage flashMessage*/)
        {
            _categoryService = categoryService;
            _logger = logger;
            //_flashMessage = flashMessage;
        }
        #endregion

        #region || *** Public Method *** ||
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return View("_PageNotFound");
            }
        }

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetList(DTParameters param)
        {
            try
            {
                var response = await _categoryService.GetListAsync(param);
                return response.Success ? Json(response.Data) : Json(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return View("_PageNotFound");
            }
        }

        /// <summary>
        /// Adds this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Add()
        {
            try
            {
                CategoryViewModel viewModel = new CategoryViewModel();
                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return View("_PageNotFound");
            }

        }

        /// <summary>
        /// Adds the specified view model.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(CategoryViewModel viewModel)
        {
            try
            {

                ModelState.Remove("Id");
                ModelState.Remove("Status");

                if (!ModelState.IsValid)
                {
                    return View(viewModel);
                }

                var model = viewModel.ToType<CategoryModel>();
                var response = await _categoryService.UpsertAsync(model);

                if (response.Success)
                {

                    return RedirectToAction(nameof(Index));
                }
                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return View("_PageNotFound");
            }

        }

        /// <summary>
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                var response = await _categoryService.GetAsync(id);
                if (response.Data != null)
                {
                    var viewModel = response.Data.ToType<CategoryViewModel>();
                    return View("Add", viewModel);
                }
                return View("Add");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return View("_PageNotFound");
            }
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                return Json(await _categoryService.DeleteAsync(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return View("_PageNotFound");
            }
        }
        #endregion
    }
}
