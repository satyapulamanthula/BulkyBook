using BulkyBookWeb.Data;
using BulkyBookWeb.IRepositories;
using BulkyBookWeb.Models;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CategoryRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<Category> GetAllCategories()
    {
        return _dbContext.Categories;
    }

    public Category GetCategoryById(int id)
    {
        return _dbContext.Categories.Find(id);
    }

    public void CreateCategory(Category category)
    {
        _dbContext.Categories.Add(category);
        _dbContext.SaveChanges();
    }

    public void UpdateCategory(Category category)
    {
        _dbContext.Categories.Update(category);
        _dbContext.SaveChanges();
    }

    public void DeleteCategory(Category category)
    {
        _dbContext.Categories.Remove(category);
        _dbContext.SaveChanges();
    }
}
