using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeLine.AdminDesktop.ViewModels.Base
{
    public abstract class BaseViewModel: ObservableObject
    {
        public virtual Task InitializeAsync()
        {
            return Task.CompletedTask;
        }
    }
}
