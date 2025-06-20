using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventDiary
{
    public partial class Form1 : Form
    {
        private EventManager eventManager;
        private TextBox txtEventName;
        private EventCalendarControl calendarControl;
        private Button btnSubmit;
        private DataGridView dgvEvents;
        private Label lblEventName;
        private Label lblCalendar;
        private Label lblTitle;

        public Form1()
        {
            InitializeComponent();
            
            eventManager = new EventManager("eventdata.xml");
            InitializeCustomComponents();
            LoadEvents();
        }

        private void InitializeCustomComponents()
        {
            
            lblTitle = new Label
            {
                Text = "Event Diary with XML Serialization",
                Location = new Point(20, 10),
                Font = new Font(Font.FontFamily, 12, FontStyle.Bold),
                AutoSize = true
            };
            this.Controls.Add(lblTitle);

           
            lblEventName = new Label
            {
                Text = "Event Name:",
                Location = new Point(20, 50),
                AutoSize = true
            };
            this.Controls.Add(lblEventName);

            
            txtEventName = new TextBox
            {
                Location = new Point(20, 75),
                Size = new Size(250, 30)
            };
            this.Controls.Add(txtEventName);

            
            lblCalendar = new Label
            {
                Text = "Select Date:",
                Location = new Point(20, 115),
                AutoSize = true
            };
            this.Controls.Add(lblCalendar);

            
            calendarControl = new EventCalendarControl
            {
                Location = new Point(20, 140),
                Size = new Size(280, 200)
            };
            this.Controls.Add(calendarControl);

            
            btnSubmit = new Button
            {
                Text = "Add Event",
                Location = new Point(20, 360),
                Size = new Size(120, 35)
            };
            btnSubmit.Click += BtnSubmit_Click;
            this.Controls.Add(btnSubmit);

            // DataGridView for Events
            dgvEvents = new DataGridView
            {


                Location = new Point(320, 75),
                Size = new Size(450, 325),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToOrderColumns = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            
            dgvEvents.Columns.Add("EventName", "Event Name");
            dgvEvents.Columns.Add("EventDate", "Event Date");
            
            this.Controls.Add(dgvEvents);

            
            this.Text = "Event Diary";
            this.Size = new Size(800, 450);
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            string eventName = txtEventName.Text.Trim();
            
            if (string.IsNullOrEmpty(eventName))
            {
                MessageBox.Show("Please enter an event name.", "Input Required", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime eventDate = calendarControl.SelectedDate;
            
        
            eventManager.AddEvent(eventName, eventDate);
            
            
            LoadEvents();
            
          
            txtEventName.Text = string.Empty;
            
            MessageBox.Show("Event added successfully and saved as XML!", "Success", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        
        private void LoadEvents()
        {
            dgvEvents.Rows.Clear();
            
            var events = eventManager.GetAllEvents();
            foreach (var eventInfo in events)
            {
                dgvEvents.Rows.Add(eventInfo.EventName, eventInfo.EventDate.ToString("yyyy-MM-dd HH:mm"));
            }
        }
    }
}