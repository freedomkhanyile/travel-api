using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Travel.Api.Common.Exceptions;
using Travel.Api.Models.Bookings;
using Travel.Data.Access.DAL;
using Travel.Data.Model.Entities;
using Travel.Queries.Queries;
using Travel.Security;
using Xunit;

namespace Travel.Queries.Tests
{
    public class BookingQueryProcessorTests
    {
        private readonly IBookingsQueryProcessor _bookingsQueryProcessor;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<ISecurityContext> _securityContextMock;
        private readonly List<BookingEntity> _bookingList;
        private readonly UserEntity _currentUserEntity;

        public BookingQueryProcessorTests()
        {
         
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _bookingList = new List<BookingEntity>();
            _unitOfWorkMock.Setup(x => x.Query<BookingEntity>())
                .Returns(() => _bookingList.AsQueryable());

            _currentUserEntity = new UserEntity { Id = Guid.NewGuid() };
            _securityContextMock = new Mock<ISecurityContext>(MockBehavior.Strict);
            _securityContextMock.Setup(x => x.UserEntity).Returns(_currentUserEntity);
            _securityContextMock.Setup(x => x.IsAdministrator).Returns(false);
            _securityContextMock.Setup(x => x.IsStaff).Returns(false);

            _bookingsQueryProcessor = new BookingsQueryProcessor(_unitOfWorkMock.Object, _securityContextMock.Object);
        }

        [Fact]
        public void GetMustReturnAllBookings()
        {
            // arrange
            _bookingList.Add(new BookingEntity { UserEntityId = _currentUserEntity.Id});
            
            // act
            var result = _bookingsQueryProcessor.Get().ToList();
            
            // assert
            result.Count.Should().Be(1);
        }

        [Fact]
        public void GetShouldReturnOnlyUserBookings()
        {
            // arrange
            _bookingList.Add(new BookingEntity { UserEntityId = Guid.NewGuid() });
            _bookingList.Add(new BookingEntity { UserEntityId = _currentUserEntity.Id});

            // act
            var result = _bookingsQueryProcessor.Get().ToList();

            // assert
            result.Count.Should().Be(1);
            result[0].UserEntityId.Should().Be(_currentUserEntity.Id);
        }

        [Fact]
        public void GetShouldReturnAllBookingsForAdministrator()
        {
            // arrange
            _securityContextMock.Setup(x => x.IsAdministrator).Returns(true);

            _bookingList.Add(new BookingEntity { UserEntityId = Guid.NewGuid() });
            _bookingList.Add(new BookingEntity { UserEntityId = _currentUserEntity.Id });

            // act
            var result = _bookingsQueryProcessor.Get().ToList();

            // assert
            result.Count.Should().Be(2);
        }

        [Fact]
        public void GetShouldReturnAllBookingsExceptDeleted()
        {
            // arrange
            _bookingList.Add(new BookingEntity { UserEntityId = _currentUserEntity.Id });
            _bookingList.Add(new BookingEntity { UserEntityId = _currentUserEntity.Id, IsDeleted = true});

            // act 
            var result = _bookingsQueryProcessor.Get().ToList();

            // assert
            result.Count.Should().Be(1);
        }

        [Fact]
        public void GetShouldReturnById()
        {
            // arrange
            var booking = new BookingEntity { Id = Guid.NewGuid(), UserEntityId = _currentUserEntity.Id};
            _bookingList.Add(booking);

            // act
            var result = _bookingsQueryProcessor.Get(booking.Id);

            // assert
            result.Should().Be(booking);
        }

        [Fact]
        public void GetBookingShouldThrowNotFoundExceptionIfBookingOfOtherUser()
        {
            // arrange
            var booking = new BookingEntity { Id = Guid.NewGuid(), UserEntityId = Guid.NewGuid()};
            _bookingList.Add(booking);

            // act
            Action get = () => { _bookingsQueryProcessor.Get(booking.Id); };
            
            // assert
            get.Should().Throw<NotFoundException>();
        }

        [Fact]
        public void GetBookingShouldThrowNotFoundExceptionIfBookingNotFoundById()
        {
            // arrange
            var booking = new BookingEntity { Id = Guid.NewGuid(), UserEntityId = _currentUserEntity.Id};
            _bookingList.Add(booking);

            // act
            Action get = () => { _bookingsQueryProcessor.Get(Guid.NewGuid()); };

            // assert
            get.Should().Throw<NotFoundException>();
        }

        [Fact]
        public void GetBookingShouldThrowNotFoundExceptionIfBookingIsDeleted()
        {
            // arrange
            var booking = new BookingEntity { Id = Guid.NewGuid(), UserEntityId = _currentUserEntity.Id, IsDeleted = true};
            _bookingList.Add(booking);

            // act
            Action get = () => { _bookingsQueryProcessor.Get(booking.Id); };

            // assert
            get.Should().Throw<NotFoundException>();
        }

        [Fact]
        public async Task CreateShouldSaveNewBookingAsync()
        {
            // arrange
            var model = new CreateBookingModel
            {
                BookingStatusCode = "testBooking",
                SelfBooked = true,
                DateOfBooking = DateTime.UtcNow.ToLocalTime(),
                OtherBookingDetails = "testOtherBookingDetails"
            };

            // act
            var result = await _bookingsQueryProcessor.Create(model);

            // assert
            result.BookingStatusCode.Should().Be(model.BookingStatusCode);
            result.SelfBooked.Should().Be(model.SelfBooked);
            result.UserEntityId.Should().Be(_currentUserEntity.Id);

            _unitOfWorkMock.Verify(x => x.CommitAsync());
        }

        [Fact]
        public async Task UpdateBookingShouldUpdateFieldsAsync()
        {
            // arrange
            var booking = new BookingEntity { Id = Guid.NewGuid(), UserEntityId = _currentUserEntity.Id };
            _bookingList.Add(booking);

            var model = new UpdateBookingModel
            {
                BookingStatusCode = "testBookingUpdate",
                SelfBooked = true,
                DateOfBooking = DateTime.UtcNow.ToLocalTime(),
                OtherBookingDetails = "testOtherBookingDetails"
            };

            // act
            var result = await _bookingsQueryProcessor.Update(booking.Id, model);

            // assert
            result.Should().Be(booking);
            result.BookingStatusCode.Should().Be(model.BookingStatusCode);
            result.SelfBooked.Should().Be(model.SelfBooked);
            result.UserEntityId.Should().Be(_currentUserEntity.Id);

            _unitOfWorkMock.Verify(x => x.CommitAsync());
        }


        [Fact]
        public void UpdateBookingShouldThrowNotFoundExceptionIfBookingNotFound()
        {
            // Act
            Action get = () =>
            {
                var result = _bookingsQueryProcessor.Update(Guid.NewGuid(), new UpdateBookingModel()).Result;
            };

            // assert
            get.Should().Throw<NotFoundException>();
        }

        [Fact]
        public async Task DeleteShouldMarkBookingAsDeleted()
        {
            // arrange
            var booking = new BookingEntity { Id = Guid.NewGuid(), UserEntityId = _currentUserEntity.Id};
            _bookingList.Add(booking);

            // act
            await _bookingsQueryProcessor.Delete(booking.Id);

            // assert
            booking.IsDeleted.Should().BeTrue();
            _unitOfWorkMock.Verify(x => x.CommitAsync());
        }

        [Fact]
        public void DeleteShouldThrowNotFoundExceptionIfBookingIsNotBelongingToUser()
        {
            // arrange
            var booking = new BookingEntity { Id = Guid.NewGuid(), UserEntityId = Guid.NewGuid() };
            _bookingList.Add(booking);

            // act
            Action executeDeletion = () => { _bookingsQueryProcessor.Delete(booking.Id).Wait(); };

            // assert
            executeDeletion.Should().Throw<NotFoundException>();
        }

        [Fact]
        public void DeleteShouldThrowNotFoundExceptionIfBookingIsNotFound()
        {
            // act
            Action executeDeletion = () => { _bookingsQueryProcessor.Delete(Guid.NewGuid()).Wait(); };
            
            // assert
            executeDeletion.Should().Throw<NotFoundException>();
        }
    }
}
