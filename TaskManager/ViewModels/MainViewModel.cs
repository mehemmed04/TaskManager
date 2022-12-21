using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using TaskManager.Commands;

namespace TaskManager.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private int index;

        public int Index
        {
            get { return index; }
            set { index = value; OnPropertyChanged(); }
        }

        public int SelectedIndex { get; set; }
        private List<ProcessPriorityClass> priorities;

        public List<ProcessPriorityClass> Priorities
        {
            get { return priorities; }
            set { priorities = value; OnPropertyChanged(); }
        }

        private ProcessPriorityClass selectedPriorityClass;

        public ProcessPriorityClass SelectedPriorityClass
        {
            get { return selectedPriorityClass; }
            set { selectedPriorityClass = value; OnPropertyChanged(); }
        }


        private List<Process> processes;

        public List<Process> Processes
        {
            get { return processes; }
            set { processes = value; OnPropertyChanged(); }
        }


        private Process selectedProcess;

        public Process SelectedProcess
        {
            get { return selectedProcess; }
            set { selectedProcess = value; OnPropertyChanged(); }
        }
        public RelayCommand KillProcessCommand { get; set; }
        public RelayCommand ChangedCommand { get; set; }
        public RelayCommand ChangePriorityCommand { get; set; }
        private string filename;

        public string Filename
        {
            get { return filename; }
            set { filename = value; }
        }

        public RelayCommand CreateCommand { get; set; }
        public MainViewModel()
        {


            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            timer.Tick += UpdateProcess;
            timer.Start();
            Processes = Process.GetProcesses().ToList();
            Priorities = Enum.GetValues(typeof(ProcessPriorityClass)).Cast<ProcessPriorityClass>().ToList();
            KillProcessCommand = new RelayCommand((o) =>
            {
                if (selectedProcess != null)
                {
                    try
                    {
                        selectedProcess.Kill();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Select Process");
                }
            });

            ChangedCommand = new RelayCommand((o) =>
            {
                if (Index != -1)
                {
                    SelectedIndex = Index;
                }
            });

            ChangePriorityCommand = new RelayCommand((o) =>
            {
                if (selectedProcess != null)
                {
                    try
                    {
                        SelectedProcess.PriorityClass = SelectedPriorityClass;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Select Process");
                }
            });


            CreateCommand = new RelayCommand((o) =>
            {
                var p = Process.Start(Filename+".exe");
               int i=  Processes.IndexOf(p);
                SelectedIndex = i;
                index = SelectedIndex;
            });


        }

        private void UpdateProcess(object sender, EventArgs e)
        {
            Processes = Process.GetProcesses().ToList();
            Index = SelectedIndex;
        }
    }
}
