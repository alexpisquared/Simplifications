using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IO;
using System.Linq;
using System.Media;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerWpfLib
{
  public static class FrequencyDurationArrayPlayer
  {
    static readonly SoundPlayer _p = new SoundPlayer();

    public static void BeepMks(int[][] HzMks, ushort volume = ushort.MaxValue)
    {
      using (var mStrm = new MemoryStream())
      using (var writer = new BinaryWriter(mStrm))
      {
        const double TAU = 2 * Math.PI;
        const int formatChunkSize = 16, headerSize = 8, samplesPerSecond = 44100, waveSize = 4;
        const short formatType = 1, tracks = 1, bitsPerSample = 16, frameSize = tracks * ((bitsPerSample + 7) / 8);
        const int bytesPerSecond = samplesPerSecond * frameSize;

        var ttlms = HzMks.Sum(r => r[1]);
        var samples = (int)(samplesPerSecond * 0.000001m * ttlms);
        var dataChunkSize = samples * frameSize;
        var fileSize = waveSize + headerSize + formatChunkSize + headerSize + dataChunkSize;
        // var encoding = new System.Text.UTF8Encoding();
        writer.Write(0x46464952); // = encoding.GetBytes("RIFF")
        writer.Write(fileSize);
        writer.Write(0x45564157); // = encoding.GetBytes("WAVE")
        writer.Write(0x20746D66); // = encoding.GetBytes("fmt ")
        writer.Write(formatChunkSize);
        writer.Write(formatType);
        writer.Write(tracks);
        writer.Write(samplesPerSecond);
        writer.Write(bytesPerSecond);
        writer.Write(frameSize);
        writer.Write(bitsPerSample);
        writer.Write(0x61746164); // = encoding.GetBytes("data")
        writer.Write(dataChunkSize);

        foreach (var hzms in HzMks)
        {
          var hz = hzms[0];
          var ms = (long)hzms[1];
          var theta = hz * TAU / samplesPerSecond;

          //Console.WriteLine($" ** Beep():  {hz,8:N0} hz   {ms,8:N0} ms     =>     2Pi *{hz,5} hz  /  {samplesPerSecond} sampl/s  =  {theta:N4}");

          // 'volume' is UInt16 with range 0 thru Uint16.MaxValue ( = 65 535)
          // we need 'amp' to have the range of 0 thru Int16.MaxValue ( = 32 767)
          double amp = volume >> 2; // so we simply set amp = volume / 2
          var stepCount = (int)(samples * ms / ttlms);
          for (var step = 0; step < stepCount; step++)
            //System.Diagnostics.Debug.WriteLine((short)(amp * Math.Sin(theta * step)));
            writer.Write((short)(amp * Math.Sin(theta * step)));
        }

        mStrm.Seek(0, SeekOrigin.Begin);
        _p.Stream = mStrm;
        try
        {
#if sync
          _p.Play();          
          await Task.Delay(ttlms / 1000);
#else
          _p.PlaySync();
#endif

          writer.Close();
          mStrm.Close();
        }
        catch (Exception) { throw; }
      }
    }

    public static int FixDuration(int frequencyHz, int durationMks) // removing spikes on stitches by cutting off non-whole parts of waves.
    {
      var times = durationMks * .000001 * frequencyHz;
      var timesm = Math.Round(times);
      if (timesm <= 0)
        timesm = 1;

      return (int)(/*1.03125 * */1000000 * timesm / frequencyHz);
    }
  }
}
