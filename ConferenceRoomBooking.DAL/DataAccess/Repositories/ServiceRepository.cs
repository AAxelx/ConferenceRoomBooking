using AutoMapper;
using ConferenceRoomBooking.Common.Models;
using ConferenceRoomBooking.DAL.DataAccess.Contexts;
using ConferenceRoomBooking.DAL.DataAccess.Repositories.Abstractions;
using ConferenceRoomBooking.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRoomBooking.DAL.DataAccess.Repositories;

public class ServiceRepository : IServiceRepository
{
    private readonly ConferenceRoomBookingDbContext _context;
    private readonly IMapper _mapper;

    public ServiceRepository(ConferenceRoomBookingDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ServiceModel>> GetServicesByIdsAsync(List<Guid> serviceIds)
    {
        var services = await _context.Services.AsNoTracking()
            .Where(service => serviceIds.Contains(service.Id))
            .ToListAsync();
        
        return _mapper.Map<List<ServiceModel>>(services);
    }
    
    public async Task<ServiceModel> AddAsync(ServiceModel model)
    {
        var serviceEntity = _mapper.Map<ServiceEntity>(model);
        var addedEntity = await _context.Services.AddAsync(serviceEntity);
        await _context.SaveChangesAsync();
        return _mapper.Map<ServiceModel>(addedEntity.Entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        var serviceEntity = await _context.Services.FindAsync(id);
        if (serviceEntity != null)
        {
            _context.Services.Remove(serviceEntity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<ServiceModel>> GetAllAsync()
    {
        var services = await _context.Services.AsNoTracking().ToListAsync();
        return _mapper.Map<List<ServiceModel>>(services);
    }

    public async Task<ServiceModel> GetByIdAsync(Guid id)
    {
        var service = await _context.Services.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
        return _mapper.Map<ServiceModel>(service);
    }

    public async Task UpdateAsync(ServiceModel model)
    {
        var serviceEntity = _mapper.Map<ServiceEntity>(model);
        _context.Services.Update(serviceEntity);
        await _context.SaveChangesAsync();
    }
}
