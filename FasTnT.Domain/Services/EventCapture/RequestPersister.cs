using FasTnT.Domain.Model.Events;
using FasTnT.Domain.Repositories;
using System;

namespace FasTnT.Domain.Services.EventCapture
{
    public class RequestPersister : IRequestPersister
    {
        private readonly IEpcisRequestRepository _requestRepository;

        public RequestPersister(IEpcisRequestRepository requestRepository)
        {
            _requestRepository = requestRepository ?? throw new ArgumentException(nameof(requestRepository));
        }

        public virtual void Persist(EpcisRequest request)
        {
            _requestRepository.Insert(request);
        }
    }
}