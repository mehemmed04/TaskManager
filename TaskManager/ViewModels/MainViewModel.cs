using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.ViewModels
{
    public class MainViewModel
    {
        public List<Process> Processes { get; set; }
        private Process selectedProcess;

        public Process SelectedProcess
        {
            get { return selectedProcess; }
            set { selectedProcess = value; }
        }

        public MainViewModel()
        {
            Processes = Process.GetProcesses().ToList();
        }
    }
}
