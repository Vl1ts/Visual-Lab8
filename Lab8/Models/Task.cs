using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using System.Reactive;
using Avalonia.Media.Imaging;

namespace Lab8.Models
{
    public class Task : ReactiveObject
    {
        public Task()
        {
            Header = "New task!";
            Description = "Change me!";
            ImagePath = "";
        }

        string header;
        public string Header
        {
            get { return header; }
            set
            {
                this.RaiseAndSetIfChanged(ref header, value);
            }
        }

        string description;
        public string Description
        {
            get { return description; }
            set
            {
                this.RaiseAndSetIfChanged(ref description, value);
            }
        }

        string imagePath;
        public string ImagePath
        {
            get { return imagePath; }
            set
            {             
                try
                {
                    TaskImage = new Bitmap(value);
                }
                catch (Exception ex)
                {
                    return;
                }
                this.RaiseAndSetIfChanged(ref imagePath, value);
            }
        }

        Bitmap taskImage;
        public Bitmap TaskImage
        {
            get { return taskImage; }
            set
            {
                this.RaiseAndSetIfChanged(ref taskImage, value);
            }
        }

    }
}
