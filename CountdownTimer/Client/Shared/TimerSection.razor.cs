using Microsoft.AspNetCore.Components;
using System.Timers;

namespace CountdownTimer.Client.Shared
{
    public partial class TimerSection
    {
        private String secondsStyle, minutesStyle, hoursStyle, daysStyle;
        private int days, hours, minutes, seconds;

        private DateTime comingSoon;
        private System.Timers.Timer timer = new(1000);
        private String? test;

        [Parameter]
        public String? comingSoonStr { get; set; }


        public TimerSection()
        {
            this.secondsStyle = this.minutesStyle = this.hoursStyle = this.daysStyle = "";

        }



        private void IsComingSoonSet()
        {
            /*
            * If comingSoonDate not set then default value is 24hours after now
            */
            if (this.comingSoonStr!= null)
            {
                this.comingSoon = DateTime.Parse(comingSoonStr);
                //this.test = "comingSoonStr not null. " + comingSoon;
            } else
            {
                this.comingSoon = DateTime.Now.AddHours(24);
                //this.test = "comingSoonStr is null. " + comingSoon;
            }
        }


        protected override async Task OnInitializedAsync()
        {
            this.IsComingSoonSet();
            
            this.timer.Elapsed += (sender, EventArgs) => CountDown();
            this.timer.Start();
            await base.OnInitializedAsync();
            //this.test = "Test: " + this.comingSoonStr;
        }

        private void CountDown()
        {
            var distance = comingSoon - DateTime.Now;
            this.hours = distance.Hours;
            this.minutes = distance.Minutes;
            this.seconds = distance.Seconds;
            this.days = distance.Days;
            
            if(this.days <=0 && this.hours<=0 && this.minutes<=0 && this.seconds<=0)
            {
                this.days = this.hours = this.minutes = this.seconds = 0;
                this.timer.Stop();
            }

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

            double daysValue = Math.Round(0.986 * this.days, 0);
            this.daysStyle = styleStartStr + daysValue + styleEndStr;
            this.test = "test1: " + daysValue;

            /**/



        }
    }
}
