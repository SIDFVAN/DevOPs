using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blanche.Domain.Reservations
{
    public enum ReservationState
    {
        NEW,
        OFFER_SENT,
        OFFER_CONFIRMED,
        INVOICE_SENT,
        DEPOSIT_PAYED,
        PAYMENT_COMPLETED, 
        FINISHED,
        CANCELLED
       
        
        
    }
}
