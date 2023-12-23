using Microsoft.AspNetCore.Components;

namespace Blanche.Client.Reservations.Components
{
	public partial class ReservationStepper
	{
        [Parameter]
        public bool Success { get; set; } = default!;
        [Parameter]
        public bool Final { get; set; } = default!;
    }
}
