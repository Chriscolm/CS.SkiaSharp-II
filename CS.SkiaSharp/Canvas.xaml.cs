using CS.SkiaSharpExample.Eventbus.Contracts.Events;
using CS.SkiaSharpExample.ViewModels;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace CS.SkiaSharpExample
{
    /// <summary>
    /// Interaktionslogik für Canvas.xaml
    /// </summary>
    public partial class Canvas : UserControl
    {
        private CanvasViewModel _dataContext;

        public Canvas()
        {
            InitializeComponent();
        }

        private void OnUserControlLoaded(object sender, RoutedEventArgs e)
        {
            var bootstrapper = TryFindResource(nameof(Bootstrapper));
            if (bootstrapper is Bootstrapper bs)
            {
                _dataContext = bs.Resolve<CanvasViewModel>();
                _dataContext.MessageBroker.MessageSending += OnMessage;
                DataContext = _dataContext;
            }
            if (!DesignerProperties.GetIsInDesignMode(this) && _dataContext == null)
            {
                throw new NullReferenceException("Datacontext not set");
            }
        }

        private void OnMessage(object sender, MessageSendingEventArgs e)
        {
            if (e.Args is RenderingRequestedEventArgs r)
            {
                OnRenderingRequested(sender, r);
            }
        }

        private void OnRenderingRequested(object sender, RenderingRequestedEventArgs e)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(() => Invalidate());
                return;
            }
            Invalidate();
        }

        private void Invalidate()
        {
            _canvas.InvalidateVisual();
        }

        private void OnPaintSurface(object sender, SkiaSharp.Views.Desktop.SKPaintSurfaceEventArgs e)
        {
            if (_dataContext != null)
            {
                _dataContext.CanvasHeight = ActualHeight;
                _dataContext.CanvasWidth = ActualWidth;

                _dataContext.Render(e.Surface, e.Info.Width, e.Info.Height);
            }
        }
    }
}
