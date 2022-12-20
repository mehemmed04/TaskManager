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
        public MainViewModel()
        {
            Processes = Process.GetProcesses().ToList();
        }
    }
}
