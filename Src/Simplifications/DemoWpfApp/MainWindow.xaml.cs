using PlayerWpfLib;
using System.Windows;

namespace DemoWpfApp
{
  public partial class MainWindow : Window
  {
    const int freqHz1 = 440, freqHz2 = 392, freqHz3 = 32000, durationMks = 256000;

    public MainWindow() => InitializeComponent();

    void button1_Click(object sender, RoutedEventArgs e)
    {
      var freqDurnArray = new[]
      {
        new[] { freqHz1, durationMks }
      };

      FrequencyDurationArrayPlayer.BeepMks(freqDurnArray);
    }
    void button2_Click(object sender, RoutedEventArgs e)
    {
      var freqDurnArray = new[]
      {
        new[] { freqHz1, durationMks },
        new[] { freqHz2, durationMks }
      };

      FrequencyDurationArrayPlayer.BeepMks(freqDurnArray);
    }
    void button3_Click(object sender, RoutedEventArgs e)
    {
      var freqDurnArray = new[]
      {
        new[] { freqHz1, durationMks },
        new[] { freqHz2, durationMks },
        new[] { freqHz3, durationMks },
        new[] { freqHz1, durationMks },
        new[] { freqHz2, durationMks },
        new[] { freqHz3, durationMks },
        new[] { freqHz1, durationMks },
        new[] { freqHz2, durationMks },
        new[] { freqHz2, durationMks }
      };

      FrequencyDurationArrayPlayer.BeepMks(freqDurnArray);
    }
    void button4_Click(object sender, RoutedEventArgs e)
    {
      var freqDurnArray = new[]
      {
        new[] { 1000, 1000 }, // this is going to produce a single wave, something like this: ~ 
        new[] { 2000, 500 },  // this too, but 2 times shorter
        new[] { 3333, 300 }   // even shorter
      };

      FrequencyDurationArrayPlayer.BeepMks(freqDurnArray);
    }

  }
}
