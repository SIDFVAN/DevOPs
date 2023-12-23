using Blanche.Domain.Reservations;
using Blanche.Shared.Reservations;
using Riok.Mapperly.Abstractions;

namespace Blanche.Mappers.Reservations
{
    [Mapper]
    public static partial class PopularDateMapper
	{
		public static partial PopularDateDto PopularDateToPopularDateDto(PopularDate popularDate);

        public static partial PopularDate PopularDateDtoToPopularDate(PopularDateDto popularDateDto);
    }
}