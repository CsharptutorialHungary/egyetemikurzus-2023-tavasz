using DU0038.Model;

namespace DU0038.Service;

public class CategoryService
{
    private static CategoryService? _instance;
    private static readonly object Padlock = new();
    private List<Category> _categories = new List<Category>();

    private CategoryService()
    {
    }
    
    public async Task InitializeCategories()
    {
        _categories = await FileService.Instance.ReadCategoriesFromFile();
    }
    
    public static CategoryService Instance
    {
        get
        {
            lock (Padlock)
            {
                return _instance ??= new CategoryService();
            }
        }
    }

    public void AddCategory(string name, bool isIncome)
    {
        _categories.Add(new Category
        (
            Guid.NewGuid().ToString(),
            name,
            isIncome
        ));
    }

    public async Task SaveCategories()
    {
        await FileService.Instance.WriteCategoriesToFile(_categories);
    }

    public List<Category> GetCategories()
    {
        return _categories;
    }
    
    public List<Category> GetCategoriesInNameOrder()
    {
        return _categories.Select(category => category)
            .OrderBy(category => category.Name).ToList();
    }
}