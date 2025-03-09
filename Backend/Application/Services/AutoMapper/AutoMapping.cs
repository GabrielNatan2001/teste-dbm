using AutoMapper;
using Communication.Requests;
using Communication.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping() 
        {
            RequestToDomain();
            DomainToResponse();
        }
        private void RequestToDomain()
        {
            CreateMap<RequestProdutoJson, Domain.Entities.Produto>();
        }

        private void DomainToResponse()
        {
            CreateMap<Domain.Entities.Produto, ResponseProdutoJson>();
        }
    }
}
