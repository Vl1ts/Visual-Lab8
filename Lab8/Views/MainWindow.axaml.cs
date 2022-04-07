using Avalonia.Controls;
using Lab8.ViewModels;
using Lab8.Models;
using System.Collections.Generic;

namespace Lab8.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.FindControl<MenuItem>("MenuAboutBtn").Click += async delegate
            {
                var aboutWindow = new AboutWindow();
                await aboutWindow.ShowDialog((Window)this.VisualRoot);
            };
            this.FindControl<MenuItem>("MenuExitBtn").Click += delegate
            {
                this.Close();
            };
            this.FindControl<MenuItem>("MenuNewBtn").Click += delegate
            {
                (this.DataContext as MainWindowViewModel).TasksToDo.Clear();
                (this.DataContext as MainWindowViewModel).TasksDoing.Clear();
                (this.DataContext as MainWindowViewModel).TasksDone.Clear();
            };
            this.FindControl<MenuItem>("MenuSaveBtn").Click += async delegate
            {
                var filePath = new SaveFileDialog()
                {
                    Title = "Save file"
                }.ShowAsync((Window)this.VisualRoot);

                string? path = await filePath;

                var context = this.DataContext as MainWindowViewModel;
                if (path != null)
                {
                    context.SaveFile(path);
                }
            };
            this.FindControl<MenuItem>("MenuLoadBtn").Click += async delegate
            {
                var filePath = new OpenFileDialog()
                {
                    Title = "Open file"
                }.ShowAsync((Window)this.VisualRoot);

                string[]? path = await filePath;

                var context = this.DataContext as MainWindowViewModel;
                if (path != null)
                {
                    context.OpenFile(string.Join(@"\", path));
                }
            };
        }

        public async void AddImg(Task currentTask)
        {
            var imageFilter = new List<FileDialogFilter>();
            imageFilter.Add(new FileDialogFilter()
            {
                Extensions = { "ico", "jpg", "jpeg", "png" }
            });

            var windowChooseImage = new OpenFileDialog()
            {
                Title = "Choose image",
                Filters = imageFilter
            }.ShowAsync((Window)this.VisualRoot);

            string[]? path = await windowChooseImage;
            string imgPath;
            
            if (path != null)
            {
                imgPath = string.Join(@"\", path);
                currentTask.ImagePath = imgPath;
            }
        }
    }
}
