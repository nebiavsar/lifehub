using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using LifeHub.MoodJournal.Models;
using LifeHub.MoodJournal.Commands;

namespace LifeHub.MoodJournal;

public partial class MainPage : ContentPage, INotifyPropertyChanged
{
	private ObservableCollection<MoodEntry> _moodEntries;
	private string _selectedMood = string.Empty;
	private string _newNote = string.Empty;
	private string _totalEntries = "0";
	private string _recentMood = "-";

	public MainPage()
	{
		InitializeComponent();
		
		_moodEntries = new ObservableCollection<MoodEntry>();
		_moodEntries.CollectionChanged += (s, e) => UpdateStats();
		
		SaveEntryCommand = new RelayCommand(
			execute: _ => SaveEntry(),
			canExecute: _ => !string.IsNullOrWhiteSpace(SelectedMood)
		);
		
		BindingContext = this;
		LoadSampleData();
	}

	private void LoadSampleData()
	{
		// Add some sample entries
		MoodEntries.Add(new MoodEntry 
		{ 
			Mood = "Happy",
			Note = "Had a great morning workout and productive day at work!",
			Timestamp = DateTime.Now.AddHours(-2)
		});
		
		MoodEntries.Add(new MoodEntry 
		{ 
			Mood = "Neutral",
			Note = "Just a regular day, nothing special.",
			Timestamp = DateTime.Now.AddDays(-1)
		});
		
		MoodEntries.Add(new MoodEntry 
		{ 
			Mood = "Amazing",
			Note = "Finished my project ahead of schedule! Feeling accomplished.",
			Timestamp = DateTime.Now.AddDays(-2)
		});
	}

	public ObservableCollection<MoodEntry> MoodEntries
	{
		get => _moodEntries;
		set
		{
			_moodEntries = value;
			OnPropertyChanged();
			UpdateStats();
		}
	}

	public string SelectedMood
	{
		get => _selectedMood;
		set
		{
			_selectedMood = value;
			OnPropertyChanged();
			((RelayCommand)SaveEntryCommand).RaiseCanExecuteChanged();
		}
	}

	public string NewNote
	{
		get => _newNote;
		set
		{
			_newNote = value;
			OnPropertyChanged();
		}
	}

	public string TotalEntries
	{
		get => _totalEntries;
		set
		{
			_totalEntries = value;
			OnPropertyChanged();
		}
	}

	public string RecentMood
	{
		get => _recentMood;
		set
		{
			_recentMood = value;
			OnPropertyChanged();
		}
	}

	public ICommand SaveEntryCommand { get; }

	private void SaveEntry()
	{
		if (!string.IsNullOrWhiteSpace(SelectedMood))
		{
			var entry = new MoodEntry
			{
				Mood = SelectedMood,
				Note = NewNote?.Trim() ?? string.Empty,
				Timestamp = DateTime.Now
			};

			MoodEntries.Insert(0, entry);
			
			// Keep only last 7 entries (data persistence simulation)
			while (MoodEntries.Count > 7)
			{
				MoodEntries.RemoveAt(MoodEntries.Count - 1);
			}

			SelectedMood = string.Empty;
			NewNote = string.Empty;
		}
	}

	private void OnSaveEntryClicked(object? sender, EventArgs e)
	{
		SaveEntry();
	}

	private void UpdateStats()
	{
		TotalEntries = MoodEntries.Count.ToString();
		RecentMood = MoodEntries.Count > 0 ? MoodEntries[0].MoodEmoji : "-";
	}

	public new event PropertyChangedEventHandler? PropertyChanged;

	protected new void OnPropertyChanged([CallerMemberName] string? propertyName = null)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
