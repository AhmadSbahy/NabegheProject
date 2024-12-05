using Nabeghe.Domain.DTOs.NovinoPay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nabeghe.Application.Services.Interfaces
{
    public interface INovinoService
    {
        Task<NovinoGetPaymentUrlResponseDto> CreateRequestAsync(NovinoGetPaymentUrlRequestDto model);

        Task<NovinoVerifyPaymentResponseDto> VerifyAsync(NovinoVerifyPaymentRequestDto model);
    }
}
