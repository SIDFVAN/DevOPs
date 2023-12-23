using System;
using Blanche.Domain.Exceptions;
using Blanche.Mappers.Reservations;
using Blanche.Server.Persistence;
using Blanche.Shared.Exceptions;
using Blanche.Shared.Reservations;
using EntityNotFoundException = Blanche.Domain.Exceptions.EntityNotFoundException;

namespace Blanche.Server.Services.Reservations
{
	public class ReservationItemService : IReservationItemService
    {
        protected readonly IUnitOfWork _unitOfWork;

        public ReservationItemService(IUnitOfWork unitOfWork)
		{
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> CreateAsync(ReservationItemDto reservationItemDto)
        {
            var reservationItem = ReservationItemMapper.ReservationItemDtoToReservationItem(reservationItemDto);

            var duplicateReservationItem = _unitOfWork.ReservationItems
                .Queryable()
                .SingleOrDefault(r => r.Id == reservationItem.Id);

            if (duplicateReservationItem != null)
            {
                throw new DuplicateReservationException();
            }

            _unitOfWork.ReservationItems.Add(reservationItem);
            await _unitOfWork.CommitAsync();

            return reservationItem.Id;
        }

        public async Task<Guid> DeleteAsync(Guid reservationItemId)
        {
            var reservationItemToDelete = _unitOfWork.ReservationItems
                .Queryable()
                .SingleOrDefault(r => r.Id == reservationItemId);

            if (reservationItemToDelete is null)
            {
                throw new EntityNotFoundException(nameof(ReservationItemDto), reservationItemId);
            }

            _unitOfWork.ReservationItems.Remove(reservationItemToDelete);
            await _unitOfWork.CommitAsync();

            return reservationItemId;
        }
    }
}

