using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diploma.Views;
using MediatR;

namespace Diploma.CQRS.Subjects
{
    public class GetSubjectsRequest:  IRequest<List<SubjectView>>
    {
        public int? SubjectId { get; set; }
        public string Filter { get; set; }
    }
}
