using AutoMapper;
using ConferenceRoomBooking.Common.Models;
using ConferenceRoomBooking.DAL.DataAccess.Contexts;
using ConferenceRoomBooking.DAL.DataAccess.Repositories.Abstractions;
using ConferenceRoomBooking.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRoomBooking.DAL.DataAccess.Repositories;

public class RoomRepository : IRoomRepository
{
    private readonly ConferenceRoomBookingDbContext _context;
    private readonly IMapper _mapper;

    public RoomRepository(ConferenceRoomBookingDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<RoomModel> GetByIdAsync(Guid id)
    {
        var room = await _context.Rooms.AsNoTracking().Include(r => r.AvaliableServices).FirstOrDefaultAsync(r => r.Id == id);
        return _mapper.Map<RoomModel>(room);
    }
    
    public async Task<List<RoomModel>> GetAllAsync()
    {
        var rooms = await _context.Rooms.AsNoTracking().ToListAsync();
        return _mapper.Map<List<RoomModel>>(rooms);
    }

    public async Task<List<RoomModel>> GetAvailableRoomsAsync(DateTime startDateTime, DateTime endDateTime, int capacity)
    {
        var availableRooms = await _context.Rooms
            .Where(r => r.Capacity >= capacity && 
                        !r.Bookings.Any(b => 
                            b.StartTime < endDateTime && 
                            b.EndTime > startDateTime))
            .Include(r => r.AvaliableServices)
            .AsNoTracking()
            .ToListAsync();

        return _mapper.Map<List<RoomModel>>(availableRooms);
    }

    public async Task<RoomModel> AddAsync(RoomModel model)
    {
        var roomEntity = _mapper.Map<RoomEntity>(model);
        var addedEntity = await _context.Rooms.AddAsync(roomEntity);
        _context.Services.AttachRange(roomEntity.AvaliableServices);

        await _context.SaveChangesAsync();
        return _mapper.Map<RoomModel>(addedEntity.Entity);
    }

    public async Task UpdateAsync(RoomModel model)
    {
        var existingRoom = await _context.Rooms
            .Include(r => r.AvaliableServices)
            .FirstOrDefaultAsync(r => r.Id == model.Id);

        _context.Entry(existingRoom).CurrentValues.SetValues(model);
        existingRoom.AvaliableServices.Clear();

        foreach (var serviceModel in model.AvailableServices)
        {
            var serviceEntity = await _context.Services
                .FirstOrDefaultAsync(s => s.Id == serviceModel.Id);

            if (serviceEntity != null)
            {
                existingRoom.AvaliableServices.Add(serviceEntity);
            }
        }

        await _context.SaveChangesAsync();
    }


    public async Task DeleteAsync(Guid id)
    {
        var roomEntity = await _context.Rooms.FindAsync(id);
        if (roomEntity != null)
        {
            _context.Rooms.Remove(roomEntity);
            await _context.SaveChangesAsync();
        }
    }
}
