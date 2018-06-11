using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.BizTalk.CAT.BestPractices.Framework.Instrumentation;

namespace $safeprojectname$
{
    /// <summary>
    /// Write here your implementation for your custom Business Component
    /// </summary>
    [Serializable]
	public class SampleClass
	{
        /// <summary>
        /// Sample method to demonstrate the instrumentation
        /// </summary>
        /// <param name="param1"></param>
        public void SampleMethod(string param1)
        {
            var tmToken = TraceManager.CustomComponent.TraceIn(param1);

            var tmScopeToken = TraceManager.CustomComponent.TraceStartScope("Start Stopwatch");
            TraceManager.CustomComponent.TraceInfo("Do something of which the duration needs to be calculated");
            TraceManager.CustomComponent.TraceEndScope("Stop Stopwatch", tmScopeToken);

            TraceManager.CustomComponent.TraceOut(tmToken);
        }
	}
}
