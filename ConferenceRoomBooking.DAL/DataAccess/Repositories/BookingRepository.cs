using AutoMapper;
using ConferenceRoomBooking.Common.Models;
using ConferenceRoomBooking.DAL.DataAccess.Contexts;
using ConferenceRoomBooking.DAL.DataAccess.Repositories.Abstractions;
using ConferenceRoomBooking.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRoomBooking.DAL.DataAccess.Repositories;

public class BookingRepository : IBookingRepository
{
    private readonly ConferenceRoomBookingDbContext _context;
    private readonly IMapper _mapper;

    public BookingRepository(ConferenceRoomBookingDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<BookingModel> AddAsync(BookingModel model)
    {
        var bookingEntity = _mapper.Map<BookingEntity>(model);
        var addedEntity = await _context.Bookings.AddAsync(bookingEntity);
        await _context.SaveChangesAsync();
        return _mapper.Map<BookingModel>(addedEntity.Entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        var bookingEntity = await _context.Bookings.FindAsync(id);
        if (bookingEntity != null)
        {
            _context.Bookings.Remove(bookingEntity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<BookingModel>> GetAllAsync()
    {
        var bookings = await _context.Bookings.AsNoTracking().ToListAsync();
        return _mapper.Map<List<BookingModel>>(bookings);
    }

    public async Task<BookingModel> GetByIdAsync(Guid id)
    {
        var booking = await _context.Bookings.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
        return _mapper.Map<BookingModel>(booking);
    }

    public async Task<List<BookingModel>> GetBookingsByUserIdAsync(Guid userId)
    {
        var bookings = await _context.Bookings
            .Where(b => b.UserId == userId)
            .AsNoTracking()
            .ToListAsync();
        return _mapper.Map<List<BookingModel>>(bookings);
    }

    public async Task<List<BookingModel>> GetBookingsByRoomIdAsync(Guid roomId)
    {
        var bookings = await _context.Bookings
            .Where(b => b.RoomId == roomId)
            .AsNoTracking()
            .ToListAsync();
        return _mapper.Map<List<BookingModel>>(bookings);
    }

    public async Task UpdateAsync(BookingModel model)
    {
        var bookingEntity = _mapper.Map<BookingEntity>(model);
        _context.Bookings.Update(bookingEntity);
        await _context.SaveChangesAsync();
    }
}