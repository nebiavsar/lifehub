using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using LifeHub.Planner.Models;
using LifeHub.Planner.Commands;

namespace LifeHub.Planner;

public partial class MainPage : ContentPage, INotifyPropertyChanged
{
	private ObservableCollection<PlannerTask> _tasks;
	private string _newTaskTitle = string.Empty;
	private string _newTaskDescription = string.Empty;
	private string _newTaskPriority = "Normal";
	private string _totalTasks = "0";
	private string _completedTasks = "0";
	private string _pendingTasks = "0";

	public MainPage()
	{
		InitializeComponent();
		
		_tasks = new ObservableCollection<PlannerTask>();
		_tasks.CollectionChanged += (s, e) => UpdateStats();
		
		AddTaskCommand = new RelayCommand(
			execute: _ => AddTask(),
			canExecute: _ => !string.IsNullOrWhiteSpace(NewTaskTitle)
		);
		
		BindingContext = this;
		LoadSampleData();
	}

	private void LoadSampleData()
	{
		// Add some sample tasks
		var task1 = new PlannerTask 
		{ 
			Title = "Morning Team Meeting",
			Description = "Discuss project milestones and weekly goals",
			Priority = "High",
			CreatedDate = DateTime.Now.AddHours(-1),
			IsCompleted = false
		};
		task1.PropertyChanged += Task_PropertyChanged;
		Tasks.Add(task1);
		
		var task2 = new PlannerTask 
		{ 
			Title = "Complete Project Documentation",
			Description = "Finalize the user manual and API docs",
			Priority = "Normal",
			CreatedDate = DateTime.Now.AddHours(-3),
			IsCompleted = true
		};
		task2.PropertyChanged += Task_PropertyChanged;
		Tasks.Add(task2);
		
		var task3 = new PlannerTask 
		{ 
			Title = "Review Code Changes",
			Priority = "Low",
			CreatedDate = DateTime.Now.AddDays(-1),
			IsCompleted = false
		};
		task3.PropertyChanged += Task_PropertyChanged;
		Tasks.Add(task3);
	}

	private void Task_PropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		if (e.PropertyName == nameof(PlannerTask.IsCompleted))
		{
			UpdateStats();
		}
	}

	public ObservableCollection<PlannerTask> Tasks
	{
		get => _tasks;
		set
		{
			_tasks = value;
			OnPropertyChanged();
			UpdateStats();
		}
	}

	public string NewTaskTitle
	{
		get => _newTaskTitle;
		set
		{
			_newTaskTitle = value;
			OnPropertyChanged();
			((RelayCommand)AddTaskCommand).RaiseCanExecuteChanged();
		}
	}

	public string NewTaskDescription
	{
		get => _newTaskDescription;
		set
		{
			_newTaskDescription = value;
			OnPropertyChanged();
		}
	}

	public string NewTaskPriority
	{
		get => _newTaskPriority;
		set
		{
			_newTaskPriority = value;
			OnPropertyChanged();
		}
	}

	public string TotalTasks
	{
		get => _totalTasks;
		set
		{
			_totalTasks = value;
			OnPropertyChanged();
		}
	}

	public string CompletedTasks
	{
		get => _completedTasks;
		set
		{
			_completedTasks = value;
			OnPropertyChanged();
		}
	}

	public string PendingTasks
	{
		get => _pendingTasks;
		set
		{
			_pendingTasks = value;
			OnPropertyChanged();
		}
	}

	public ICommand AddTaskCommand { get; }

	private void AddTask()
	{
		if (!string.IsNullOrWhiteSpace(NewTaskTitle))
		{
			var task = new PlannerTask
			{
				Title = NewTaskTitle.Trim(),
				Description = NewTaskDescription?.Trim() ?? string.Empty,
				Priority = string.IsNullOrWhiteSpace(NewTaskPriority) ? "Normal" : NewTaskPriority,
				CreatedDate = DateTime.Now,
				IsCompleted = false
			};

			// Subscribe to property changes
			task.PropertyChanged += Task_PropertyChanged;

			Tasks.Add(task);
			NewTaskTitle = string.Empty;
			NewTaskDescription = string.Empty;
			NewTaskPriority = "Normal";
			TaskEntry.Focus();
		}
	}

	private void OnAddTaskClicked(object? sender, EventArgs e)
	{
		AddTask();
	}

	private void UpdateStats()
	{
		TotalTasks = Tasks.Count.ToString();
		CompletedTasks = Tasks.Count(t => t.IsCompleted).ToString();
		PendingTasks = Tasks.Count(t => !t.IsCompleted).ToString();
	}

	public new event PropertyChangedEventHandler? PropertyChanged;

	protected new void OnPropertyChanged([CallerMemberName] string? propertyName = null)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
