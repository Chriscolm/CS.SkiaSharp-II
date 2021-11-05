using CS.SkiaSharpExample.ViewModels;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace CS.SkiaSharpExample
{
    /// <summary>
    /// Interaktionslogik für Controls.xaml
    /// </summary>
    public partial class Controls : UserControl
    {
        private ControlsViewModel _dataContext;

        public Controls()
        {
            InitializeComponent();
        }

        private void OnUserControlLoaded(object sender, RoutedEventArgs e)
        {
            var bootstrapper = TryFindResource(nameof(Bootstrapper));
            if (bootstrapper is Bootstrapper bs)
            {
                _dataContext = bs.Resolve<ControlsViewModel>();

                DataContext = _dataContext;
            }
            if (!DesignerProperties.GetIsInDesignMode(this) && _dataContext == null)
            {
                throw new NullReferenceException("Datacontext not set");
            }
        }
    }
}
