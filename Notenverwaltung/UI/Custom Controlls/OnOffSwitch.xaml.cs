using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace _16_CustomControls
{
  public delegate void SwitchEvent(OnOffSwitch sender);
  
  
  public partial class OnOffSwitch : UserControl
  {
    public event SwitchEvent SwitchEvent;

    private bool _on;
    public bool On
    {
      get
      {
        return _on;
      }
      set
      {
        _on = value;
        SwitchEvent?.Invoke(this);
      }
    }


    public OnOffSwitch() => InitializeComponent();


    private void elliInside_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      if (!On)
      {
        this.lblOff.Visibility = Visibility.Hidden;


        var myDoubleAnimation = new DoubleAnimation();
        myDoubleAnimation.From = 0.0;
        myDoubleAnimation.To = this.yeet.ActualWidth - this.elliInside.ActualWidth;
        myDoubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.1));
        elliInside.BeginAnimation(Canvas.LeftProperty, myDoubleAnimation);


        Canvas.SetTop(this.elliInside, -1);
        Canvas.SetLeft(this.elliInside, this.yeet.ActualWidth - this.elliInside.ActualWidth + 1);


        this.lblOn.Visibility = Visibility.Visible;
        On = true;
      }


      else if (On)
      {
        this.lblOn.Visibility = Visibility.Hidden;


        var myDoubleAnimation = new DoubleAnimation();
        myDoubleAnimation.From = this.yeet.ActualWidth - this.elliInside.ActualWidth;
        myDoubleAnimation.To = 0.0;
        myDoubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.1));
        elliInside.BeginAnimation(Canvas.LeftProperty, myDoubleAnimation);


        Canvas.SetLeft(this.elliInside, -1);
        Canvas.SetTop(this.elliInside, -1);


        this.lblOff.Visibility = Visibility.Visible;
        On = false;
      }
    }





    private void elliInside_MouseLeftButtonDown1(object sender, MouseButtonEventArgs e)
    {
      var myDoubleAnimation = new DoubleAnimation();
      myDoubleAnimation.From = On ? this.yeet.ActualWidth - this.elliInside.ActualWidth : 0.0;
      myDoubleAnimation.To = On ? 0.0 : this.yeet.ActualWidth - this.elliInside.ActualWidth;
      myDoubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.1));
      elliInside.BeginAnimation(Canvas.LeftProperty, myDoubleAnimation);


      Canvas.SetTop(this.elliInside, -1);
      Canvas.SetLeft(this.elliInside, On ? -1 : this.yeet.ActualWidth - this.elliInside.ActualWidth + 1);


      this.lblOff.Visibility = On ? Visibility.Visible : Visibility.Hidden;
      this.lblOn.Visibility = On ? Visibility.Hidden : Visibility.Visible;
      On = !On;
    }





    private void LadenSwitch(object sender, RoutedEventArgs e)
    {
      this.lblOff.Visibility = On ? Visibility.Hidden : Visibility.Visible;
      this.lblOn.Visibility = On ? Visibility.Visible : Visibility.Hidden;


      Canvas.SetTop(this.elliInside, -1);
      Canvas.SetLeft(
        this.elliInside,
        On ? this.yeet.ActualWidth - this.elliInside.ActualWidth + 1 : -1
      );


      this.elliInside.MouseLeftButtonDown += elliInside_MouseLeftButtonDown1;
      this.lblOff.MouseLeftButtonDown += elliInside_MouseLeftButtonDown1;
      this.lblOn.MouseLeftButtonDown += elliInside_MouseLeftButtonDown1;


      SwitchEvent?.Invoke(this);
    }
  }
}
