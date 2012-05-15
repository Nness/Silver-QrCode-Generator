/*
 *  Silver QrCode Generator - Windows WPF application to generate QrCode.
 *  Copyright (c) 2012 Canxing(Jason) He
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>. 
*/
using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace QrCodeGenerator.ViewModel
{
    public static class PropertyChangedExtensions
    {
        public static void Raise<T>(this PropertyChangedEventHandler handler,
            Expression<Func<T>> memberExpression)
        {
            if (memberExpression == null)
                throw new ArgumentNullException("memberExpression");

            MemberExpression body = memberExpression.Body as MemberExpression;
            if (body == null)
                throw new ArgumentException("Lambda must return a property.");

            ConstantExpression vmExpression = body.Expression as ConstantExpression;

            if (vmExpression == null)
                throw new ArgumentException("'propertyExpression' body should be a constant expression");

            LambdaExpression lambda = Expression.Lambda(vmExpression);
            Delegate vmFunc = lambda.Compile();
            object sender = vmFunc.DynamicInvoke();

            if (handler != null)
            {
                handler(sender, new PropertyChangedEventArgs(body.Member.Name));
            }

        }
    }
}
