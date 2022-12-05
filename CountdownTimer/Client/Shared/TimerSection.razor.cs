using Microsoft.AspNetCore.Components;
using System.Timers;

namespace CountdownTimer.Client.Shared
{
    public partial class TimerSection
    {
        private String secondsStyle, minutesStyle, hoursStyle;
        private int days, hours, minutes, seconds;

        private DateTime comingSoon;
        private System.Timers.Timer timer = new(1000);

        [Parameter]
        public DateTime? comingSoonDate { get; set; }

        /*
         * If comingSoonDate not set then default value is 24 // test;
         */
        public TimerSection()
        {
            this.secondsStyle = this.minutesStyle = this.hoursStyle = "";
            if(this.comingSoonDate == null)
            {
                //this.comingSoon = DateTime.Today.AddDays(1);
                this.comingSoon = new DateTime(2022, 12, 6);
            } else
            {
                this.comingSoon = (DateTime)this.comingSoonDate;
                
            }

        }



        protected override async Task OnInitializedAsync()
        {
           
            timer.Elapsed += (sender, EventArgs) => CountDown();
            timer.Start();
            await base.OnInitializedAsync();
        }

        private void CountDown()
        {
            var distance = comingSoon - DateTime.Now;
            this.hours = distance.Hours;
            this.minutes = distance.Minutes;
            this.seconds = distance.Seconds;
            this.days = distance.Days;
            SetProgressBarStyles();
            StateHasChanged();
        }

        private void SetProgressBarStyles()
        {
            String styleStartStr = "background: conic-gradient(#140033 ";
            String styleEndStr = "deg, #ededed 0deg);";

            double secondsValue = this.seconds * 6;
            this.secondsStyle = styleStartStr + secondsValue + styleEndStr;

            double minutesValue = this.minutes * 6;
            this.minutesStyle = styleStartStr + minutesValue + styleEndStr;

            double hoursValue = this.hours * (360/24);
            this.hoursStyle = styleStartStr + hoursValue + styleEndStr;



        }
    }
}
