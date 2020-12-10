using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;

namespace StandardProject.Crm.Workflows.DateTimeActivities
{
    /// <summary>
    /// Make a date from components
    /// </summary>
    public class MakeDateActivity : CodeActivity
    {

        [RequiredArgument]
        [Input("Day")]
        public InArgument<int> Day { get; set; }

        [RequiredArgument]
        [Input("Month")]
        public InArgument<int> Month { get; set; }

        [RequiredArgument]
        [Input("Year")]
        public InArgument<int> Year { get; set; }

        [Output("Result date")]
        public OutArgument<DateTime> Date { get; set; }

        [Output("Is date valid")]
        public OutArgument<bool> IsValid { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            ITracingService tracingservice = context.GetExtension<ITracingService>();

            int day = Day.Get<int>(context);
            int month = Month.Get<int>(context);
            int year = Year.Get<int>(context);

            try
            {
                var result = new DateTime(year, month, day);

                Date.Set(context, result);
                IsValid.Set(context, true);
            }
            catch (Exception exc)
            {
                tracingservice.Trace(exc.ToString());

                IsValid.Set(context, false);
            }
        }
    }
}
