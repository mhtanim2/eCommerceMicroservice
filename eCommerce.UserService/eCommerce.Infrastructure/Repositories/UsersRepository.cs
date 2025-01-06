using Dapper;
using eCommerce.Core.Entities;
using eCommerce.Core.RepositoryContracts;
using eCommerce.Infrastructure.DbContext;

namespace eCommerce.Infrastructure.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly DapperDbContext _context;

    public UsersRepository(DapperDbContext context)
    {
        this._context = context;
    }

    public async Task<ApplicationUser?> AddUser(ApplicationUser user)
    {
        user.UserID=Guid.NewGuid();
        string query = "INSERT INTO public.\"Users\"(\"UserID\", \"Email\", \"PersonName\", \"Gender\", \"Password\") VALUES(@UserID, @Email, @PersonName, @Gender, @Password)";
        int rowCountAffected = await _context.DbConnection.ExecuteAsync(query, user);

        if (rowCountAffected > 0)
            return user;
        else
            return null;
    }

    public async Task<ApplicationUser?> GetUserByEmailAndPassword(string? email, string? password)
    {
        string query = "SELECT * FROM public.\"Users\" WHERE \"Email\"=@Email AND \"Password\"=@Password";
        var parameters = new { Email = email, Password = password };

        ApplicationUser? user = await _context.DbConnection.QueryFirstOrDefaultAsync<ApplicationUser>(query, parameters);
        return user;
    }


    public async Task<ApplicationUser?> GetUserByUserID(Guid? userID)
    {
        var query = "SELECT * FROM public.\"Users\" WHERE \"UserID\" = @UserID";
        var parameters = new { UserID = userID };
        
        return await _context.DbConnection.QueryFirstOrDefaultAsync<ApplicationUser>(query, parameters);
    }
}

