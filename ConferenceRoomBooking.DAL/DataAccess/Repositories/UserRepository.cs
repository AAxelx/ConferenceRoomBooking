using AutoMapper;
using ConferenceRoomBooking.Common.Models;
using ConferenceRoomBooking.DAL.DataAccess.Contexts;
using ConferenceRoomBooking.DAL.DataAccess.Repositories.Abstractions;
using ConferenceRoomBooking.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRoomBooking.DAL.DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ConferenceRoomBookingDbContext _context;
    private readonly IMapper _mapper;

    public UserRepository(ConferenceRoomBookingDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserModel> GetByIdAsync(Guid id)
    {
        var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
        return _mapper.Map<UserModel>(user);
    }

    public async Task<List<UserModel>> GetAllAsync()
    {
        var users = await _context.Users.AsNoTracking().ToListAsync();
        return _mapper.Map<List<UserModel>>(users);
    }

    public async Task<UserModel> AddAsync(UserModel model)
    {
        var userEntity = _mapper.Map<UserEntity>(model);
        var addedEntity = await _context.Users.AddAsync(userEntity);
        await _context.SaveChangesAsync();
        return _mapper.Map<UserModel>(addedEntity.Entity);
    }

    public async Task UpdateAsync(UserModel model)
    {
        var userEntity = _mapper.Map<UserEntity>(model);
        _context.Users.Update(userEntity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var userEntity = await _context.Users.FindAsync(id);
        if (userEntity != null)
        {
            _context.Users.Remove(userEntity);
            await _context.SaveChangesAsync();
        }
    }
}
