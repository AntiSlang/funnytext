using System;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using NAudio.Wave;

public static class AudioPlayer
{
    private static WaveOutEvent? _waveOut;
    private static Mp3FileReader? _mp3Reader;
    private static bool _isLooping = false;
    private static string? _currentFilePath;
    private static bool _isPlaying = false;

    public static async Task PlayMp3Async(string filePath, bool loop = false)
    {
        Console.WriteLine("test");
        if (_isPlaying) return;

        _isPlaying = true;
        _currentFilePath = filePath;
        _isLooping = loop;

            do
            {
                _mp3Reader = new Mp3FileReader(AssetLoader.Open(new Uri(filePath)));
                _waveOut = new WaveOutEvent();
                _waveOut.Init(_mp3Reader);
                _waveOut.Play();
                _waveOut.PlaybackStopped += OnPlaybackStopped;
                while (_waveOut.PlaybackState == PlaybackState.Playing)
                {
                    await Task.Delay(100);
                }
                _waveOut.Dispose();
                _mp3Reader.Dispose();
            } while (_isLooping && _isPlaying);
        
    }

    private static void OnPlaybackStopped(object? sender, StoppedEventArgs e)
    {
        if (_isLooping && _isPlaying && _currentFilePath != null)
        {
            PlayMp3Async(_currentFilePath, _isLooping).Wait();
        }
        else
        {
            Stop();
        }
    }

    public static void Stop()
    {
        try
        {
            _isLooping = false;
            _isPlaying = false;

            _waveOut?.Stop();
            _waveOut?.Dispose();
            _mp3Reader?.Dispose();

            _waveOut = null;
            _mp3Reader = null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при остановке: {ex.Message}");
        }
    }

    public static void SetVolume(float volume)
    {
        if (_waveOut != null)
        {
            _waveOut.Volume = Math.Clamp(volume, 0.0f, 1.0f);
        }
    }

    public static void Mute()
    {
        SetVolume(0.0f);
    }

    public static void Unmute(float volume = 0.5f)
    {
        SetVolume(volume);
    }
}