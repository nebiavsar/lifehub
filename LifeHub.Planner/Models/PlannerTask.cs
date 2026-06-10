using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LifeHub.Planner.Models;

public class PlannerTask : INotifyPropertyChanged
{
    private string _title = string.Empty;
    private string _description = string.Empty;
    private bool _isCompleted;
    private DateTime _createdDate;
    private DateTime? _completedDate;
    private string _priority = "Normal";

    public string Title
    {
        get => _title;
        set
        {
            _title = value;
            OnPropertyChanged();
        }
    }

    public string Description
    {
        get => _description;
        set
        {
            _description = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(HasDescription));
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
            OnPropertyChanged(nameof(TitleColor));
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
        }
    }

    public string Priority
    {
        get => _priority;
        set
        {
            _priority = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(PriorityColor));
            OnPropertyChanged(nameof(PriorityIcon));
        }
    }

    public string CreatedDateText => CreatedDate.ToString("dd/MM/yyyy HH:mm");

    public string StatusText => IsCompleted ? "✓ Completed" : "○ To Do";
    
    public string StatusColor => IsCompleted ? "#00D9A3" : "#009973";

    public string TitleColor => IsCompleted ? "#666666" : "#FFFFFF";

    public string PriorityColor => Priority switch
    {
        "High" => "#FF6B6B",
        "Normal" => "#00D9A3",
        "Low" => "#7FE7C4",
        _ => "#00D9A3"
    };

    public string PriorityIcon => Priority switch
    {
        "High" => "🔴",
        "Normal" => "🟢",
        "Low" => "🔵",
        _ => "🟢"
    };

    public bool HasDescription => !string.IsNullOrWhiteSpace(Description);

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
