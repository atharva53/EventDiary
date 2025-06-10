using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EventDiary
{
    public class EventCalendarControl : UserControl
    {
        private MonthCalendar calendar;
        private DateTime selectedDate;
        
        [Browsable(true)]
        [Category("Action")]
        [Description("Triggered when a date is selected")]
        public event EventHandler DateSelected;

        public DateTime SelectedDate 
        {
            get { return selectedDate; }
            private set 
            { 
                selectedDate = value;
                OnDateSelected(EventArgs.Empty);
            }
        }
        
        public EventCalendarControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            calendar = new MonthCalendar();
            SuspendLayout();
            
            calendar.Dock = DockStyle.Fill;
            calendar.Location = new Point(0, 0);
            calendar.MaxSelectionCount = 1;
            calendar.Name = "calendar";
            calendar.TabIndex = 0;
            calendar.DateChanged += Calendar_DateChanged;
            
            Controls.Add(calendar);
            Name = "EventCalendarControl";
            Size = new Size(250, 250);
            ResumeLayout(false);
        }

        private void Calendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            SelectedDate = calendar.SelectionStart;
        }
        
        protected virtual void OnDateSelected(EventArgs e)
        {
            DateSelected?.Invoke(this, e);
        }
    }
}