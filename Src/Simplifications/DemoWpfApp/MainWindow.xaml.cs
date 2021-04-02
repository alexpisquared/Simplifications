using PlayerWpfLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DemoWpfApp
{
  public partial class MainWindow : Window
  {
    const int freqHz1 = 440, freqHz2 = 392, freqHz3 = 32000, durationMks = 256000;

    public MainWindow()
    {
      InitializeComponent();
    }

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
  }
}
