using Diploma.Views;
using MediatR;

namespace Diploma.CQRS.Subjects
{
    public class EditSubjectRequest: IRequest<ResultView>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Course { get; set; }
    }
}
