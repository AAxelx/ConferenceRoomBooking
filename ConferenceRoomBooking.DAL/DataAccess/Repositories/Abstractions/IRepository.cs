namespace ConferenceRoomBooking.DAL.DataAccess.Repositories.Abstractions;

public interface IRepository<TModel>
{
    Task<TModel> GetByIdAsync(Guid id);
    Task<List<TModel>> GetAllAsync();
    Task<TModel> AddAsync(TModel model);
    Task UpdateAsync(TModel model);
    Task DeleteAsync(Guid id);
}