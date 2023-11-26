using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blanche.Domain.Reservations
{
    public enum ReservationState
    {
        OFFER, OFFER_CONFIRMED, DEPOSIT_PAYED, PAYMENT_COMPLETED, FINISHED
    }
}
