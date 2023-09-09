using Microsoft.EntityFrameworkCore;
using StockWallet.Domain.Infraestructure;
using StockWallet.Domain.Models;

namespace StockWallet.Db.Mysql.Repositories;

public class CompanyRepository: ICompanyRepository
{
    private readonly StockWalletContext _context;
    public CompanyRepository(StockWalletContext context)
    {
        _context = context;
    }
    
    public async Task<(Company company, string error)> Get(int id)
    {
        string error = string.Empty;
        Company? company = null;
        try
        {
            company = await _context.Companies.FirstOrDefaultAsync(x => x.CompanyId == id);
            if (company == null) error = "Item not found";
        }
        catch(Exception e)
        {
            error = e.Message;
        }

        return (company ?? new Company { Name = "Not found"}, error);
    }

    public async Task<(bool success, string error)> Remove(int id)
    {
        string error = string.Empty;

        try
        {
            Company? company = await _context.Companies.FirstOrDefaultAsync(x => x.CompanyId == id);

            if(company != null) _context.Companies.Remove(company);

            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            error = e.Message;
        }

        return (string.IsNullOrEmpty(error), error);
    }

    public async Task<(bool success, string error)> Insert(Company item)
    {
        string error = string.Empty;

        try
        {
            _context.Companies.Attach(item);

            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            error = e.Message;
        }

        return (string.IsNullOrEmpty(error), error);
    }

    public async Task<(List<Company> companies, string error)> All()
    {
        string error = string.Empty;
        var companies = new List<Company>(0);
        try
        {
            companies = await _context.Companies.ToListAsync();
        }
        catch (Exception e)
        {
            error = e.Message;
        }

        return (companies, error);
    }
}