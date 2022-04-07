using System;
using System.Collections.Generic;
using System.Text;
using Lab8.Models;
using System.Reactive;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.IO;

namespace Lab8.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            //TasksToDo = new ObservableCollection<Task>();
            TasksToDo = new ObservableCollection<Task> { new Task() };
            TasksDoing = new ObservableCollection<Task> { new Task(), new Task(), new Task(), new Task(), new Task() };
            TasksDone = new ObservableCollection<Task> { new Task(), new Task(), new Task() };
        }

        ObservableCollection<Task> tasksToDo;
        public ObservableCollection<Task> TasksToDo
        {
            get
            {
                return tasksToDo;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref tasksToDo, value);
            }
        }

        ObservableCollection<Task> tasksDoing;
        public ObservableCollection<Task> TasksDoing
        {
            get
            {
                return tasksDoing;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref tasksDoing, value);
            }
        }

        ObservableCollection<Task> tasksDone;
        public ObservableCollection<Task> TasksDone
        {
            get
            {
                return tasksDone;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref tasksDone, value);
            }
        }

        public void AddTaskToDo()
        {
            TasksToDo.Add(new Task());
        }

        public void AddTaskDoing()
        {
            TasksDoing.Add(new Task());
        }

        public void AddTaskDone()
        {
            TasksDone.Add(new Task());
        }

        public void DeleteTaskToDo(Task thisTask)
        {
            TasksToDo.Remove(thisTask);
        }

        public void DeleteTaskDoing(Task thisTask)
        {
            TasksDoing.Remove(thisTask);
        }

        public void DeleteTaskDone(Task thisTask)
        {
            TasksDone.Remove(thisTask);
        }

        public void SaveFile(string path)
        {
            using (StreamWriter sw = File.CreateText(path))
            {
                foreach (var currentTaskToDo in TasksToDo)
                {
                    sw.Write($"{currentTaskToDo.Header}\n{currentTaskToDo.Description}\n{currentTaskToDo.ImagePath}\n");
                }
                sw.Write("!\n");
                foreach (var currentTaskDoing in TasksDoing)
                {
                    sw.Write($"{currentTaskDoing.Header}\n{currentTaskDoing.Description}\n{currentTaskDoing.ImagePath}\n");
                }
                sw.Write("!\n");
                foreach (var currentTaskDone in TasksDone)
                {
                    sw.Write($"{currentTaskDone.Header}\n{currentTaskDone.Description}\n{currentTaskDone.ImagePath}\n");
                }

                //foreach (var currentTaskToDo in TasksToDo)
                //{
                //    sw.Write($"\"{currentTaskToDo.Header}\" \"{currentTaskToDo.Description}\" \"{currentTaskToDo.ImagePath}\" ");
                //}
                //sw.Write('\n');
                //foreach (var currentTaskDoing in TasksDoing)
                //{
                //    sw.Write($"\"{currentTaskDoing.Header}\" \"{currentTaskDoing.Description}\" \"{currentTaskDoing.ImagePath}\" ");
                //}
                //sw.Write('\n');
                //foreach (var currentTaskDone in TasksDone)
                //{
                //    sw.Write($"\"{currentTaskDone.Header}\" \"{currentTaskDone.Description}\" \"{currentTaskDone.ImagePath}\" ");
                //}
            }
        }

        public void OpenFile(string path)
        {
            if (File.Exists(path))
            {
                TasksToDo.Clear();
                TasksDoing.Clear();
                TasksDone.Clear();

                using (StreamReader sr = File.OpenText(path))
                {
                    var currentString = "";
                    while ((currentString = sr.ReadLine()) != "!")
                    {
                        Task newTask = new Task();

                        newTask.Header = currentString;
                        newTask.Description = sr.ReadLine();
                        newTask.ImagePath = sr.ReadLine();

                        TasksToDo.Add(newTask);
                    }

                    currentString = "";
                    while ((currentString = sr.ReadLine()) != "!")
                    {
                        Task newTask = new Task();

                        newTask.Header = currentString;
                        newTask.Description = sr.ReadLine();
                        newTask.ImagePath = sr.ReadLine();

                        TasksDoing.Add(newTask);
                    }

                    currentString = "";
                    while ((currentString = sr.ReadLine()) != null)
                    {
                        Task newTask = new Task();
                        
                        newTask.Header = currentString;
                        newTask.Description = sr.ReadLine();
                        newTask.ImagePath = sr.ReadLine();

                        TasksDone.Add(newTask);
                    }

                    //var currentString = "";

                    //while ((currentString = sr.ReadLine()) != null)
                    //{
                    //    var i = 0;
                    //    while(currentString.IndexOf('"', 0) != -1)
                    //    {
                    //        Task newTask = new Task();

                    //        //ѕоиск Header в кавычках: первые ищутс€ с индекса номер (0), вторые - с индекса номер (1)
                    //        //+1 - начинаем с первых кавычек (не включа€), длина - (индекс вторых - 1)
                    //        newTask.Header = currentString.Substring(currentString.IndexOf('"', 0) + 1, currentString.IndexOf('"', 1) - 1);
                    //        //ѕоиск Description в кавычках: первые ищутс€ с индекса номер (длина Header + 2 предыдущие кавычки + пробел), вторые - с индекса номер (индекс первых + 1)
                    //        //+1 - начинаем с первых кавычек (не включа€), длина - (индекс вторых - индекс первых - 1)
                    //        newTask.Description = currentString.Substring(currentString.IndexOf('"', newTask.Header.Length + 3) + 1, currentString.IndexOf('"', newTask.Header.Length + 4) - currentString.IndexOf('"', newTask.Header.Length + 3) - 1);
                    //        //ѕоиск ImagePath в кавычках: первые ищутс€ с индекса номер (длина Header + длина Description + 4 предыдущие кавычки + 2 пробела), вторые - с индекса номер (индекс первых + 1)
                    //        //+1 - начинаем с первых кавычек (не включа€), длина - (индекс вторых - индекс первых - 1)
                    //        newTask.ImagePath = currentString.Substring(currentString.IndexOf('"', newTask.Header.Length + newTask.Description.Length + 6) + 1, currentString.IndexOf('"', newTask.Header.Length + newTask.Description.Length + 7) - currentString.IndexOf('"', newTask.Header.Length + newTask.Description.Length + 6) - 1);

                    //        TasksToDo.Add(newTask);

                    //        //”бираем первый Task в строке
                    //        currentString = currentString.Substring(currentString.IndexOf('"', newTask.Header.Length + newTask.Description.Length + 7) + 1);
                    //    }
                    //}
                }
            }
        }
    }
}
