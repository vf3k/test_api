using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TestApi.Core.Entity;
using TestApi.DTO;
using TestApi.Infrastructure.DAL;

namespace TestApi.Core.Services
{
    public class RegistrationService
    {
        private readonly AppDbContext dbContext;
        private readonly IMapper mapper;

        public RegistrationService(AppDbContext dbContext,IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<ServiceResponse<Registration>> CreateAsync(Registration model)
        {
            var sr = new ServiceResponse<Registration>();
            try
            {
                if (model.Organisation != null)
                {
                    //Chechk if org. already exists (by name, in real life should be by id) 
                    var org = await dbContext.Organisations.FirstOrDefaultAsync(x => x.Name == model.Organisation.Name);
                    if (org != null)
                        model.Organisation = org;
                }

                var person = await dbContext.Persons.FirstOrDefaultAsync(x => x.Email == model.Person.Email);
                if (person != null)
                    model.Person = person;
                
                await dbContext.Registrations.AddAsync(model);
                await dbContext.SaveChangesAsync();
                    return sr.Successful(model);
            }
            catch (Exception e)
            {
                return sr.Failed(e.Message, e);
            }
        }

        public async ValueTask<GetRegistrationResponse> GetByIdAsync(Guid id)
        {
            var res= await dbContext.Registrations.AsNoTracking()
                .Include(x => x.Organisation)
                    .ThenInclude(x => x.Address)
                .Include(x => x.Person)
                    .ThenInclude(x => x.Address)
                .FirstOrDefaultAsync(x => x.Id == id);
            return mapper.Map<GetRegistrationResponse>(res);
        }

        public async Task<List<Registration>> GetAllAsync()
        {
            return await dbContext.Registrations.AsNoTracking()
                .Include(x => x.Organisation)
                .ThenInclude(x => x.Address)
                .Include(x => x.Person)
                .ThenInclude(x => x.Address)
                .ToListAsync();
        }
    }
}