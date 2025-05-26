// Form3.cs
// Represents the main application form after successful login.
using System;
using System.Windows.Forms; // Required for MessageBox, Cursors, etc.
using System.Drawing; // Required for Color, Font, Point

namespace iShop
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            InitializeCustomEventHandlers_Form3();
            this.Text = "iShop - Dashboard"; // Set a more descriptive title
        }

        private void InitializeCustomEventHandlers_Form3()
        {
            // Make labels in the side panel clickable
            MakeLabelClickable(label2, "Categories"); // categories
            MakeLabelClickable(label3, "Customers");  // customers
            MakeLabelClickable(label4, "Cart");       // cart
            MakeLabelClickable(label5, "Audit Log");  // audit_log
            MakeLabelClickable(label6, "Products");   // products
            MakeLabelClickable(label7, "Orders");     // orders
            MakeLabelClickable(label8, "Order Items"); // order items
            MakeLabelClickable(label9, "Payments");   // payments
            MakeLabelClickable(label10, "Logs");      // logs
            MakeLabelClickable(label11, "Shippers");  // shippers

            // Settings | Log Out Label
            this.label12.Click += new System.EventHandler(this.label12_Click);
            this.label12.Cursor = Cursors.Hand;
            // this.label12.ForeColor = Color.DarkGreen; // Already set in Designer, but good to be aware

            // Handle form closing to show login form again if user logs out or to exit app
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form3_FormClosing);
        }

        // Helper method to make labels behave like links
        private void MakeLabelClickable(Label label, string actionName)
        {
            if (label != null)
            {
                label.Cursor = Cursors.Hand;
                label.Tag = actionName; // Store the action name in the Tag property
                label.Click += PanelLabel_Click;
                // Optional: Add hover effects for better UX
                label.MouseEnter += (s, e) => { if (s is Label lbl) lbl.Font = new Font(lbl.Font, FontStyle.Underline | lbl.Font.Style); };
                label.MouseLeave += (s, e) => { if (s is Label lbl) lbl.Font = new Font(lbl.Font, FontStyle.Regular & ~FontStyle.Underline | (lbl.Font.Style & ~FontStyle.Underline)); };
            }
        }

        // Generic click handler for panel labels
        private void PanelLabel_Click(object sender, EventArgs e)
        {
            if (sender is Label clickedLabel && clickedLabel.Tag is string actionName)
            {
                MessageBox.Show($"Navigating to {actionName} section...", actionName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                // In a real application, you would load the corresponding view or data.
                // For example:
                // if (actionName == "Customers") LoadCustomersView(); 
                // else if (actionName == "Products") LoadProductsView();
                // etc.
                // You might also want to update a central panel on Form3 with the new content.
            }
        }

        // Event handler for "Settings | Log Out" label (label12)
        private void label12_Click(object sender, EventArgs e)
        {
            // Create a context menu for "Settings" and "Log Out"
            ContextMenuStrip menu = new ContextMenuStrip();

            ToolStripMenuItem settingsItem = new ToolStripMenuItem("Settings");
            settingsItem.Click += (s, args) => {
                MessageBox.Show("Opening application settings...", "Settings", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Example:
                // FormSettings settingsForm = new FormSettings();
                // settingsForm.ShowDialog(this); // Show as modal to this form
            };
            menu.Items.Add(settingsItem);

            menu.Items.Add(new ToolStripSeparator()); // Optional separator

            ToolStripMenuItem logoutItem = new ToolStripMenuItem("Log Out");
            logoutItem.Click += (s, args) => {
                DialogResult result = MessageBox.Show(this, "Are you sure you want to log out?", "Log Out", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    // Indicate that logout is happening so FormClosing event can differentiate
                    this.Tag = "LoggingOut";
                    this.Close(); // This will trigger Form3_FormClosing
                }
            };
            menu.Items.Add(logoutItem);

            // Show the menu at the label's location relative to the screen
            // The PointToScreen is important if the label is inside other containers
            // label12.ContextMenuStrip = menu; // Assigning here is not strictly necessary for Show()
            menu.Show(label12.PointToScreen(new Point(0, label12.Height)));
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Check if the form is closing because the user is logging out
            if (this.Tag != null && this.Tag.ToString() == "LoggingOut")
            {
                // Find Form1 if it exists and is not disposed, otherwise create a new one
                Form1 loginForm = null;
                foreach (Form openForm in Application.OpenForms)
                {
                    if (openForm is Form1 && !openForm.IsDisposed)
                    {
                        loginForm = (Form1)openForm;
                        break;
                    }
                }

                if (loginForm == null)
                {
                    loginForm = new Form1();
                }
                loginForm.Show();
                // Do not call Application.Exit() here, as we are returning to the login form.
            }
            else if (e.CloseReason == CloseReason.UserClosing)
            {
                // If the user clicks the 'X' button on Form3 (and not due to logout),
                // confirm if they want to exit the entire application.
                DialogResult result = MessageBox.Show(this, "Are you sure you want to exit iShop?", "Exit Application", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    e.Cancel = true; // Prevent the form from closing
                }
                else
                {
                    Application.Exit(); // Exit the entire application
                }
            }
            // If closing for other reasons (e.g., Application.Exit() was called elsewhere,
            // or system shutdown), let it close without further prompts.
        }


        // Original click handlers from the designer (can be removed if not used,
        // as they are now handled by the generic PanelLabel_Click if MakeLabelClickable was used).
        // It's generally safe to leave them empty if the designer still has them wired up.
        private void label2_Click(object sender, EventArgs e)
        {
            // This would be triggered if label2.Click was still wired in the designer
            // AND MakeLabelClickable did not re-wire it.
            // If MakeLabelClickable is used, PanelLabel_Click handles it.
        }

        private void label3_Click(object sender, EventArgs e)
        {
            // Similar to label2_Click.
        }
    }
}
