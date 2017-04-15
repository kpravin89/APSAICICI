using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSA.Portable.Nimbi.Navigation
{
    [AttributeUsage(AttributeTargets.Class)]
    public class RegisterViewModelAttribute : Attribute
    {
        public Type ViewModelType { get; private set; }

        public RegisterViewModelAttribute(Type viewModelType)
        {
            ViewModelType = viewModelType;
        }
    }
}
