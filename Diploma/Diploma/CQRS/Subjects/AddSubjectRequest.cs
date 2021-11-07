using Diploma.Views;
using MediatR;

namespace Diploma.CQRS.Subjects
{
    public class AddSubjectRequest: IRequest<ResultView>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Course{ get; set; }
    }
}
