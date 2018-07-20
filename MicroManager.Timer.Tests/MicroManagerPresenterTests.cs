using MicroManager.Timer.Core.Presenters;
using Xunit;

namespace MicroManager.Timer.Tests
{
    public class MicroManagerPresenterTests
    {
        [Fact]
        public void PresenterTests_Construction_MustNotThrowErrors()
        {
            var presenter = new MicroManagerPresenter(null, null);

        }
    }
}
