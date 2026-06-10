# 🌿 LifeHub

**Your Personal Life Management Ecosystem**

LifeHub, .NET MAUI ile geliştirilmiş, günlük yaşamınızı organize etmenize yardımcı olan dört uygulamadan oluşan bir kişisel yaşam yönetim platformudur. Alışkanlık takibi, ruh hali günlüğü ve planlayıcıyı tek bir ekosistemde bir araya getirir.

---

## 📱 Uygulamalar

| Uygulama | Açıklama |
|----------|----------|
| **🏠 LifeHub.Dashboard** | Tüm uygulamalara erişim sağlayan ana panel |
| **🎯 LifeHub.HabitTracker** | Günlük alışkanlıkları basit checkbox'larla takip et, ilerlemeyi izle |
| **💚 LifeHub.MoodJournal** | Günlük ruh halini ve düşünceleri kaydet, duygusal örüntüleri keşfet |
| **📅 LifeHub.Planner** | Görevleri oluştur, yönet ve tamamla |

---

## 🛠️ Teknolojiler

- **.NET 9**
- **.NET MAUI** (Multi-platform App UI)
- **C#** + **XAML**
- **MVVM** pattern (Commands klasörleri)

### Hedef Platformlar
- Android (`net9.0-android`)
- iOS (`net9.0-ios`)
- macOS (`net9.0-maccatalyst`)
- Windows (`net9.0-windows10.0.19041.0`)

---

## 🚀 Başlangıç

### Gereksinimler
- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- Visual Studio 2022 (17.5+) veya JetBrains Rider — **MAUI workload** kurulu
- Windows için: `Windows 10/11 SDK`

### Kurulum

```bash
git clone https://github.com/<kullanici-adin>/lifehub.git
cd lifehub
dotnet workload install maui
dotnet restore "windows proje.sln"
```

### Çalıştırma

Tüm çözümü açmak için:
```bash
start "windows proje.sln"
```

Belirli bir uygulamayı doğrudan çalıştırmak için:
```bash
dotnet build LifeHub.Dashboard/LifeHub.Dashboard.csproj -f net9.0-windows10.0.19041.0
dotnet run --project LifeHub.Dashboard -f net9.0-windows10.0.19041.0
```

---

## 📂 Proje Yapısı

```
lifehub-main/
├── LifeHub.Dashboard/      # Ana panel
├── LifeHub.HabitTracker/   # Alışkanlık takibi
├── LifeHub.MoodJournal/    # Ruh hali günlüğü
├── LifeHub.Planner/        # Görev yönetimi
├── windows proje.sln       # Visual Studio Solution
└── .gitignore
```

Her alt proje aynı MAUI iskeletini paylaşır: `App.xaml`, `AppShell.xaml`, `MainPage.xaml`, `MauiProgram.cs`, `Commands/`, `Models/`, `Resources/`, `Platforms/`.

---

## 🎨 Tasarım

Koyu tema, neon yeşil (`#00D9A3`) vurgular ve yuvarlatılmış kart tabanlı modern bir arayüz.

---

## 📄 Lisans

MIT
