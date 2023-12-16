using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SRChat.Client.ViewModels
{
    internal class BaseViewModel : BindableObject
    {
        protected void SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = default)
        {
            if (Equals(field, value)) return;
            field = value;
            base.OnPropertyChanged(propertyName);
        }
    }
}
