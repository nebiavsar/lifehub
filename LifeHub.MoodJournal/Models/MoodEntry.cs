using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LifeHub.MoodJournal.Models;

public class MoodEntry : INotifyPropertyChanged
{
    private string _mood = string.Empty;
    private string _note = string.Empty;
    private DateTime _timestamp;

    public string Mood
    {
        get => _mood;
        set
        {
            _mood = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(MoodEmoji));
            OnPropertyChanged(nameof(MoodColor));
        }
    }

    public string Note
    {
        get => _note;
        set
        {
            _note = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(HasNote));
        }
    }

    public DateTime Timestamp
    {
        get => _timestamp;
        set
        {
            _timestamp = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(TimestampText));
            OnPropertyChanged(nameof(RelativeTime));
        }
    }

    public string TimestampText => Timestamp.ToString("dd/MM/yyyy - HH:mm");

    public string RelativeTime
    {
        get
        {
            var diff = DateTime.Now - Timestamp;
            if (diff.TotalMinutes < 1) return "Just now";
            if (diff.TotalMinutes < 60) return $"{(int)diff.TotalMinutes}m ago";
            if (diff.TotalHours < 24) return $"{(int)diff.TotalHours}h ago";
            if (diff.TotalDays < 7) return $"{(int)diff.TotalDays}d ago";
            return TimestampText;
        }
    }

    public string MoodEmoji => Mood switch
    {
        "Amazing" => "😄",
        "Happy" => "😊",
        "Neutral" => "😐",
        "Sad" => "😢",
        "Stressed" => "😰",
        _ => "😐"
    };

    public string MoodColor => Mood switch
    {
        "Amazing" => "#00D9A3",
        "Happy" => "#00B386",
        "Neutral" => "#7FE7C4",
        "Sad" => "#4D4D4D",
        "Stressed" => "#FF6B6B",
        _ => "#7FE7C4"
    };

    public bool HasNote => !string.IsNullOrWhiteSpace(Note);

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
