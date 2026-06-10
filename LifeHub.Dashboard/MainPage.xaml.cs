using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using LifeHub.Dashboard.Commands;

namespace LifeHub.Dashboard;

public partial class MainPage : ContentPage, INotifyPropertyChanged
{
	private string _welcomeMessage = string.Empty;
	private string _totalApps = string.Empty;
	private string _featuresCount = string.Empty;
	private string _purpose = string.Empty;
	private string _currentDateTime = string.Empty;

	public MainPage()
	{
		InitializeComponent();
		InitializeData();
		InitializeCommands();
		BindingContext = this;
		
		// Update time every second
		Dispatcher.StartTimer(TimeSpan.FromSeconds(1), () =>
		{
			CurrentDateTime = DateTime.Now.ToString("dddd, MMMM dd, yyyy - HH:mm:ss");
			return true;
		});
	}

	private void InitializeCommands()
	{
		OpenHabitTrackerCommand = new RelayCommand(_ => OpenHabitTracker());
		OpenMoodJournalCommand = new RelayCommand(_ => OpenMoodJournal());
		OpenPlannerCommand = new RelayCommand(_ => OpenPlanner());
	}

	private async void OpenHabitTracker()
	{
		await DisplayAlert("HabitTracker", "Bu özellik yakında gelecek!\n\nHabit tracking functionality will be available soon.", "Tamam");
	}

	private async void OpenMoodJournal()
	{
		await DisplayAlert("MoodJournal", "Bu özellik yakında gelecek!\n\nMood journaling functionality will be available soon.", "Tamam");
	}

	private async void OpenPlanner()
	{
		await DisplayAlert("Planner", "Bu özellik yakında gelecek!\n\nTask planning functionality will be available soon.", "Tamam");
	}

	private void InitializeData()
	{
		WelcomeMessage = "Manage your life with elegance and simplicity.\nTrack habits, plan your day, and reflect on your journey.";
		TotalApps = "3";
		FeaturesCount = "9+";
		Purpose = "You";
		CurrentDateTime = DateTime.Now.ToString("dddd, MMMM dd, yyyy - HH:mm:ss");
	}

	public string WelcomeMessage
	{
		get => _welcomeMessage;
		set
		{
			_welcomeMessage = value;
			OnPropertyChanged();
		}
	}

	public string TotalApps
	{
		get => _totalApps;
		set
		{
			_totalApps = value;
			OnPropertyChanged();
		}
	}

	public string FeaturesCount
	{
		get => _featuresCount;
		set
		{
			_featuresCount = value;
			OnPropertyChanged();
		}
	}

	public string Purpose
	{
		get => _purpose;
		set
		{
			_purpose = value;
			OnPropertyChanged();
		}
	}

	public string CurrentDateTime
	{
		get => _currentDateTime;
		set
		{
			_currentDateTime = value;
			OnPropertyChanged();
		}
	}

	public ICommand OpenHabitTrackerCommand { get; private set; } = null!;
	public ICommand OpenMoodJournalCommand { get; private set; } = null!;
	public ICommand OpenPlannerCommand { get; private set; } = null!;

	public new event PropertyChangedEventHandler? PropertyChanged;

	protected new void OnPropertyChanged([CallerMemberName] string? propertyName = null)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
