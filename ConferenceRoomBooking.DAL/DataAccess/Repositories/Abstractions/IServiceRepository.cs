using ConferenceRoomBooking.Common.Models;

namespace ConferenceRoomBooking.DAL.DataAccess.Repositories.Abstractions;

public interface IServiceRepository : IRepository<ServiceModel>
{
    Task<List<ServiceModel>> GetServicesByIdsAsync(List<Guid> serviceIds);
}