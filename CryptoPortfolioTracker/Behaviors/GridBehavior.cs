using Microsoft.Maui.Controls;

namespace CryptoPortfolioTracker.Behaviors
{
    public class GridAnimationBehavior : Behavior<Grid>
    {
        public static readonly BindableProperty TargetProperty = BindableProperty.Create(nameof(Target), typeof(VisualElement), typeof(GridAnimationBehavior), null,
        propertyChanged: (bindable, oldValue, newValue) => ((GridAnimationBehavior)bindable).Target = (VisualElement)newValue);

        public VisualElement Target
        {
            get => (VisualElement)GetValue(TargetProperty);
            set => SetValue(TargetProperty, value);
        }

        private double OriginalTranslationY { get; set; }

        protected override void OnAttachedTo(Grid grid)
        {
            base.OnAttachedTo(grid);
            Target = grid;
            OriginalTranslationY = Target.TranslationY;
            grid.PropertyChanged += OnGridPropertyChanged;
        }

        protected override void OnDetachingFrom(Grid grid)
        {
            base.OnDetachingFrom(grid);
            grid.PropertyChanged -= OnGridPropertyChanged;
        }

        private void OnGridPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Grid.IsVisible))
            {
                AnimateVisibilityChange();
            }
        }

        private void AnimateVisibilityChange()
        {
            if (Target.IsVisible)
            {
                var fadeInAnimation = FadeIn(OriginalTranslationY);
                // Subscribe to the PropertyChanged event of the target element
                Target.PropertyChanged += (sender, e) =>
                {
                    // Check if the IsVisible property changed while the animation is still running
                    if (e.PropertyName == nameof(Target.IsVisible) && !fadeInAnimation.HasFinished && !Target.IsVisible)
                    {
                        Target.TranslationY = OriginalTranslationY;
                    }
                };
                Target.Dispatcher.Dispatch(() => Target.Animate("FadeIn", fadeInAnimation, 16, 800));
            }
        }

        internal Animation FadeIn(double initialTranslationY)
        {
            var animation = new Animation();

            animation.WithConcurrent((f) => Target.Opacity = f, 0, 1, Microsoft.Maui.Easing.CubicOut);

            animation.WithConcurrent(
              (f) => Target.TranslationY = f,
              initialTranslationY - 50, initialTranslationY,
              Microsoft.Maui.Easing.CubicOut, 0, 1);
            // Subscribe to the Completed event of the animation
            animation.Finished = () =>
            {
                // Reset the Y translation property to its original position
                Target.TranslationY = initialTranslationY;
            };
            return animation;
        }
    }
}