using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CustomerServiceRESTAPI.Entities;
using CustomerServiceRESTAPI.Services;

namespace CustomerServiceRESTAPI.Tests.Mocks
{
    class SalesServiceMock : ISalesService
    {
        public Task<bool> RequestReplacementDevice(Ticket ticketForRefund)
        {
            return Task.Run(async () => true);
        }
    }
}
