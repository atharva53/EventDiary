using System.Data;
using System.Drawing;

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

        public Form1()
        {
            InitializeComponent();
            eventManager = new EventManager();
            InitializeCustomComponents();
            LoadEvents();
        }

        private void InitializeCustomComponents()
        {
            // Event Name Label
            lblEventName = new Label
            {
                Text = "Event Name:",
                Location = new Point(20, 20),
                AutoSize = true
            };
            this.Controls.Add(lblEventName);

            // Event Name TextBox
            txtEventName = new TextBox
            {
                Location = new Point(20, 50),
                Size = new Size(250, 30)
            };
            this.Controls.Add(txtEventName);

            // Calendar Label
            lblCalendar = new Label
            {
                Text = "Select Date:",
                Location = new Point(20, 90),
                AutoSize = true
            };
            this.Controls.Add(lblCalendar);

            // Custom Calendar Control
            calendarControl = new EventCalendarControl
            {
                Location = new Point(20, 120),
                Size = new Size(280, 200)
            };
            this.Controls.Add(calendarControl);

            // Submit Button
            btnSubmit = new Button
            {
                Text = "Add Event",
                Location = new Point(20, 340),
                Size = new Size(120, 35)
            };
            btnSubmit.Click += BtnSubmit_Click;
            this.Controls.Add(btnSubmit);

            // DataGridView for Events
            dgvEvents = new DataGridView
            {
                Location = new Point(320, 50),
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

            // Form properties
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
            
            // Add the event
            eventManager.AddEvent(eventName, eventDate);
            
            // Refresh the grid
            LoadEvents();
            
            // Clear the input field
            txtEventName.Text = string.Empty;
            
            MessageBox.Show("Event added successfully!", "Success", 
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
