using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerServiceRESTAPI.Entities;

namespace CustomerServiceRESTAPI.Services
{
    public interface ISalesService
    {
        Task<bool> RequestReplacementDevice(Ticket ticketForRefund);
    }
}
