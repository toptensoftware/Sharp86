// Sharp86 - 8086 Emulator
// Copyright © 2017-2021 Topten Software. All Rights Reserved.
// 
// Licensed under the Apache License, Version 2.0 (the "License"); you may 
// not use this product except in compliance with the License. You may obtain 
// a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, WITHOUT 
// WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the 
// License for the specific language governing permissions and limitations 
// under the License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topten.JsonKit;

namespace Topten.Sharp86
{
    public class WatchExpression
    {
        public WatchExpression()
        {
        }

        public WatchExpression(Expression expression)
        {
            _expression = expression;
        }

        [Json("number")]
        public int Number;

        [Json("expression")]
        public string ExpressionText
        {
            get { return _expression == null ? null : _expression.OriginalExpression; }
            set { _expression = value == null ? null : new Expression(value); }
        }

        [Json("name")]
        public string Name
        {
            get;
            set;
        }

        public Expression Expression
        {
            get { return _expression; }
            set { _expression = value; }
        }

        public string EvalAndFormat(DebuggerCore debugger)
        {
            try
            {
                return debugger.ExpressionContext.EvalAndFormat(_expression);
            }
            catch (Exception x)
            {
                return "err:" + x.Message;
            }
        }

        Expression _expression;

        public override string ToString()
        {
            return string.Format("#{0} - {1}", Number, _expression.OriginalExpression);
        }
    }
}
