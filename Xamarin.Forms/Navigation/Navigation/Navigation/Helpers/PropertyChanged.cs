using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Navigation.Helpers
{
    public class PropertyChanged : BindableObject
    {
        /// <summary>
        /// FUNCTION THAT HANDLES CHANGES MADE TO A PROPERTY
        /// PROPAGATES UPDATES TO THE UI
        /// </summary>
        public void RaisePropertyChanged<T>(Expression<Func<T>> Property)
        {
            var PropertyName = GetMemberInfo(Property).Name;
            OnPropertyChanged(PropertyName);
        }

        /// <summary>
        /// GETS METADATA ABOUT MEMBERS OF A CLASS
        /// </summary>
        private MemberInfo GetMemberInfo(Expression E)
        {
            MemberExpression Operand;
            LambdaExpression Lambda = (LambdaExpression)E;
            if (Lambda.Body as UnaryExpression != null)
            {
                UnaryExpression Body = (UnaryExpression)Lambda.Body;
                Operand = (MemberExpression)Body.Operand;
            }
            else
            {
                Operand = (MemberExpression)Lambda.Body;
            }
            return Operand.Member;
        }
    }
}