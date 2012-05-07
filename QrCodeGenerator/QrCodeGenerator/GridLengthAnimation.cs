using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Animation;
using System.Windows;

namespace QrCodeGenerator
{
    internal class GridLengthAnimation : AnimationTimeline
    {
        internal static readonly DependencyProperty FromProperty;
        internal static readonly DependencyProperty ToProperty;

        static GridLengthAnimation()
        {
            FromProperty = DependencyProperty.Register("From", typeof(GridLength), typeof(GridLengthAnimation));
            ToProperty = DependencyProperty.Register("To", typeof(GridLength), typeof(GridLengthAnimation));
        }

        protected override Freezable CreateInstanceCore()
        {
            return new GridLengthAnimation();
        }

        public override Type TargetPropertyType
        {
            get { return typeof(GridLength); }
        }

        internal GridLength From
        {
            get { return (GridLength)GetValue(FromProperty); }
            set { SetValue(FromProperty, value); }
        }

        internal GridLength To
        {
            get { return (GridLength)GetValue(ToProperty); }
            set { SetValue(ToProperty, value); }
        }

        public override object GetCurrentValue(object defaultOriginValue, object defaultDestinationValue, AnimationClock animationClock)
        {
            double fromValue = ((GridLength)GetValue(FromProperty)).Value;
            double toValue = ((GridLength)GetValue(ToProperty)).Value;

            if (fromValue > toValue)
            {
                return new GridLength((1 - animationClock.CurrentProgress.Value) * (fromValue - toValue) + toValue, this.To.IsStar ? GridUnitType.Star : GridUnitType.Pixel);
            }
            else
            {
                return new GridLength((animationClock.CurrentProgress.Value) * (toValue - fromValue) + fromValue, this.From.IsStar ? GridUnitType.Star : GridUnitType.Pixel);
            }
        }


    }
}
