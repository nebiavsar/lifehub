using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using LifeHub.HabitTracker.Models;
using LifeHub.HabitTracker.Commands;

namespace LifeHub.HabitTracker;

public partial class MainPage : ContentPage, INotifyPropertyChanged
{
	private ObservableCollection<Habit> _habits;
	private string _newHabitName = string.Empty;
	private string _totalHabits = "0";
	private string _completedHabits = "0";

	public MainPage()
	{
		InitializeComponent();
		
		_habits = new ObservableCollection<Habit>();
		_habits.CollectionChanged += (s, e) => UpdateStats();
		
		AddHabitCommand = new RelayCommand(
			execute: _ => AddHabit(),
			canExecute: _ => !string.IsNullOrWhiteSpace(NewHabitName)
		);
		
		BindingContext = this;
		LoadSampleData();
	}

	private void LoadSampleData()
	{
		// Add some sample habits
		Habits.Add(new Habit 
		{ 
			Name = "Morning Meditation", 
			CreatedDate = DateTime.Now.AddDays(-2),
			IsCompleted = true
		});
		
		Habits.Add(new Habit 
		{ 
			Name = "Read for 30 minutes", 
			CreatedDate = DateTime.Now.AddDays(-1),
			IsCompleted = false
		});
	}

	public ObservableCollection<Habit> Habits
	{
		get => _habits;
		set
		{
			_habits = value;
			OnPropertyChanged();
			UpdateStats();
		}
	}

	public string NewHabitName
	{
		get => _newHabitName;
		set
		{
			_newHabitName = value;
			OnPropertyChanged();
			((RelayCommand)AddHabitCommand).RaiseCanExecuteChanged();
		}
	}

	public string TotalHabits
	{
		get => _totalHabits;
		set
		{
			_totalHabits = value;
			OnPropertyChanged();
		}
	}

	public string CompletedHabits
	{
		get => _completedHabits;
		set
		{
			_completedHabits = value;
			OnPropertyChanged();
		}
	}

	public ICommand AddHabitCommand { get; }

	private void AddHabit()
	{
		if (!string.IsNullOrWhiteSpace(NewHabitName))
		{
			var habit = new Habit
			{
				Name = NewHabitName.Trim(),
				CreatedDate = DateTime.Now,
				IsCompleted = false
			};

			// Subscribe to property changes to update stats when checkbox changes
			habit.PropertyChanged += (s, e) =>
			{
				if (e.PropertyName == nameof(Habit.IsCompleted))
				{
					UpdateStats();
				}
			};

			Habits.Add(habit);
			NewHabitName = string.Empty;
			HabitEntry.Focus();
		}
	}

	private void OnAddHabitClicked(object? sender, EventArgs e)
	{
		AddHabit();
	}

	private void UpdateStats()
	{
		TotalHabits = Habits.Count.ToString();
		CompletedHabits = Habits.Count(h => h.IsCompleted).ToString();
	}

	public new event PropertyChangedEventHandler? PropertyChanged;

	protected new void OnPropertyChanged([CallerMemberName] string? propertyName = null)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
