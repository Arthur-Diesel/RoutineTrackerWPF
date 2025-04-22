using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RoutineTracker.Classes;
using SQLite;

namespace RoutineTracker
{
    public partial class MainWindow : Window
    {
        Routine routine;
        public MainWindow()
        {
            InitializeComponent();
            TextBlockDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
            {
                connection.CreateTable<Routine>();
                routine = connection.Table<Routine>().FirstOrDefault(r => r.Date == DateTime.Now.Date);
                if (routine != null)
                {
                    routine.Date = DateTime.Now.Date;
                    ToggleBreakfest.IsChecked = routine.Breakfest;
                    ToggleLunch.IsChecked = routine.Lunch;
                    ToggleEveningLunch.IsChecked = routine.EveningLunch;
                    ToggleDinner.IsChecked = routine.Dinner;
                    ToggleOutOfDiet.IsChecked = routine.OutOfDiet;
                    ToggleExercise.IsChecked = routine.Exercise;
                    ToggleAlcoohol.IsChecked = routine.Alcoohol;
                    ToggleLecture.IsChecked = routine.Lecture;
                    ToggleStudy.IsChecked = routine.Study;
                    ToggleOrganization.IsChecked = routine.Organization;
                }
                else
                {
                    routine = new Routine
                    {
                        Date = DateTime.Now.Date,
                        Breakfest = false,
                        Lunch = false,
                        EveningLunch = false,
                        Dinner = false,
                        OutOfDiet = false,
                        Exercise = false,
                        Alcoohol = false,
                        Lecture = false,
                        Study = false,
                        Organization = false
                    };
                    connection.Insert(routine);
                }
            }
        }

        private void HandleCheck(object sender, RoutedEventArgs e)
        {
            if(sender is ToggleButton toggleButton)
            {
                switch (toggleButton.Name)
                {
                    case "ToggleBreakfest":
                        routine.Breakfest = true;
                        break;
                    case "ToggleLunch":
                        routine.Lunch = true;
                        break;
                    case "ToggleEveningLunch":
                        routine.EveningLunch = true;
                        break;
                    case "ToggleDinner":
                        routine.Dinner = true;
                        break;
                    case "ToggleOutOfDiet":
                        routine.OutOfDiet = true;
                        break;
                    case "ToggleExercise":
                        routine.Exercise = true;
                        break;
                    case "ToggleAlcoohol":
                        routine.Alcoohol = true;
                        break;
                    case "ToggleLecture":
                        routine.Lecture = true;
                        break;
                    case "ToggleStudy":
                        routine.Study = true;
                        break;
                    case "ToggleOrganization":
                        routine.Organization = true;
                        break;
                }
                using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
                {
                    connection.CreateTable<Routine>();
                    connection.Update(routine);
                }
            }
        }

        private void HandleUnchecked(object sender, RoutedEventArgs e)
        {
            if(sender is ToggleButton toggleButton)
            {
                switch (toggleButton.Name)
                {
                    case "ToggleBreakfest":
                        routine.Breakfest = false;
                        break;
                    case "ToggleLunch":
                        routine.Lunch = false;
                        break;
                    case "ToggleEveningLunch":
                        routine.EveningLunch = false;
                        break;
                    case "ToggleDinner":
                        routine.Dinner = false;
                        break;
                    case "ToggleOutOfDiet":
                        routine.OutOfDiet = false;
                        break;
                    case "ToggleExercise":
                        routine.Exercise = false;
                        break;
                    case "ToggleAlcoohol":
                        routine.Alcoohol = false;
                        break;
                    case "ToggleLecture":
                        routine.Lecture = false;
                        break;
                    case "ToggleStudy":
                        routine.Study = false;
                        break;
                    case "ToggleOrganization":
                        routine.Organization = false;
                        break;
                }
                using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
                {
                    connection.CreateTable<Routine>();
                    connection.Update(routine);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
            {
                connection.CreateTable<Routine>();
                List<Routine> routines = connection.Table<Routine>().ToList();

                StringBuilder csvContent = new StringBuilder();
                csvContent.AppendLine("Id,Date,Breakfest,Lunch,EveningLunch,Dinner,OutOfDiet,Exercise,Alcoohol,Lecture,Study,Organization");
                string date = DateTime.Now.ToString("dd-MM-yyyy");

                foreach (var routine in routines)
                {
                    csvContent.AppendLine($"{routine.Id},{routine.Date:dd/MM/yyyy},{routine.Breakfest},{routine.Lunch},{routine.EveningLunch},{routine.Dinner},{routine.OutOfDiet},{routine.Exercise},{routine.Alcoohol},{routine.Lecture},{routine.Study},{routine.Organization}");
                }

                Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog
                {
                    Filter = "CSV files (*.csv)|*.csv",
                    FileName = "Routines_" + date + ".csv",
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    System.IO.File.WriteAllText(saveFileDialog.FileName, csvContent.ToString());
                    MessageBox.Show("CSV file saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
    }
}
