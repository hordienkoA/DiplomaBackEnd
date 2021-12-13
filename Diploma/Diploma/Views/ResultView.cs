using System.Collections.Generic;

namespace Diploma.Views
{
    public class ResultView
    {
        public List<IView> Views { get; set; } = null;
        public ErrorState Error { get; set; } = null;
    }
}