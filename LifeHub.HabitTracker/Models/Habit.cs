using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LifeHub.HabitTracker.Models;

public class Habit : INotifyPropertyChanged
{
    private string _name = string.Empty;
    private bool _isCompleted;
    private DateTime _createdDate;
    private DateTime? _completedDate;

    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged();
        }
    }

    public bool IsCompleted
    {
        get => _isCompleted;
        set
        {
            _isCompleted = value;
            if (value)
            {
                CompletedDate = DateTime.Now;
            }
            else
            {
                CompletedDate = null;
            }
            OnPropertyChanged();
            OnPropertyChanged(nameof(StatusText));
            OnPropertyChanged(nameof(StatusColor));
        }
    }

    public DateTime CreatedDate
    {
        get => _createdDate;
        set
        {
            _createdDate = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(CreatedDateText));
        }
    }

    public DateTime? CompletedDate
    {
        get => _completedDate;
        set
        {
            _completedDate = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(CompletedDateText));
        }
    }

    public string CreatedDateText => $"Created: {CreatedDate:dd/MM/yyyy HH:mm}";
    
    public string CompletedDateText => CompletedDate.HasValue 
        ? $"Completed: {CompletedDate.Value:dd/MM/yyyy HH:mm}" 
        : "Not completed yet";

    public string StatusText => IsCompleted ? "✓ Done" : "○ Pending";
    
    public string StatusColor => IsCompleted ? "#00D9A3" : "#666666";

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
