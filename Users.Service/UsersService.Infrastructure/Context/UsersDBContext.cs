using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.DataAccess.Entities;

namespace UsersService.Infrastructure.Context
{
    public class UsersDBContext : DbContext
    {
        public UsersDBContext(DbContextOptions options) : base(options) => Database.Migrate();

        public DbSet<UserEntity> Users { get; set; }
    }
}
