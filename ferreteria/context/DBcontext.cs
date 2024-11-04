using Microsoft.EntityFrameworkCore;

namespace ferreteria.context;

public class DBcontext : DbContext
{
    
    public DBcontext(DbContextOptions <DBcontext> options) : base(options)
    {
    }
    
    
    protected DBcontext()
    {
    }

}