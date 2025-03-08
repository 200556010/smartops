using Microsoft.AspNetCore.Mvc;
using Model.Product;
using Service.Contract;
using Service.Helper;
using SmartOps.Models.Product;
using Utility;

namespace SmartOps.Controllers
{
    public class ProductController : Controller
    {
        #region || *** Private Variable *** ||
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ILogger<ProductController> _logger;
        //private readonly IFlashMessage _flashMessage;
        #endregion

        #region || *** Constructor *** ||
        /// <summary>
        /// Initializes a new instance of the <see cref="PlanController"/> class.
        /// </summary>
        /// <param name="planService">The plan service.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="flashMessage">The flash message.</param>
        public ProductController(IProductService productService, ILogger<ProductController> logger/*, IFlashMessage flashMessage*/, ICategoryService categoryService)
        {
            _productService = productService;
            _logger = logger;
            _categoryService = categoryService;
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
                BindViewBag();
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
                var response = await _productService.GetListAsync(param);
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
                BindViewBag();
                ProductViewModel viewModel = new ProductViewModel();
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
        public async Task<IActionResult> Add(ProductViewModel viewModel)
        {
            try
            {
                BindViewBag();
                ModelState.Remove("Id");
                ModelState.Remove("Status");

                if (!ModelState.IsValid)
                {
                    return View(viewModel);
                }

                var model = viewModel.ToType<ProductModel>();
                var response = await _productService.UpsertAsync(model);

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
                BindViewBag();
                var response = await _productService.GetAsync(id);
                if (response.Data != null)
                {
                    var viewModel = response.Data.ToType<ProductViewModel>();
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
                return Json(await _productService.DeleteAsync(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return View("_PageNotFound");
            }
        }
        #endregion

        #region || *** Private Method *** ||
        /// <summary>
        /// Binds the view bag.
        /// </summary>
        private void BindViewBag()
        {
            ViewBag.Category = _categoryService.GetCategoryList();
        }
        #endregion
    }
}
