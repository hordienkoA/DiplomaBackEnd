using Diploma.Views;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.CQRS.Atempt
{
    public class SubmitAnswerRequest:IRequest<ResultView>
    {
        public int TaskInfoId { get; set; }
        public string Answer { get; set; }
    }
}
