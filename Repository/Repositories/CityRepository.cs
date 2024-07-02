using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class CityRepository : BaseRepository<City>, ICityRepository
    {
        public CityRepository(AppDbContext context):base(context)
        {
            
        }

        public async Task<IEnumerable<City>> GetAllWithCountryAsync()
        {
            return await _context.Cities.Include(m => m.Country).ToListAsync();
        }

        public async Task<IEnumerable<City>> FilterAsync(string name, string countryName)
        {
            var query = _entities.AsQueryable();

            if(name is not null)
            {
                query = query.Where(m => m.Name == name);
            }

            if(countryName is not null)
            {
                query = query.Where(m => m.Country.Name == countryName);
            }
            
            return await query.ToListAsync();
        }
    }
}
