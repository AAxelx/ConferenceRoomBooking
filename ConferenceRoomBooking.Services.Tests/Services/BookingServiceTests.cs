using AutoMapper;
using ConferenceRoomBooking.DAL.DataAccess.Repositories.Abstractions;
using ConferenceRoomBooking.SAL.Services;
using Moq;
using Xunit;

namespace ConferenceRoomBooking.Tests.Services;

public class BookingServiceTests
{
    private readonly Mock<IBookingRepository> _mockBookingRepository;
    private readonly Mock<IRoomRepository> _mockRoomRepository;
    private readonly Mock<IServiceRepository> _mockServiceRepository;
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly BookingService _bookingService;
    
    public BookingServiceTests()
    {
        _mockBookingRepository = new Mock<IBookingRepository>();
        _mockRoomRepository = new Mock<IRoomRepository>();
        _mockServiceRepository = new Mock<IServiceRepository>();
        _mockUserRepository = new Mock<IUserRepository>();
        _mockMapper = new Mock<IMapper>();

        _bookingService = new BookingService(
            _mockBookingRepository.Object, 
            _mockRoomRepository.Object, 
            _mockServiceRepository.Object, 
            _mockUserRepository.Object, 
            _mockMapper.Object);
    }
    
    [Theory]
    [InlineData(100, "2024-10-04 08:00", 5, 0, 0, 450)]
    [InlineData(200, "2024-10-04 10:00", 6, 50, 0,  1250)]
    [InlineData(150, "2024-10-04 09:00", 3, 20, 0, 470)]
    [InlineData(200, "2024-10-04 14:00", 4, 100, 0, 900)]
    [InlineData(120, "2024-10-04 18:00", 2, 10, 15, 217)]
    public void CalculateRentalCost_ShouldReturnCorrectCost(decimal basePrice, string bookingDate, int durationHours, decimal servicePrice1, decimal servicePrice2, decimal expectedCost)
    {
        // Arrange
        var bookingDateTime = DateTime.Parse(bookingDate);
        var duration = TimeSpan.FromHours(durationHours);
    
        var selectedServices = new List<decimal> { servicePrice1, servicePrice2 }.Where(x => x != 0).ToList();

        // Act
        var cost = _bookingService.CalculateRentalCost(basePrice, bookingDateTime, duration, selectedServices);

        // Assert
        Assert.Equal(expectedCost, cost);
    }
}