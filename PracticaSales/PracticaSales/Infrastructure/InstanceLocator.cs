using System;
using System.Collections.Generic;
using System.Text;
using PracticaSales.ViewModels;

namespace PracticaSales.Infrastructure
{
    public class InstanceLocator
    {
        public MainViewModel Main { get; set; }

        public InstanceLocator()
        {
            Main = new MainViewModel();
        }
    }
}
