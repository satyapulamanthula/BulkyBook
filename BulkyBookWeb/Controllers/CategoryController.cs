using BulkyBookWeb.IRepositories;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

public class CategoryController : Controller
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryController(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public IActionResult Index()
    {
        IEnumerable<Category> objCategoryList = _categoryRepository.GetAllCategories();
        return View(objCategoryList);
    }

    // GET
    public IActionResult Create()
    {
        return View();
    }

    // POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category obj)
    {
        if (obj.Name == obj.DisplayOrder.ToString())
        {
            ModelState.AddModelError("Name", "The DisplayOrder cannot exactly match the Name.");
        }

        if (ModelState.IsValid)
        {
            _categoryRepository.CreateCategory(obj);
            TempData["success"] = "Category created successfully";
            return RedirectToAction("Index");
        }

        return View(obj);
    }

    // GET
    public IActionResult Edit(int id)
    {
        if ( id <= 0)
        {
            return NotFound();
        }

        var categoryFromDb = _categoryRepository.GetCategoryById(id);

        if (categoryFromDb == null)
        {
            return NotFound();
        }

        return View(categoryFromDb);
    }

    // POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Category obj)
    {
        if (obj.Name == obj.DisplayOrder.ToString())
        {
            ModelState.AddModelError("Name", "The DisplayOrder cannot exactly match the Name.");
        }

        if (ModelState.IsValid)
        {
            _categoryRepository.UpdateCategory(obj);
            TempData["success"] = "Category updated successfully";
            return RedirectToAction("Index");
        }

        return View(obj);
    }

    public IActionResult Delete(int id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        var categoryFromDb = _categoryRepository.GetCategoryById(id);

        if (categoryFromDb == null)
        {
            return NotFound();
        }

        return View(categoryFromDb);
    }

    // POST
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePOST(int id)
    {
        var obj = _categoryRepository.GetCategoryById(id);

        if (obj == null)
        {
            return NotFound();
        }

        _categoryRepository.DeleteCategory(obj);
        TempData["success"] = "Category deleted successfully";
        return RedirectToAction("Index");
    }
}