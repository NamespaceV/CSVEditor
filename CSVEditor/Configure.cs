using CSVEditor.ViewModel;
using Ninject;
using System;

namespace CSVEditor
{
    static class Configure
    {
        static private Lazy<IKernel> _iocKernel = new Lazy<IKernel>(CreateKernel);

        static public IKernel NinjectKernel => _iocKernel.Value;

        static private IKernel CreateKernel()
        {
            var k = new StandardKernel();
            k.Bind<MainWindowViewModel>().ToSelf();
            k.Bind<MainWindow>().ToSelf();
            return k;
        }
    }
}
